using Microsoft.Azure.Documents.SystemFunctions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Moq;
using Shopping.Server.Services.Implementations;
using Shopping.Server.UnitTests.Mocks;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Shopping.Server.UnitTests.Services
{
    public class ShoppingListRepositoryTests
    {

        [Fact]
        public async Task GetAsync_IdNotExisting_ThrowsItemNotFoundException()
        {
            using (var context = DBContextMocks.GetMock())
            {
                var repo = new ShoppingListRepository(context, null, null);
                await Assert.ThrowsAsync<ItemNotFoundException>(async () =>
                   await repo.GetAsync(DateTime.Now.ToString("ddMMyyyyHHmm")));
            }
        }
        [Fact]
        public async Task GetAsync_IdExists_ReturnsItem()
        {
            using (var context = DBContextMocks.GetMock())
            {
                var repo = new ShoppingListRepository(context, null, null);

                var item = await repo.GetAsync(DataMocks.GetShoppingLists()[0].Id);

                Assert.NotNull(item);
            }
        }

        [Fact]
        public async Task GetAllOfUserAsync_UserIsCreatorAndGroupMember_GetBothLists()
        {
            using (var context = DBContextMocks.GetMock())
            {
                var userRepo = GetUserRepoMock(DataMocks.CreatorId);
                var listRepo = new ShoppingListRepository(context, null, userRepo);

                var lists = await listRepo.GetAllOfUserAsync(DataMocks.CreatorId);

                Assert.NotNull(lists);
                Assert.Equal(2, lists.Count);
            }
        }
        private IUserGroups GetUserRepoMock(string userId)
        {
            List<UserGroup> retList = DataMocks.GetUserGroups()
                .Where(i => i.OwnerId == userId || i.MemberIds.Contains(userId))
                .ToList();

            var userRepoMock = new Mock<IUserGroups>();
            userRepoMock.Setup(u => u.GetAllOfUserAsync(userId))
                .ReturnsAsync(retList);

            return userRepoMock.Object;
        }
        [Fact]
        public async Task AddOrUpdateItemAsync_AddNewItemToList_ListGetsUpdated()
        {
            using (var context = DBContextMocks.GetMock())
            {
                var listRepo = new ShoppingListRepository(context, null, null);
                var list = await context.ShoppingLists.FirstOrDefaultAsync();
                int itemCountBefore = list.Items.Count;
                list.AddOrUpdateItem(new ShoppingListItem()
                {
                    ProductItemId = "0010",
                    Amount = 250.5f,
                });
                var upd = await listRepo.UpdateAsync(list.Id, list);

                Assert.Equal(itemCountBefore + 1, upd.Items.Count);
            }
        }
        [Fact]
        public async Task AddOrUpdateItemAsync_UpdateExistingItemToList_ListGetsUpdated()
        {
            using (var context = DBContextMocks.GetMock())
            {
                var listRepo = new ShoppingListRepository(context, null, null);
                var list = await context.ShoppingLists.FirstOrDefaultAsync();

                var listCount = list.Items.Count;

                var firstItemInList = list.Items.FirstOrDefault();
                var firstItemId = firstItemInList.Id;
                var firstItemAmount = firstItemInList.Amount;

                var updatedAmount = firstItemAmount + 100.0f;

                var updItem = new ShoppingListItem(firstItemInList);
                updItem.Amount = updatedAmount;

                await listRepo.AddOrUpdateItemAsync(list.Id, updItem);

                var updatedList = await listRepo.GetAsync(list.Id);

                Assert.Equal(listCount, updatedList.Items.Count);
                Assert.Equal(
                    updatedList.Items.FirstOrDefault(i => i.Id == firstItemId).Amount,
                    updatedAmount);
            }
        }
    }
}
