using MediatR;

namespace Dot.Net.Core.Services.Observations.Query
{
    public class ObservationQuery : IRequest<ObservationModel>
    {
        public string Id { get; set; }

    }
}
