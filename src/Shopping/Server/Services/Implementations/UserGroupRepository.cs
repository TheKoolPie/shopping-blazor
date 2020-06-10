using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopping.Server.Data;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Model.Results;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Services.Implementations
{
    public class UserGroupRepository : CRUDDbContextBaseImpl<UserGroup>, IUserGroups
    {
        private readonly IUserGroupShoppingLists _userGroupShoppingLists;
        private readonly IUserRepository _userRepository;
        public UserGroupRepository(ShoppingDbContext context, IUserRepository userRepository,
            ILogger<UserGroupRepository> logger, IUserGroupShoppingLists userGroupShoppingLists)
            : base(context, logger)
        {
            _userRepository = userRepository;
            _userGroupShoppingLists = userGroupShoppingLists;
        }

        public override async Task<List<UserGroup>> GetAllAsync()
        {
            var groups = await _context.UserGroups.ToListAsync();
            foreach (var group in groups)
            {
                group.ShoppingLists = await _userGroupShoppingLists.GetShoppingListsOfUserGroupAsync(group.Id);
            }
            return groups;
        }

        public override async Task<UserGroup> GetAsync(string id)
        {
            var userGroup = await _context.UserGroups.FirstOrDefaultAsync(i => i.Id == id);
            if (userGroup == null)
            {
                throw new ItemNotFoundException(typeof(UserGroup), id);
            }
            userGroup.ShoppingLists = await _userGroupShoppingLists.GetShoppingListsOfUserGroupAsync(userGroup.Id);

            return userGroup;
        }

        public async Task<List<UserGroup>> GetAllOfUserAsync(string userId)
        {
            return (await GetAllAsync()).Where(i => UserIsInGroup(i, userId)).ToList();
        }

        public override bool ItemAlreadyExists(UserGroup item)
        {
            var userGroups = _context.UserGroups.ToList();
            return userGroups.Any(i => i.Id == item.Id || i.Name == item.Name);
        }
        public override void UpdateExistingItem(UserGroup existing, UserGroup update)
        {
            existing.Owner = update.Owner;
            existing.Name = update.Name;
            existing.Members = new List<ShoppingUserModel>(update.Members);
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
                throw new ItemAlreadyExistsException($"User with id {existingUser.Id} already exists in group: '{group.Name}'");
            }

            group.Members.Add(new ShoppingUserModel()
            {
                Id = existingUser.Id
            });

            _context.UserGroups.Update(group);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new PersistencyException($"Could save adding of user {existingUser.Id} to group '{group.Name}'", e);
            }

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
    }
}
