using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRestFull.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly ILogger<PessoaController> _logger;
        public PessoaController(ILogger<PessoaController> logger)
        {
            _logger = logger;
        }
        // GET: /<controller>/
        [HttpGet("soma/{firstnumber}/{secondnumber}")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
