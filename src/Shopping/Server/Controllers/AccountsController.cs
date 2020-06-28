using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<AccountsController> _logger;
        private readonly IUserGroupRepository _userGroupRepo;
        private readonly IShoppingLists _shoppingListRepo;
        public AccountsController(IAuthService authService, ICurrentUserProvider currentUser, 
            ILogger<AccountsController> logger, IUserGroupRepository userGroupRepo, IShoppingLists shoppingListRepo)
        {
            _authService = authService;
            _currentUser = currentUser;
            _logger = logger;
            _userGroupRepo = userGroupRepo;
            _shoppingListRepo = shoppingListRepo;
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteUserResult>> DeleteUser(string id)
        {
            var currentUser = await _currentUser.GetUserAsync();
            var isAdmin = await _currentUser.IsUserAdminAsync();

            DeleteUserResult result = new DeleteUserResult();
            if (!(currentUser.Id == id || isAdmin))
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add("Not authorized");
                return Unauthorized(result);
            }

            await _userGroupRepo.DeleteAllOfUser(id);
            await _userGroupRepo.RemoveUserFromAllGroups(id);
            await _shoppingListRepo.DeleteAllOfUser(id);
            

            result = await _authService.DeleteUser(new DeleteUserModel { UserId = id });

            return Ok(result);
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _authService.Logout();
                _logger.LogInformation("User logged out");
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
            }


            return Ok();
        }
    }
}
