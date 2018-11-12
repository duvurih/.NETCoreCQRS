using Dot.Net.Core.Common.DTO;
using Dot.Net.Core.Common.Settings;
using Dot.Net.Core.Interfaces.Repository;
using Dot.Net.Core.Services.Observations.Command.Create;
using Dot.Net.Infrastructure.Data;
using Microsoft.Extensions.Options;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Dot.Net.Application.Tests.Observations.Command
{
    public class CreateObservationCommandHandlerTest
    {
        [Fact]
        public async Task CreateObservationQueryTest()
        {
            DatabaseSettingsConfig dbSettings = new DatabaseSettingsConfig()
            {
                ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='E:\\MicroService CQRS\\MicroServicesCQRSDotNetCore\\Dot.Net.Infrastructure.Data\\Database\\PalindromeDB.mdf';Integrated Security=True;Connect Timeout=30"
            };
            IOptions<DatabaseSettingsConfig> iDBSettings = Options.Create(dbSettings);
            IConnectToDatabase connection = new ConnectDB(iDBSettings);
            IObservationRepository observationRepository = new ObservationRepository(connection);
            var entity = new ObservationDTO
            {
                Id = 0,
                Discussion = "CQRS",
                DiscussionDate = DateTime.Now,
                DiscussionLocation = "Office",
                DiscussionWith = "Staff",
                Subject = "CQRS Pros vs Cons",
                Outcome = "Decided to go with CQRS",
                RecordedBy = "Hari",
                RecordedDate = DateTime.Now
            };
            var sut = new CreateObservationCommandHandler(observationRepository);

            var result = await sut.Handle(new CreateObservationCommand
            {
                Id = 0,
                Discussion = "CQRS",
                DiscussionDate = DateTime.Now,
                DiscussionLocation = "Office",
                DiscussionWith = "Staff",
                Subject = "CQRS Pros vs Cons",
                Outcome = "Decided to go with CQRS",
                RecordedBy = "Hari",
                RecordedDate = DateTime.Now
            }, CancellationToken.None);

            result.ShouldBeOfType<bool>();
            //result.ShouldBe(Unit);
        }
    }
}
