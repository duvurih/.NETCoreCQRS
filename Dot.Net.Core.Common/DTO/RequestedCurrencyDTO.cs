namespace Dot.Net.Core.Common.DTO
{
    public class RequestedCurrencyDTO
    {
        public string baseCurrency { get; set; }
        public string targetCurrency { get; set; }
        public double amount { get; set; }
        public bool latestExchangeRate { get; set; }
    }
}
