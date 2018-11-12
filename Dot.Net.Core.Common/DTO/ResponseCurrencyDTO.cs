namespace Dot.Net.Core.Common.DTO
{
    public class ResponseCurrencyDTO
    {
        public string baseCurrency { get; set; }
        public string targetCurrency { get; set; }
        public double exchangeRate { get; set; }
        public string timestamp { get; set; }
    }
}
