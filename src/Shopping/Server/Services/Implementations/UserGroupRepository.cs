using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopping.Server.Data;
using Shopping.Shared.Data;
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
            return await _context.UserGroups.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<UserGroup>> GetContainingUserAsync(string userId)
        {
            return (await GetAllAsync())
                .Where(i => i.OwnerId == userId || i.MemberIds.Contains(userId))
                .ToList();
        }

        public override bool ItemAlreadyExists(UserGroup item)
        {
            return _context.UserGroups.Any(i => i.Id == item.Id);
        }

        public override bool ItemHasChanged(UserGroup existing, UserGroup updated)
        {
            if (existing.OwnerId != updated.OwnerId || existing.MemberIds.Count != updated.MemberIds.Count)
            {
                return true;
            }

            var existingUsers = existing.MemberIds.OrderBy(x => x).ToList();
            var updatedUsers = existing.MemberIds.OrderBy(x => x).ToList();
            for (int i = 0; i < existingUsers.Count(); i++)
            {
                if (existingUsers[i] != updatedUsers[i])
                {
                    return true;
                }
            }
            return false;
        }

        public override void UpdateExistingItem(UserGroup existing, UserGroup update)
        {
            existing.OwnerId = update.OwnerId;
            existing.MemberIds = new List<string>(update.MemberIds);
        }
    }
}
