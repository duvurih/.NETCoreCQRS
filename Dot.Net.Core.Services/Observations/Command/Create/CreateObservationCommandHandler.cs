using Dot.Net.Core.Common.DTO;
using Dot.Net.Core.Interfaces.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Dot.Net.Core.Services.Observations.Command.Create
{
    public class CreateObservationCommandHandler : IRequestHandler<CreateObservationCommand, Unit>
    {
        IObservationRepository _observationRepository;

        public CreateObservationCommandHandler(IObservationRepository observationRepository)
        {
            _observationRepository = observationRepository;
        }

        public async Task<Unit> Handle(CreateObservationCommand request, CancellationToken cancellationToken)
        {
            var entity = new ObservationDTO
            {
                Id = request.Id,
                Discussion = request.Discussion,
                DiscussionDate = request.DiscussionDate,
                DiscussionLocation = request.DiscussionLocation,
                DiscussionWith = request.DiscussionWith,
                Subject = request.Subject,
                Outcome = request.Outcome,
                RecordedBy = request.RecordedBy,
                RecordedDate = request.RecordedDate
            };
            var result = await _observationRepository.Save(entity);
            return Unit.Value;
        }
    }
}
