using Dot.Net.Core.Common.DTO;
using System.Threading.Tasks;

namespace Dot.Net.Core.Interfaces.IntegrationService
{
    public interface IForexIntegrationService
    {
        Task<ResponseCurrencyDTO> GetCurrencyConversion(RequestedCurrencyDTO currency);
    }
}
