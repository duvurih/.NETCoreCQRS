using Dot.Net.Core.Common.DTO;
using System.Threading.Tasks;

namespace Dot.Net.Core.Interfaces.Service
{
    public interface IForexService
    {
        Task<ResponseCurrencyDTO> GetCurrencyConversion(RequestedCurrencyDTO currency);
    }
}
