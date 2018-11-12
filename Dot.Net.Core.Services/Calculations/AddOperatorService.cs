using Dot.Net.Core.Interfaces.Service;

namespace Dot.Net.Core.Services
{
    public class AddOperatorService : ICalculateOperator
    {
        public int PerformOperation(int firstValue, int secondValue)
        {
            return secondValue + firstValue;
        }
    }
}
