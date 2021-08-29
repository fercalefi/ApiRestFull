﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRestFull.Controllers.Model;
using ApiRestFull.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiRestFull.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly ILogger<PessoaController> _logger;
        private IPessoaService _pessoaService;

        public PessoaController(ILogger<PessoaController> logger, IPessoaService pessoaService)
        {
            _logger = logger;
            _pessoaService = pessoaService;

        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_pessoaService.FindAll());
        }  
        
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var pessoa = _pessoaService.FindByYd(id);

            if (pessoa == null) return NotFound();

            return Ok(pessoa);
        }        
        [HttpPost]
        public IActionResult Post([FromBody] Pessoa pessoa)
        {
            if (pessoa == null) return BadRequest();

            return Ok(_pessoaService.Create(pessoa));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Pessoa pessoa)
        {
            if (pessoa == null) return BadRequest();

            return Ok(_pessoaService.Update(pessoa));
        }
        [HttpDelete("{id}")]
        public IActionResult Put(long id)
        {
            _pessoaService.Delete(id);

            return NoContent();
        }






    }
}
