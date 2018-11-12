using Dot.Net.Core.Common.DTO;
using System.Collections.Generic;

namespace Dot.Net.Core.Services.Observations.Query
{
    public class ObservationModel
    {
        public IEnumerable<ObservationDTO> Observations { get; set; }

    }
}
