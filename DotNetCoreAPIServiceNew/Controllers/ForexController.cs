using Dot.Net.Core.Common.DTO;
using Dot.Net.Core.Interfaces.Service;
using Dot.Net.Core.Services.Forex.Query;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DotNetCoreAPIServicesNew.Controllers
{
    [Produces("application/json")]
    [Route("api/Forex")]
    public class ForexController : BaseController
    {
        private IForexService _forexService;

        public ForexController(IForexService forexService)
        {
            _forexService = forexService;
        }

        [HttpGet]
        [Route("CurrencyConversion/{baseCurrency}/{targetCurrency}/{amount}")]
        public async Task<ResponseCurrencyDTO> GetCurrencyConversion(string baseCurrency, string targetCurrency, double amount)
        {
            RequestedCurrencyDTO currency = new RequestedCurrencyDTO
            {
                baseCurrency = baseCurrency,
                targetCurrency = targetCurrency,
                amount = amount,
                latestExchangeRate = true
            };
            var result = await _forexService.GetCurrencyConversion(currency);
            return result;
        }

        [HttpGet]
        [Route("GetExchangeRate/{baseCurrency}/{targetCurrency}/{amount}")]
        public async Task<IActionResult> GetExchangeRate(string baseCurrency, string targetCurrency, double amount)
        {
            RequestedCurrencyDTO currency = new RequestedCurrencyDTO
            {
                baseCurrency = baseCurrency,
                targetCurrency = targetCurrency,
                amount = amount,
                latestExchangeRate = true
            };
            var command = new GetForexExchangeRateQuery { requestedCurrencyDTO = currency };
            var response = await Mediator.Send(new GetForexExchangeRateQuery { requestedCurrencyDTO = currency });
            return Ok(response);
        }
    }
}