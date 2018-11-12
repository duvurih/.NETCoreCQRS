using Dot.Net.Core.Common.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dot.Net.Core.Interfaces.Repository
{
    public interface IObservationRepository
    {
        Task<IEnumerable<ObservationDTO>> GetObservations(string userId);
        Task<bool> Save(ObservationDTO observation);
    }
}
