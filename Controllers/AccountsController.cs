using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Helpers.Pagination;
using YallaNghani.Helpers.Results;
using YallaNghani.Services.Account;
using AccountDtos = YallaNghani.DTOs.Accounts;


namespace YallaNghani.Controllers
{
    enum Testy
    {
        val1, 
        val2
    }
    [Route("[controller]")]
    [ApiController]

    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsService;

        public AccountsController(IAccountsService accountsService)
        {

            _accountsService = accountsService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] AccountDtos.CreateDto dto)
        {
            return _mapResult(await _accountsService.Create(dto));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> Get([FromQuery] PaginationParameters paginationParameters)
        {
            Console.WriteLine(User.IsInRole("Parent"));

            Console.WriteLine(User.Identity.Name);

            //var id = User.Claims.Where()
            return _mapResult(await _accountsService.Get(paginationParameters));
        }

        [Authorize()]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (!User.IsInRole("Admin") && User.Identity.Name != id)
                return Forbid();

            return _mapResult(await _accountsService.GetById(id));
        }

        [Authorize()]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(string id, [FromBody] AccountDtos.UpdateProfileDto dto)
        {
            if (!User.IsInRole("Admin") && User.Identity.Name != id)
                return Forbid();

            return _mapResult(await _accountsService.UpdateProfile(id, dto));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return _mapResult(await _accountsService.Delete(id));
        }

        [Authorize]
        [HttpPost("updatepassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] AccountDtos.UpdatePasswordDto dto)
        {

            return _mapResult(await _accountsService.UpdatePassword(User.Identity.Name, dto));
        }

        [Authorize]
        [HttpPost("images/profiles")]
        public async Task<IActionResult> UpdateProfileImage(IFormFile formFile)
        {
            return _mapResult(await _accountsService.UpdateProfileImage(User.Identity.Name, formFile));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword(AccountDtos.ResetPasswordDto dto)
        {
            return _mapResult(await _accountsService.ResetPassowrd(dto));
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
