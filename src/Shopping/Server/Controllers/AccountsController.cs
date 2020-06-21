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

namespace Shopping.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ShoppingUser> _userManager;
        private readonly SignInManager<ShoppingUser> _signInManager;
        private readonly ILogger<AccountsController> _logger;
        public AccountsController(IConfiguration configuration,
            UserManager<ShoppingUser> userManager,
            SignInManager<ShoppingUser> signInManager,
            ILogger<AccountsController> logger)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        [Authorize(Roles = ShoppingUserRoles.Admin)]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var newUser = new ShoppingUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var existing = await _userManager.FindByNameAsync(model.UserName);
            if (existing != null)
            {
                return Ok(new RegisterResult
                {
                    IsSuccessful = false,
                    ErrorMessages = new List<string>
                    {
                        $"User with user name {model.UserName} already exists"
                    }
                });
            }

            existing = await _userManager.FindByEmailAsync(model.Email);
            if (existing != null)
            {
                return Ok(new RegisterResult
                {
                    IsSuccessful = false,
                    ErrorMessages = new List<string>
                    {
                        $"User with email {model.Email} already exists"
                    }
                });
            }

            var createResult = await _userManager.CreateAsync(newUser, model.Password);
            if (!createResult.Succeeded)
            {
                return Ok(new RegisterResult
                {
                    IsSuccessful = false,
                    ErrorMessages = createResult.Errors.Select(x => x.Description).ToList()
                });
            }

            existing = await _userManager.FindByNameAsync(newUser.UserName);
            var roleResult = await _userManager.AddToRoleAsync(existing, ShoppingUserRoles.User);
            if (!roleResult.Succeeded)
            {
                _logger.LogWarning($"Could not add user {existing.UserName} to role {ShoppingUserRoles.User}");
            }

            return Ok(new RegisterResult { IsSuccessful = true });
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.LoginName);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(model.LoginName);
                if (user == null)
                {
                    return BadRequest(
                        new LoginResult
                        {
                            IsSuccessful = false,
                            ErrorMessages = new List<string> { $"Could not find user with name or email of value: {model.LoginName}" }
                        });
                }
            }

            var loginResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if (!loginResult.Succeeded)
            {
                return BadRequest(
                    new LoginResult
                    {
                        IsSuccessful = false,
                        ErrorMessages = new List<string> { $"Username and password combination is invalid." }
                    });
            }

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, model.LoginName));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );
            return Ok(new LoginResult { IsSuccessful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
