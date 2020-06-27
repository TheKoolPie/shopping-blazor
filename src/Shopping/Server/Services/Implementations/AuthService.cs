using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Shopping.Server.Models;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Results;
using Shopping.Shared.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Server.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ShoppingUser> _userManager;
        private readonly SignInManager<ShoppingUser> _signInManager;
        private readonly ILogger<AuthService> _logger;
        public AuthService(IConfiguration configuration,
            UserManager<ShoppingUser> userManager,
            SignInManager<ShoppingUser> signInManager,
            ILogger<AuthService> logger)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public Task<HttpClient> GetHttpClientAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResult> Login(LoginModel model)
        {
            LoginResult result = new LoginResult();

            var user = await _userManager.FindByNameAsync(model.LoginName);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(model.LoginName);
                if (user == null)
                {
                    return new LoginResult
                    {
                        IsSuccessful = false,
                        ErrorMessages = new List<string> { $"Could not find user with name or email of value: {model.LoginName}" }
                    };
                }
            }

            var loginResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if (!loginResult.Succeeded)
            {
                return new LoginResult
                {
                    IsSuccessful = false,
                    ErrorMessages = new List<string> { $"Username and password combination is invalid." }
                };
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

            return new LoginResult { IsSuccessful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) };
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<RegisterResult> Register(RegisterModel model)
        {
            RegisterResult result = new RegisterResult();

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
                return new RegisterResult
                {
                    IsSuccessful = false,
                    ErrorMessages = new List<string>
                    {
                        $"User with user name {model.UserName} already exists"
                    }
                };
            }

            existing = await _userManager.FindByEmailAsync(model.Email);
            if (existing != null)
            {
                return new RegisterResult
                {
                    IsSuccessful = false,
                    ErrorMessages = new List<string>
                    {
                        $"User with email {model.Email} already exists"
                    }
                };
            }

            var createResult = await _userManager.CreateAsync(newUser, model.Password);
            if (!createResult.Succeeded)
            {
                return new RegisterResult
                {
                    IsSuccessful = false,
                    ErrorMessages = createResult.Errors.Select(x => x.Description).ToList()
                };
            }

            existing = await _userManager.FindByNameAsync(newUser.UserName);
            var roleResult = await _userManager.AddToRoleAsync(existing, ShoppingUserRoles.User);
            if (!roleResult.Succeeded)
            {
                _logger.LogWarning($"Could not add user {existing.UserName} to role {ShoppingUserRoles.User}");
            }

            return new RegisterResult { IsSuccessful = true };
        }

        public async Task<ChangePasswordResult> ChangePassword(ChangePasswordModel model)
        {
            if (string.IsNullOrEmpty(model.UserId))
            {
                return new ChangePasswordResult
                {
                    IsSuccessful = false,
                    ErrorMessages = new List<string> { "No user id provided" }
                };
            }

            var existingUser = await _userManager.FindByIdAsync(model.UserId);
            if (existingUser == null)
            {
                return new ChangePasswordResult
                {
                    IsSuccessful = false,
                    ErrorMessages = new List<string> { $"Could not find user with id {model.UserId}" }
                };
            }

            var changePwResult = await _userManager.ChangePasswordAsync(existingUser, model.CurrentPassword, model.NewPassword);
            if (!changePwResult.Succeeded)
            {
                return new ChangePasswordResult
                {
                    IsSuccessful = false,
                    ErrorMessages = changePwResult.Errors.Select(e => e.Description).ToList()
                };
            }

            return new ChangePasswordResult { IsSuccessful = true };
        }
    }
}
