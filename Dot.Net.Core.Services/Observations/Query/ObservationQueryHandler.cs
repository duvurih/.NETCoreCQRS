using Dot.Net.Core.Interfaces.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Dot.Net.Core.Services.Observations.Query
{
    public class ObservationQueryHandler : IRequestHandler<ObservationQuery, ObservationModel>
    {
        IObservationRepository _observationRepository;

        public ObservationQueryHandler(IObservationRepository observationRepository)
        {
            _observationRepository = observationRepository;
        }

        public async Task<ObservationModel> Handle(ObservationQuery request, CancellationToken cancellationToken)
        {
            var result = await _observationRepository.GetObservations(request.Id);
            var model = new ObservationModel
            {
                Observations = result
            };
            return model;
        }
    }
}
