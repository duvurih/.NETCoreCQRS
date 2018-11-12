using Dot.Net.Core.Common.DTO;
using Dot.Net.Core.Common.Settings;
using Dot.Net.Core.Common.Utils;
using Dot.Net.Core.Interfaces.IntegrationService;
using Dot.Net.Core.Services.Forex.Query;
using Dot.Net.Infrastructure.Integrations;
using Microsoft.Extensions.Options;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Dot.Net.Application.Tests.Forex.Query
{
    public class GetForexExchangeRateQueryHandlerTest
    {

        public GetForexExchangeRateQueryHandlerTest()
        {
        }

        [Fact]
        public async Task GetForexExchangeRate()
        {

            RequestedCurrencyDTO requestedCurrencyDTO = new RequestedCurrencyDTO()
            {
                baseCurrency = "EUR",
                targetCurrency = "AUD",
                amount = 200,
                latestExchangeRate = true
            };
            AppSettingsConfig appSettings = new AppSettingsConfig()
            {
                ForexService = "http://data.fixer.io/api/",
                ForexApiKey = "00c362aa5caf69a39813ced3f8cb2e26"
            };
            IOptions<AppSettingsConfig> iAppSettings = Options.Create(appSettings);
            IManageApi apiManager = new ServiceManager(iAppSettings);
            IForexIntegrationService forexIntegrationService = new ForexIntegrationService(apiManager, iAppSettings);
            var sut = new GetForexExchangeRateQueryHandler(forexIntegrationService);
            var result = await sut.Handle(new GetForexExchangeRateQuery { requestedCurrencyDTO = requestedCurrencyDTO }, CancellationToken.None);

            result.ShouldBeOfType<ForexExchangeModel>();
            result.exchangeRate.ShouldBeGreaterThan(0);
        }
    }
}
