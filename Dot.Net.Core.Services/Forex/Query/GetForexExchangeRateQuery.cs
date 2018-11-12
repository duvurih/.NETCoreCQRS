using Dot.Net.Core.Common.DTO;
using MediatR;

namespace Dot.Net.Core.Services.Forex.Query
{
    public class GetForexExchangeRateQuery : IRequest<ForexExchangeModel>
    {
        public RequestedCurrencyDTO requestedCurrencyDTO { get; set; }
    }
}
