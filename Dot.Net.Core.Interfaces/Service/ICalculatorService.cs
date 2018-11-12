using Dot.Net.Core.Common.Enums;

namespace Dot.Net.Core.Interfaces.Service
{
    public interface ICalculatorService
    {
        int CalculateValue(GlobalEnums.OperatorsEnum operatorsEnum, int firstValue, int secondValue);
    }
}
