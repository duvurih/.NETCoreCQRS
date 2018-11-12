using Dot.Net.Core.Common.Settings;
using Dot.Net.Core.Interfaces.Repository;
using Dot.Net.Core.Services.Observations.Query;
using Dot.Net.Infrastructure.Data;
using Microsoft.Extensions.Options;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Dot.Net.Application.Tests.Observations.Query
{
    public class ObservationQueryHandlerTest
    {
        [Fact]
        public async Task ObservationQueryTest()
        {
            DatabaseSettingsConfig dbSettings = new DatabaseSettingsConfig()
            {
                ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='E:\\MicroService CQRS\\MicroServicesCQRSDotNetCore\\Dot.Net.Infrastructure.Data\\Database\\PalindromeDB.mdf';Integrated Security=True;Connect Timeout=30"
            };
            IOptions<DatabaseSettingsConfig> iDBSettings = Options.Create(dbSettings);
            IConnectToDatabase connection = new ConnectDB(iDBSettings);
            IObservationRepository observationRepository = new ObservationRepository(connection);
            var sut = new ObservationQueryHandler(observationRepository);
            var result = await sut.Handle(new ObservationQuery { Id = "CBX000001" }, CancellationToken.None);

            result.ShouldBeOfType<ObservationModel>();
            result.Observations.ShouldNotBeNull();
        }
    }
}
