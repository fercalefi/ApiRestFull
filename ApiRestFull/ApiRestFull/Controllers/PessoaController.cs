using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApiRestFull.Business;
using ApiRestFull.Data.VO;
using ApiRestFull.Hypermedia.Filters;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRestFull.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Authorize("Bearer")]
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
        // nottations swagger
        [ProducesResponseType((200), Type = typeof(List<PessoaVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_pessoaBusiness.FindAll());
        }  
        
        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(PessoaVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var pessoa = _pessoaBusiness.FindById(id);

            if (pessoa == null) return NotFound();

            return Ok(pessoa);
        } 
        
        [HttpPost]
        [ProducesResponseType((200), Type = typeof(PessoaVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PessoaVO pessoa)
        {
            if (pessoa == null) return BadRequest();

            return Ok(_pessoaBusiness.Create(pessoa));
        }

        [HttpPut]
        [ProducesResponseType((200), Type = typeof(PessoaVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] PessoaVO pessoa)
        {
            if (pessoa == null) return BadRequest();

            return Ok(_pessoaBusiness.Update(pessoa));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType((200), Type = typeof(PessoaVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Patch(long id)
        {
            var pessoa = _pessoaBusiness.Disable(id);
            return Ok(pessoa);
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _pessoaBusiness.Delete(id);

            return NoContent();
        }






    }
}
