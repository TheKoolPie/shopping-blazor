using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopping.Server.Data;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Results;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Services.Implementations
{
    public class UserGroupRepository : IUserGroupRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly IDataRepository _data;

        public UserGroupRepository(IDataRepository data, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _data = data;
        }

        public async Task<List<UserGroup>> GetAllAsync()
        {
            return await _data.GetUserGroupsAsync();
        }

        public async Task<UserGroup> GetAsync(string id)
        {
            return await _data.GetUserGroupAsync(id);
        }

        public async Task<List<UserGroup>> GetAllOfUserAsync(string userId)
        {
            return (await GetAllAsync()).Where(i => UserIsInGroup(i, userId)).ToList();
        }

        public async Task<bool> UserIsInGroupAsync(string userGroupId, string userId)
        {
            var group = await GetAsync(userGroupId);

            return UserIsInGroup(group, userId);
        }
        private bool UserIsInGroup(UserGroup group, string userId)
        {
            bool isOwner = group.Owner.Id == userId;
            bool isMember = group.Members.Select(x => x.Id).Contains(userId);

            return isOwner || isMember;
        }

        public async Task<UserGroup> AddUserToGroup(string userGroupId, ShoppingUserModel user)
        {
            var group = await GetAsync(userGroupId);

            var existingUser = await _userRepository.GetUserAsync(user);
            if (existingUser == null)
            {
                throw new ItemNotFoundException($"No user with provided user data found");
            }

            if (group.Members.Any(m => m.Id == existingUser.Id))
            {
                throw new ItemAlreadyExistsException($"User with '{existingUser.UserName} already exists in group: '{group.Name}'");
            }

            group.Members.Add(new ShoppingUserModel()
            {
                Id = existingUser.Id
            });

            await _data.SaveChangesAsync();

            return group;
        }
        public async Task<UserGroup> RemoveUserFromGroup(string userGroupId, ShoppingUserModel user)
        {
            var group = await GetAsync(userGroupId);

            var existingUser = await _userRepository.GetUserAsync(user);
            if (existingUser == null)
            {
                throw new ItemNotFoundException($"No user with provided user data found");
            }
            var groupMember = group.Members.FirstOrDefault(m => m.Id == existingUser.Id);
            if (groupMember == null)
            {
                throw new ItemNotFoundException($"User not in group: '{group.Name}'");
            }

            if (!group.Members.Remove(groupMember))
            {
                throw new Exception("Could not remove member");
            }

            await _data.SaveChangesAsync();

            return group;

        }

        public async Task<List<ShoppingUserModel>> GetUsersInGroup(string userGroupId)
        {
            var group = await GetAsync(userGroupId);
            if (group == null)
            {
                throw new ItemNotFoundException(typeof(UserGroup), userGroupId);
            }
            List<ShoppingUserModel> users = new List<ShoppingUserModel>();
            foreach (var member in group.Members)
            {
                var userData = await _userRepository.GetUserAsync(member);
                users.Add(userData);
            }
            return users;
        }

        public async Task<List<UserGroup>> GetCommonGroupsAsync(string userOneId, string userTwoId)
        {
            List<UserGroup> commonLists = new List<UserGroup>();

            var allGroups = await GetAllOfUserAsync(userOneId);
            if (allGroups.Count > 0)
            {
                commonLists
                    .Where(g => g.Owner.Id == userTwoId || g.Members.Select(x => x.Id).Contains(userTwoId))
                    .ToList();
            }
            return commonLists;
        }

        public Task<UserGroup> CreateAsync(UserGroup item)
        {
            return _data.CreateUserGroupAsync(item);
        }

        public Task<UserGroup> UpdateAsync(string id, UserGroup item)
        {
            return _data.UpdateUserGroupAsync(id, item);
        }

        public Task<bool> DeleteByIdAsync(string id)
        {
            return _data.DeleteUserGroupAsync(id);
        }
    }
}
