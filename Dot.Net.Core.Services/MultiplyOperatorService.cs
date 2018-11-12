using Dot.Net.Core.Interfaces.Service;

namespace Dot.Net.Core.Services
{
    public class MultiplyOperatorService : ICalculateOperator
    {
        public int PerformOperation(int firstValue, int secondValue)
        {
            return (firstValue * secondValue);
        }
    }
}
