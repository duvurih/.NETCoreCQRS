using Dot.Net.Core.Common.Enums;
using Dot.Net.Core.Interfaces.Repository;
using Dot.Net.Core.Interfaces.Service;
using System;

namespace Dot.Net.Core.Services
{
    public class CalculatorService : ICalculatorService
    {
        private ICalculatorRepository _iCalculatorRepository;
        private readonly Func<GlobalEnums.OperatorsEnum, ICalculateOperator> _serviceAccessor;

        public CalculatorService(
            ICalculatorRepository iCalculatorRepository,
            Func<GlobalEnums.OperatorsEnum, ICalculateOperator> serviceAccessor
        )
        {
            _iCalculatorRepository = iCalculatorRepository;
            _serviceAccessor = serviceAccessor;
        }

        public int CalculateValue(GlobalEnums.OperatorsEnum operatorsEnum, int firstValue, int secondValue)
        {
            int result = _serviceAccessor(operatorsEnum).PerformOperation(firstValue, secondValue);
            _iCalculatorRepository.Calculate(operatorsEnum, result);
            return result;
        }
    }
}
