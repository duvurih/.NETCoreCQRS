using Dot.Net.Core.Common.DTO;
using System;
using System.Linq.Expressions;

namespace Dot.Net.Core.Services.Forex.Query
{
    public class ForexExchangeModel
    {
        public string baseCurrency { get; set; }
        public string targetCurrency { get; set; }
        public double exchangeRate { get; set; }
        public string timestamp { get; set; }


        public static Expression<Func<ResponseCurrencyDTO, ForexExchangeModel>> Projection
        {
            get
            {
                return exchange => new ForexExchangeModel
                {
                    baseCurrency = exchange.baseCurrency,
                    targetCurrency = exchange.targetCurrency,
                    exchangeRate = exchange.exchangeRate,
                    timestamp = exchange.timestamp
                };
            }
        }

        public static ForexExchangeModel Create(ResponseCurrencyDTO exchange)
        {
            return Projection.Compile().Invoke(exchange);
        }
    }
}
