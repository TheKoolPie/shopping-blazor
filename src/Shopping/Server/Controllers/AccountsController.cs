using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Shopping.Server.Models;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Results;
using Shopping.Shared.Services;

namespace Shopping.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AccountsController(IAuthService authService)
        {
            _authService = authService;
        }
        [Authorize(Roles = ShoppingUserRoles.Admin)]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var registerResult = await _authService.Register(model);

            return Ok(registerResult);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var loginResult = await _authService.Login(model);

            return Ok(loginResult);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();

            return Ok();
        }
    }
}
