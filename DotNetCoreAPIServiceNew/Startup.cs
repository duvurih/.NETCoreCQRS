using Dot.Net.Core.Common.Enums;
using Dot.Net.Core.Common.Settings;
using Dot.Net.Core.Common.Utils;
using Dot.Net.Core.Interfaces.IntegrationService;
using Dot.Net.Core.Interfaces.Repository;
using Dot.Net.Core.Interfaces.Service;
using Dot.Net.Core.Services;
using Dot.Net.Core.Services.Forex.Query;
using Dot.Net.Core.Services.Infrastructure;
using Dot.Net.Core.Services.Observations.Query;
using Dot.Net.Infrastructure.Data;
using Dot.Net.Infrastructure.Integrations;
using DotNetCoreAPIServicesNew.Filters;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NSwag.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DotNetCoreAPIServiceNew
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvcCore()
                .AddJsonFormatters();
            services.AddHealthChecks();
            services.AddSwagger();

            // Add MediatR
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddMediatR(typeof(GetForexExchangeRateQueryHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(ObservationQueryHandler).GetTypeInfo().Assembly);

            // Adding configuration setting
            services.Configure<AppSettingsConfig>(Configuration.GetSection("ApplicationSettings"));
            services.Configure<DatabaseSettingsConfig>(Configuration.GetSection("DatabaseSettings"));

            services.AddOptions();
            services
                .AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)));
            //.SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            //.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateCustomerCommandValidator>());

            //Distributed Caching
            //if (_hostContext.IsDevelopment())
            //{
            //    services.AddDistributedMemoryCache();
            //}
            //else
            //{
            //    services.AddDistributedSqlServerCache(options =>
            //    {
            //        options.ConnectionString =
            //            _config["DistCache_ConnectionString"];
            //        options.SchemaName = "dbo";
            //        options.TableName = "TestCache";
            //    });
            //}

            //depedency injection configuration
            services.AddTransient<IManageApi, ServiceManager>();
            services.AddTransient<IConnectToDatabase, ConnectDB>();
            services.AddTransient<IPalidromeRepository, PalindromeRepository>();
            services.AddTransient<IObservationRepository, ObservationRepository>();
            services.AddTransient<ICalculatorRepository, CalculatorRepository>();
            services.AddTransient<ICalculatorService, CalculatorService>();
            services.AddTransient<AddOperatorService>();
            services.AddTransient<SubtractOperatorService>();
            services.AddTransient<MultiplyOperatorService>();
            services.AddTransient<DivideOperatorService>();
            services.AddTransient<IForexIntegrationService, ForexIntegrationService>();

            services.AddTransient<Func<GlobalEnums.OperatorsEnum, ICalculateOperator>>(serviceProvider => key =>
            {
                switch (key)
                {
                    case GlobalEnums.OperatorsEnum.Add:
                        return serviceProvider.GetService<AddOperatorService>();
                    case GlobalEnums.OperatorsEnum.Subtract:
                        return serviceProvider.GetService<SubtractOperatorService>();
                    case GlobalEnums.OperatorsEnum.Multiply:
                        return serviceProvider.GetService<MultiplyOperatorService>();
                    case GlobalEnums.OperatorsEnum.Division:
                        return serviceProvider.GetService<DivideOperatorService>();
                    default:
                        throw new KeyNotFoundException();
                }
            });

            //enabling cors
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseCors(builder => builder.WithOrigins("http://localhost:4200"));
            app.UseCors(builder => builder.AllowAnyOrigin()
                                            .AllowAnyHeader()
                                            .AllowAnyMethod());

            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                // This custom writer formats the detailed status as JSON.
                ResponseWriter = WriteResponse,
            });

            app.UseSwaggerWithApiExplorer();
            //app.UseSwaggerUi3();
            app.UseSwaggerUi3(settings =>
            {
                settings.SwaggerUiRoute = "/api";
                //settings.SwaggerRoute = "/api/specification.json";
            });


            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private static Task WriteResponse(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";

            var json = new JObject(
                new JProperty("status", result.Status.ToString()),
                new JProperty("results", new JObject(result.Entries.Select(pair =>
                    new JProperty(pair.Key, new JObject(
                        new JProperty("status", pair.Value.Status.ToString()),
                        new JProperty("description", pair.Value.Description),
                        new JProperty("data", new JObject(pair.Value.Data.Select(p => new JProperty(p.Key, p.Value))))))))));
            return httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
        }
    }
}
