using Dot.Net.Core.Interfaces.Service;

namespace Dot.Net.Core.Services
{
    public class SubtractOperatorService : ICalculateOperator
    {
        public int PerformOperation(int firstValue, int secondValue)
        {
            return (secondValue - firstValue);
        }
    }
}
