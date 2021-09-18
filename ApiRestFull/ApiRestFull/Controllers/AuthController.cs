using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRestFull.Business;
using ApiRestFull.Data.VO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestFull.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILoginBusiness _loginbusiness;

        public AuthController(ILoginBusiness loginbusiness)
        {
            _loginbusiness = loginbusiness;
        }

        // definindo metodo de login
        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserVO user)
        {
            if (user == null) return BadRequest("Requisição invalida");

            var token = _loginbusiness.ValidateCredentials(user);

            if (token == null) return Unauthorized();

            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVO)
        {
            if (tokenVO == null) return BadRequest("Requisição invalida");

            var token = _loginbusiness.ValidateCredentials(tokenVO);

            if (token == null) return BadRequest("Requisição inválida.");

            return Ok(token);
        }

        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            var userName = User.Identity.Name;
            var result = _loginbusiness.RevokeToken(userName);

            if (!result) return BadRequest("Requisição invalida");

            return NoContent();
        }
    }
}