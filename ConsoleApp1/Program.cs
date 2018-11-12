namespace ConsoleApp1
{
    class Program
    {


        static void Main(string[] args)
        {
            //Calling Forex Service
            ForexConversionService forex = new ForexConversionService();
            //forex.CalculatorService("EUR", "AUD");
            forex.GetExchangeRateConversionService("EUR", "AUD");

            //Calling Calculate Service
            CalculateService calService = new CalculateService();
            calService.CalculatorService();
        }





    }
}
