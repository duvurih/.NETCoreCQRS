using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ConsoleApp1
{
    public class CalculateService
    {
        private readonly HttpClient client = new HttpClient();

        public void CalculatorService()
        {
            Console.WriteLine("Calculate value based on selected operator!");


            //Reading values from user input
            Console.WriteLine("Select Operator ");
            Console.WriteLine("1. Add ");
            Console.WriteLine("2. Subtract ");
            Console.WriteLine("3. Multiply ");
            Console.WriteLine("4. Divide ");
            var operatorValue = Console.ReadLine();
            Console.WriteLine("Enter First Value ");
            var firstValue = Console.ReadLine();
            Console.WriteLine("Enter Second Value ");
            var secondValue = Console.ReadLine();


            //Result
            string msg = ConnectToCalculateService(operatorValue, firstValue, secondValue);
            Console.Write("Result - ");
            Console.Write(msg);
            Console.ReadLine();
        }

        private string ConnectToCalculateService(string operatorValue, string firstValue, string secondValue)
        {
            //making service call using HttpClient
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            var stringTask = client.GetStringAsync("http://localhost:62004/api/Calculator/GetCalculatorResult/" + operatorValue + "/" + firstValue + "/" + secondValue);
            var result = stringTask.Result;
            return result;
        }

    }
}
