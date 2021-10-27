using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Helpers.Results;
using YallaNghani.Services.Auth;
using AuthDtos = YallaNghani.DTOs.Auth;

namespace YallaNghani.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(AuthDtos.LoginDto dto)
        {
            return _mapResult(await _authService.Login(dto));
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> AdminLogin(AuthDtos.LoginDto dto)
        {
            return _mapResult(await _authService.AdminLogin(dto));
        }

        private IActionResult _mapResult(ServiceResult result)
        {
            if (result.StatusCode == 200)
                return Ok(result);

            else if (result.StatusCode == 400)
                return BadRequest(result);

            else if (result.StatusCode == 404)
                return NotFound(result);

            else if (result.StatusCode == 403)
                return Forbid();

            return BadRequest(result);
        }
    }
}
