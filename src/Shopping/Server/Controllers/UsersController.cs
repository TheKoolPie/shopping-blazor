using System.Collections.Generic;
using System.Composition;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shopping.Server.Models;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Results;
using Shopping.Shared.Results.Account;
using Shopping.Shared.Services.Interfaces;

namespace Shopping.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUserRepository userRepository, ICurrentUserProvider currentUserProvider,
            IUserGroupRepository userGroupRepository, ILogger<UsersController> logger)
        {
            _userRepository = userRepository;
            _currentUserProvider = currentUserProvider;
            _userGroupRepository = userGroupRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ShoppingUserResult>> GetCurrentUser()
        {
            var currentUser = await _currentUserProvider.GetUserAsync();
            var result = new ShoppingUserResult()
            {
                IsSuccessful = true,
                ResultData = new List<ShoppingUserModel>() { currentUser }
            };
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingUserResult>> GetByIdAsync(string id)
        {
            ShoppingUserResult result = new ShoppingUserResult();

            var currentUser = await _currentUserProvider.GetUserAsync();
            var dbUser = await _userRepository.GetUserAsync(new ShoppingUserModel() { Id = id });
            if (dbUser == null)
            {
                _logger.LogDebug($"No user found with id {id}");
                result.IsSuccessful = false;
                result.ErrorMessages.Add("Not authorized");
                return Unauthorized(result);
            }

            if (currentUser.Id != id)
            {
                var groupsInCommon = await _userGroupRepository.GetCommonGroupsAsync(currentUser.Id, id);
                if (groupsInCommon.Count == 0)
                {
                    result.IsSuccessful = false;
                    result.ErrorMessages.Add("Not authorized");
                    return Unauthorized(result);
                }
            }

            result.IsSuccessful = true;
            result.ResultData = new List<ShoppingUserModel>() { dbUser };
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ShoppingUserResult>> UpdateUserData(string id, [FromBody] ShoppingUserModel updatedData)
        {
            ShoppingUserResult result = new ShoppingUserResult();

            if (id != updatedData.Id)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add($"Ids do not match");
                return BadRequest(result);
            }
            if (!(await IsUserAuthorized(id)))
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add("Not authorized to access this resource");
                return Unauthorized(result);
            }

            var updateResult = await _userRepository.UpdateUserData(id, updatedData);
            if (updateResult == null)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add("Could not update user data");
                return NotFound(result);
            }



            result.IsSuccessful = true;
            result.ResultData.Add(updateResult);

            return Ok(result);
        }
        [HttpPut("UpdateUserSettings/{id}")]
        public async Task<ActionResult<UpdateUserSettingsResult>> UpdateUserSettings(string id, [FromBody] ShoppingUserSettingsModel settings)
        {
            UpdateUserSettingsResult result = new UpdateUserSettingsResult();
            if (!(await IsUserAuthorized(id)))
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add("Not authorized to access this resource");
                return Unauthorized(result);
            }
            var updateResult = await _userRepository.UpdateUserSettingsAsync(id, settings);
            if (updateResult == null)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add("Could not update user settings");
                return NotFound(result);
            }

            return Ok(result);
        }

        private async Task<bool> IsUserAuthorized(string targetUserId)
        {
            var currentUser = await _currentUserProvider.GetUserAsync();
            bool IsCurrentUserAdmin = await _currentUserProvider.IsUserAdminAsync();
            return currentUser.Id == targetUserId || IsCurrentUserAdmin;
        }
    }
}
