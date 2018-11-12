using Dot.Net.Core.Common.Enums;

namespace Dot.Net.Core.Interfaces.Repository
{
    public interface ICalculatorRepository
    {
        int Calculate(GlobalEnums.OperatorsEnum operatorValue, int result);
    }
}
