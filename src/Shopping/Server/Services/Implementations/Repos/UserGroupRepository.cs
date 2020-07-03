using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Shopping.Shared.Data;
using Shopping.Shared.Data.Abstractions;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Model.Account;
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
        private readonly IShoppingDataRepository _context;
        private readonly IUserGroupShoppingLists _groupListAssignments;

        public UserGroupRepository(IShoppingDataRepository context, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public async Task<List<UserGroup>> GetAllAsync()
        {
            var groups = await _context.UserGroups.ToListAsync();
            return groups;
        }

        public async Task<UserGroup> GetAsync(string id)
        {
            var group = await _context.UserGroups.FirstOrDefaultAsync(i => i.Id == id);
            if (group == null)
            {
                throw new ItemNotFoundException(typeof(UserGroup), id);
            }
            return group;
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

            await _context.SaveChangesAsync();

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

            await _context.SaveChangesAsync();

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

        public async Task<UserGroup> CreateAsync(UserGroup item)
        {
            if (ItemAlreadyExists(item))
            {
                throw new ItemAlreadyExistsException(typeof(UserGroup), item.Id);
            }

            _context.UserGroups.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<UserGroup> UpdateAsync(string id, UserGroup item)
        {
            if (!ItemCanBeUpdated(item))
            {
                throw new ItemAlreadyExistsException(typeof(UserGroup), item.Id);
            }
            var existing = await GetAsync(id);
            existing.Name = item.Name;
            existing.Owner = item.Owner;
            existing.Members = new List<ShoppingUserModel>(item.Members);

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var existing = await GetAsync(id);

            await RemoveAssignmentsOfGroupAsync(id);

            _context.UserGroups.Remove(existing);

            bool result = false;
            try
            {
                await _context.SaveChangesAsync();
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public async Task<bool> DeleteAllOfUser(string userId)
        {
            var all = await GetAllAsync();
            var owning = all.Where(g => g.Owner.Id == userId)
                .Select(u => u.Id)
                .ToList();

            foreach (var groupId in owning)
            {
                if (!await DeleteByIdAsync(groupId))
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> RemoveUserFromAllGroups(string userId)
        {
            var all = await GetAllAsync();
            var groupsWhereUserIsMember = all.Where(g => g.Owner.Id != userId && g.Members.Select(x => x.Id).Contains(userId))
                .ToList();

            foreach (var group in groupsWhereUserIsMember)
            {
                await RemoveUserFromGroup(group.Id, new ShoppingUserModel { Id = userId });
            }

            return true;
        }

        public bool ItemAlreadyExists(UserGroup item)
        {
            var groups = _context.UserGroups.ToList();
            return groups.Any(g => g.Id == item.Id || (g.Owner.Id == item.Id && g.Name == item.Name));
        }

        public bool ItemCanBeUpdated(UserGroup item)
        {
            var groups = _context.UserGroups.ToList();
            var groupsWithoutCurrentItem = groups.Where(g => g.Id == item.Id).ToList();
            var groupsOfOwner = groupsWithoutCurrentItem.Where(g => g.Owner.Id == item.Owner.Id).ToList();
            return !(groupsOfOwner.Any(g => g.Name == item.Name));
        }

        private async Task<bool> RemoveAssignmentsOfGroupAsync(string userGroupId)
        {
            var allAssignmentsOfGroup = (await _groupListAssignments.GetAllAsync())
                .Where(a => a.UserGroupId == userGroupId)
                .ToList();

            return await DeleteAssignments(allAssignmentsOfGroup);
        }

        private async Task<bool> DeleteAssignments(List<UserGroupShoppingList> assignments)
        {
            foreach (var assignment in assignments)
            {
                if (!(await _groupListAssignments.DeleteAsync(assignment)))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
