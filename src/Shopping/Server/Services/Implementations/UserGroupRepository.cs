using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopping.Server.Data;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Services.Implementations
{
    public class UserGroupRepository : CRUDDbContextBaseImpl<UserGroup>, IUserGroups
    {
        public UserGroupRepository(ShoppingDbContext context,
            ILogger<UserGroup> logger)
            : base(context, logger)
        {
        }

        public override async Task<List<UserGroup>> GetAllAsync()
        {
            return await _context.UserGroups.ToListAsync();
        }

        public override async Task<UserGroup> GetAsync(string id)
        {
            var userGroup = await _context.UserGroups.FirstOrDefaultAsync(i => i.Id == id);
            if (userGroup == null)
            {
                throw new ItemNotFoundException(typeof(UserGroup), id);
            }
            return userGroup;
        }

        public async Task<List<UserGroup>> GetAllOfUserAsync(string userId)
        {
            return (await GetAllAsync())
                .Where(i => i.OwnerId == userId || i.Members.Select(i => i.UserId).Contains(userId))
                .ToList();
        }

        public override bool ItemAlreadyExists(UserGroup item)
        {
            return _context.UserGroups.Any(i => i.Id == item.Id || i.Name == item.Name);
        }
        public override void UpdateExistingItem(UserGroup existing, UserGroup update)
        {
            existing.OwnerId = update.OwnerId;
            existing.Name = update.Name;
            existing.Members = new List<UserGroupMember>(update.Members);
        }

        public async Task<bool> UserIsInGroupAsync(string userGroupId, string userId)
        {
            var group = await GetAsync(userGroupId);

            bool isOwner = group.OwnerId == userId;
            bool isMember = group.Members.Select(x => x.UserId).Contains(userId);

            return isOwner || isMember;
        }
    }
}
