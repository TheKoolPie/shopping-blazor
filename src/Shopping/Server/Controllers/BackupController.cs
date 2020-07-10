using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Model.Serialization;
using Shopping.Shared.Results;
using Shopping.Shared.Services.Interfaces;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shopping.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = ShoppingUserPolicies.IsDatabaseManager)]
    public class BackupController : ControllerBase
    {
        private readonly ILogger<BackupController> _logger;
        private readonly IShoppingRepoBackup _backup;
        private readonly ICurrentUserProvider _currentUser;
        private readonly IUserRepository _users;

        public BackupController(IShoppingRepoBackup backup, ICurrentUserProvider currentUser,
            IUserRepository users, ILogger<BackupController> logger)
        {
            _backup = backup;
            _currentUser = currentUser;
            _users = users;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<DatabaseBackupResult>> ImportData([FromBody] ShoppingDataSerializationModel model)
        {
            DatabaseBackupResult result = new DatabaseBackupResult();

            var fallbackUser = await _currentUser.GetUserAsync();

            _logger.LogInformation($"Read model information");

            _logger.LogInformation("Check user data in user groups");
            foreach (var group in model.UserGroups)
            {
                var owner = await _users.GetUserByIdAsync(group.OwnerId);
                if (owner == null)
                {
                    _logger.LogInformation($"Replace owner in group: {group.Id}");
                    group.OwnerId = fallbackUser.Id;
                    group.Owner = fallbackUser;
                }
                for (int i = group.Members.Count - 1; i > 0; i--)
                {
                    string memberId = group.Members[i].Id;
                    var existing = await _users.GetUserByIdAsync(memberId);
                    if (existing == null)
                    {
                        _logger.LogInformation($"Remove user '{memberId}' from group '{group.Id}'");
                        group.Members.RemoveAt(i);
                    }
                }
            }
            _logger.LogInformation("Check user data in shopping lists");
            foreach (var list in model.ShoppingLists)
            {
                var owner = await _users.GetUserByIdAsync(list.OwnerId);
                if (owner == null)
                {
                    _logger.LogInformation($"Replace owner in list: {list.Id}");
                    list.OwnerId = fallbackUser.Id;
                    list.Owner = fallbackUser;
                }
            }

            _logger.LogInformation("Perform import");

            try
            {
                await _backup.ImportDataAsync(model);
            }
            catch (PersistencyException e)
            {
                result.IsSuccessful = false;
                result.ErrorMessages.Add(e.Message);
                return Conflict(result);
            }

            result.IsSuccessful = true;

            return Ok(result);
        }
    }
}
