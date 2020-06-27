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
using Shopping.Shared.Services.Interfaces;

namespace Shopping.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ICurrentUserProvider _currentUser;
        public AccountsController(IAuthService authService, ICurrentUserProvider currentUser)
        {
            _authService = authService;
            _currentUser = currentUser;
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

        [HttpPost("ChangePassword")]
        public async Task<ActionResult<ChangePasswordResult>> ChangePassword([FromBody] ChangePasswordModel model)
        {
            var currentUser = await _currentUser.GetUserAsync();
            var isAdmin = await _currentUser.IsUserAdminAsync();

            ChangePasswordResult result = new ChangePasswordResult();

            if(!(currentUser.Id == model.UserId || isAdmin))
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add("Not authorized");
                return Unauthorized(result);
            }

            result = await _authService.ChangePassword(model);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();

            return Ok();
        }
    }
}
