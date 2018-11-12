using Dot.Net.Core.Common.DTO;
using System.Collections.Generic;

namespace Dot.Net.Core.Interfaces.Service
{
    public interface IObservationService
    {
        List<ObservationDTO> GetObservations(string userId);
        bool Save(ObservationDTO observation);
    }
}
