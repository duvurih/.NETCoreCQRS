using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ForexConversionService
    {
        private readonly HttpClient client = new HttpClient();

        /// <summary>
        /// Calling the Calculator Service
        /// </summary>
        /// <param name="baseCurrency"></param>
        /// <param name="targetCurrency"></param>
        public void CalculatorService(string baseCurrency, string targetCurrency)
        {
            double amount = 100;
            Console.WriteLine("Enter to call the Forex Service");
            Console.ReadLine();
            var result = ConnectToForexService(baseCurrency, targetCurrency, amount);
            Console.Write("Result of Exchange Rate");
            Console.Write(result);
            Console.ReadLine();
        }

        /// <summary>
        /// Calling the RESTful API to retrieve the response of exchange rate
        /// </summary>
        /// <param name="baseCurrency"></param>
        /// <param name="targetCurrency"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private string ConnectToForexService(string baseCurrency, string targetCurrency, double amount)
        {
            //making service call using HttpClient
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            var stringTask = client.GetStringAsync("http://localhost:62004/api/Forex/CurrencyConversion/" + baseCurrency + "/" + targetCurrency + "/" + amount).GetAwaiter().GetResult();
            if (stringTask is null)
            {
                throw new Exception("Response is Null. Check the end point permission");
            }
            var result = stringTask;
            return result;
        }

        public void GetExchangeRateConversionService(string baseCurrency, string targetCurrency)
        {
            double amount = 100;
            Console.WriteLine("Enter to call the Forex Service");
            Console.ReadLine();
            var result = GetExchangeRateService(baseCurrency, targetCurrency, amount);
            Console.Write("Result of Exchange Rate");
            Console.Write(result);
            Console.ReadLine();
        }


        private async Task<string> GetExchangeRateService(string baseCurrency, string targetCurrency, double amount)
        {
            //making service call using HttpClient
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            var stringTask = await client.GetStringAsync("http://localhost:62004/api/Forex/GetExchangeRate/" + baseCurrency + "/" + targetCurrency + "/" + amount);
            if (stringTask is null)
            {
                throw new Exception("Response is Null. Check the end point permission");
            }
            return stringTask;
        }
    }
}
