using Dot.Net.Core.Common.DTO;
using Dot.Net.Core.Common.Settings;
using Dot.Net.Core.Common.Utils;
using Dot.Net.Core.Interfaces.IntegrationService;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dot.Net.Infrastructure.Integrations
{
    public class ForexIntegrationService : IForexIntegrationService
    {
        private IManageApi _apiManager;
        IOptions<AppSettingsConfig> _appSettings;

        public ForexIntegrationService(IManageApi apiManager, IOptions<AppSettingsConfig> appSettings)
        {
            _apiManager = apiManager;
            _appSettings = appSettings;
        }

        /// <summary>
        /// Returns the Exchange Rate based on the Information provided
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public async Task<ResponseCurrencyDTO> GetCurrencyConversion(RequestedCurrencyDTO currency)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("access_key", _appSettings.Value.ForexApiKey);
            ResponseConvertDTO result;
            if (currency.latestExchangeRate == true)
            {
                result = await _apiManager.GetSynch<ResponseConvertDTO>(_appSettings.Value.ForexService + "latest?", string.Empty, keyValuePairs, true);
            }
            else
            {
                keyValuePairs.Add("from", currency.baseCurrency);
                keyValuePairs.Add("to", currency.targetCurrency);
                keyValuePairs.Add("amount", currency.amount.ToString());
                result = await _apiManager.GetSynch<ResponseConvertDTO>(_appSettings.Value.ForexService + "convert?", string.Empty, keyValuePairs, true);
            }
            if (result.Rates is null)
            {
                throw new Exception("Response is Null. Check the end point permission");
            }
            //result.Info.TryGetValue("timestamp", out resultTimestamp);
            var exchangeRate = result.Rates.Single(item => item.Key == currency.targetCurrency);
            return new ResponseCurrencyDTO()
            {
                baseCurrency = currency.baseCurrency,
                targetCurrency = currency.targetCurrency,
                exchangeRate = Convert.ToDouble(exchangeRate.Value),
                timestamp = result.Timestamp
            };

        }
    }
}
