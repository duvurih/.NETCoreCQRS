using Dot.Net.Core.Interfaces.IntegrationService;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Dot.Net.Core.Services.Forex.Query
{
    public class GetForexExchangeRateQueryHandler : IRequestHandler<GetForexExchangeRateQuery, ForexExchangeModel>
    {
        IForexIntegrationService _forexIntegrationService;

        public GetForexExchangeRateQueryHandler(IForexIntegrationService forexIntegrationService)
        {
            _forexIntegrationService = forexIntegrationService;
        }

        public async Task<ForexExchangeModel> Handle(GetForexExchangeRateQuery request, CancellationToken cancellationToken)
        {
            var response = await _forexIntegrationService.GetCurrencyConversion(request.requestedCurrencyDTO);
            return new ForexExchangeModel
            {
                baseCurrency = response.baseCurrency,
                targetCurrency = response.targetCurrency,
                exchangeRate = response.exchangeRate,
                timestamp = response.timestamp
            };
        }
    }
}
