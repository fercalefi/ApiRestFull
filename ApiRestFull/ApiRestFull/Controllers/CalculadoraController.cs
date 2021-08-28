using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiRestFull.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculadoraController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CalculadoraController> _logger;

        public CalculadoraController(ILogger<CalculadoraController> logger)
        {
            _logger = logger;
        }

        //path params
        [HttpGet("soma/{firstnumber}/{secondnumber}")]
        public IActionResult Soma(string firstNumber, string secondNumber)
        {

            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }

            return BadRequest("Dados inválidos");
        }

        [HttpGet("subtracao/{firstnumber}/{secondnumber}")]
        public IActionResult Subtracao(string firstNumber, string secondNumber)
        {

            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var conta = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);
                return Ok(conta.ToString());
            }

            return BadRequest("Dados inválidos");
        }

        [HttpGet("multiplicacao/{firstnumber}/{secondnumber}")]
        public IActionResult Multiplicacao(string firstNumber, string secondNumber)
        {

            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var conta = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);
                return Ok(conta.ToString());
            }

            return BadRequest("Dados inválidos");
        }

        [HttpGet("divisao/{firstnumber}/{secondnumber}")]
        public IActionResult Divisao(string firstNumber, string secondNumber)
        {

            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                if (ConvertToDecimal(secondNumber) == 0)
                {
                    return BadRequest("Não é permitido divisão por zero.");
                }
                var conta = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);
                return Ok(conta.ToString());
            }

            return BadRequest("Dados inválidos");
        }

        private bool IsNumeric(string strNumber)
        {
            double number;
            bool isNumber = double.TryParse(
                strNumber,
                System.Globalization.NumberStyles.Any, 
                System.Globalization.NumberFormatInfo.InvariantInfo, 
                out number);

            return isNumber;
            
        }

        private decimal ConvertToDecimal(string strNumber)
        {
            decimal decimalValue;
            if (decimal.TryParse(strNumber, out decimalValue))
            {
                return decimalValue;
            }
            return 0;
        }

     
    }
}
