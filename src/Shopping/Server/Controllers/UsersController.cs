using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Results;
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
    }
}
