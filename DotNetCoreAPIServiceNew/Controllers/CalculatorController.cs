using Dot.Net.Core.Common.Enums;
using Dot.Net.Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreAPIServicesNew.Controllers
{
    [Produces("application/json")]
    [Route("api/Calculator")]
    public class CalculatorController : Controller
    {
        private ICalculatorService _iCalculatorService;

        public CalculatorController(ICalculatorService iCalculatorService)
        {
            _iCalculatorService = iCalculatorService;
        }

        [HttpGet]
        [Route("GetCalculatorResult/{operatorsEnum}/{firstValue}/{secondValue}")]
        public int Get(GlobalEnums.OperatorsEnum operatorsEnum, int firstValue, int secondValue)
        {
            return _iCalculatorService.CalculateValue(operatorsEnum, firstValue, secondValue);
        }
    }
}