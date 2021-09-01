using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApiRestFull.Business;
using ApiRestFull.Data.VO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRestFull.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PessoaController : ControllerBase
    {
        private readonly ILogger<PessoaController> _logger;
        private IPessoaBusiness _pessoaBusiness;

        public PessoaController(ILogger<PessoaController> logger, IPessoaBusiness pessoaBusiness)
        {
            _logger = logger;
            _pessoaBusiness = pessoaBusiness;

        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_pessoaBusiness.FindAll());
        }  
        
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var pessoa = _pessoaBusiness.FindById(id);

            if (pessoa == null) return NotFound();

            return Ok(pessoa);
        }        
        [HttpPost]
        public IActionResult Post([FromBody] PessoaVO pessoa)
        {
            if (pessoa == null) return BadRequest();

            return Ok(_pessoaBusiness.Create(pessoa));
        }

        [HttpPut]
        public IActionResult Put([FromBody] PessoaVO pessoa)
        {
            if (pessoa == null) return BadRequest();

            return Ok(_pessoaBusiness.Update(pessoa));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _pessoaBusiness.Delete(id);

            return NoContent();
        }






    }
}
