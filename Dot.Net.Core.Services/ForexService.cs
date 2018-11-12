using Dot.Net.Core.Common.DTO;
using Dot.Net.Core.Interfaces.IntegrationService;
using Dot.Net.Core.Interfaces.Service;
using System.Threading.Tasks;

namespace Dot.Net.Core.Services
{
    public class ForexService : IForexService
    {
        IForexIntegrationService _forexIntegrationService;

        public ForexService(IForexIntegrationService forexIntegrationService)
        {
            _forexIntegrationService = forexIntegrationService;
        }

        public async Task<ResponseCurrencyDTO> GetCurrencyConversion(RequestedCurrencyDTO currency)
        {
            return await _forexIntegrationService.GetCurrencyConversion(currency);
        }
    }
}
