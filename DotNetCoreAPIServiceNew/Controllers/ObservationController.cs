using Dot.Net.Core.Common.DTO;
using Dot.Net.Core.Services.Observations.Command.Create;
using Dot.Net.Core.Services.Observations.Query;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
//using System.Web.Http.Cors;

namespace DotNetCoreAPIServicesNew.Controllers
{
    //[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [Produces("application/json")]
    [Route("api/Observation")]
    public class ObservationController : BaseController
    {

        public ObservationController() { }

        [HttpGet]
        [Route("GetObservations/{userId}")]
        public async Task<IActionResult> GetAsync(string userId)
        {
            //return _observationService.GetObservations(userId);
            var command = new ObservationQuery { Id = userId };
            var response = await Mediator.Send(new ObservationQuery { Id = userId });
            return Ok(response);
        }

        // POST api/values
        [HttpPost]
        [Route("SaveObservation")]
        public async Task<IActionResult> PostAsync([FromBody] ObservationDTO observation)
        {
            //return _observationService.Save(observation);
            var command = new CreateObservationCommand
            {
                Id = 0,
                Discussion = observation.Discussion,
                DiscussionDate = observation.DiscussionDate,
                DiscussionLocation = observation.DiscussionLocation,
                DiscussionWith = observation.DiscussionWith,
                Subject = observation.Subject,
                Outcome = observation.Outcome,
                RecordedBy = observation.RecordedBy,
                RecordedDate = observation.RecordedDate
            };
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}