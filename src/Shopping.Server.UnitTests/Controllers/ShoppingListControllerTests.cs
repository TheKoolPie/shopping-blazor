using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using Shopping.Server.Controllers;
using Shopping.Server.UnitTests.TestData;
using Shopping.Shared.Data;
using Shopping.Shared.Exceptions;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Results;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Shopping.Server.UnitTests.Controllers
{
    public class ShoppingListControllerTests
    {
        [Fact]
        public async Task GetLists_ResponseType_IsOkObjectResult()
        {
            var adminList = new ShoppingList();
            adminList.Owner = UserMocks.AdminUser;

            var userList = new ShoppingList();
            userList.Owner = UserMocks.TestUser;

            var shoppingLists = new List<ShoppingList>
            {
                adminList,
                userList
            };

            var shoppingListRepoMock = new Mock<IShoppingLists>();
            shoppingListRepoMock.Setup(l => l.GetAllAsync())
                .Returns(Task.FromResult(shoppingLists));

            var currentUserRepoMock = UserMocks.GetMockUserProvider(UserMocks.AdminUser);

            var controller = new ShoppingListsController(shoppingListRepoMock.Object, currentUserRepoMock, null);

            var response = await controller.GetLists();

            Assert.IsType<OkObjectResult>(response.Result);
        }
        [Fact]
        public async Task GetLists_ResponseValue_IsListOfShoppingLists()
        {
            var adminList = new ShoppingList();
            adminList.Owner = UserMocks.AdminUser;

            var userList = new ShoppingList();
            userList.Owner = UserMocks.TestUser;

            var shoppingLists = new List<ShoppingList>
            {
                adminList,
                userList
            };

            var shoppingListRepoMock = new Mock<IShoppingLists>();
            shoppingListRepoMock.Setup(l => l.GetAllAsync())
                .Returns(Task.FromResult(shoppingLists));

            var currentUserRepoMock = UserMocks.GetMockUserProvider(UserMocks.AdminUser);

            var controller = new ShoppingListsController(shoppingListRepoMock.Object, currentUserRepoMock, null);

            var response = await controller.GetLists();
            var result = response.Result as OkObjectResult;

            Assert.IsType<ShoppingListResult>(result.Value);
        }

        [Fact]
        public async Task GetLists_UserIsAdmin_ReturnsAllLists()
        {
            var adminList = new ShoppingList();
            adminList.Owner = UserMocks.AdminUser;

            var userList = new ShoppingList();
            userList.Owner = UserMocks.TestUser;

            var shoppingLists = new List<ShoppingList>
            {
                adminList,
                userList
            };

            var shoppingListRepoMock = new Mock<IShoppingLists>();
            shoppingListRepoMock.Setup(l => l.GetAllAsync())
                .Returns(Task.FromResult(shoppingLists));

            var currentUserRepoMock = UserMocks.GetMockUserProvider(UserMocks.AdminUser);

            var controller = new ShoppingListsController(shoppingListRepoMock.Object, currentUserRepoMock, null);

            var response = await controller.GetLists();
            var result = response.Result as OkObjectResult;
            var resultValue = result.Value as ShoppingListResult;

            Assert.Equal(2, resultValue.ResultData.Count);
        }

        [Fact]
        public async Task GetLists_UserIsNotAdmin_ReturnsListsBelongingToUser()
        {
            string userListName = "UserList";

            var adminList = new ShoppingList();
            adminList.Owner = UserMocks.AdminUser;
            adminList.Name = "AdminList";

            var userList = new ShoppingList();
            userList.Owner = UserMocks.TestUser;
            userList.Name = userListName;

            var shoppingLists = new List<ShoppingList>
            {
                adminList,
                userList
            };

            var shoppingListRepoMock = new Mock<IShoppingLists>();
            shoppingListRepoMock.Setup(l => l.GetAllAsync())
                .Returns(Task.FromResult(shoppingLists));
            shoppingListRepoMock.Setup(l => l.GetAllOfUserAsync(UserMocks.TestUser.Id))
                .Returns(Task.FromResult(new List<ShoppingList>
                {
                    userList
                }));

            var currentUserRepoMock = UserMocks.GetMockUserProvider(UserMocks.TestUser);

            var controller = new ShoppingListsController(shoppingListRepoMock.Object, currentUserRepoMock, null);

            var response = await controller.GetLists();
            var result = response.Result as OkObjectResult;
            var resultValue = result.Value as ShoppingListResult;

            Assert.Single(resultValue.ResultData);
            Assert.Equal(userListName, resultValue.ResultData.First().Name);
        }

        [Fact]
        public async Task GetList_UserIsNotAdminAndNotUserOfList_ReturnsUnauthorizedResult()
        {
            var userRepoMock = UserMocks.GetMockUserProvider(UserMocks.TestUser);

            var listRepoMock = new Mock<IShoppingLists>();
            listRepoMock.Setup(l => l.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(new ShoppingList()));

            var controller = new ShoppingListsController(listRepoMock.Object, userRepoMock, null);

            var response = await controller.GetList("");

            Assert.IsType<UnauthorizedObjectResult>(response.Result);
        }

        [Fact]
        public async Task GetList_UserIsAdminNotInList_ReturnsList()
        {
            var testListName = "TestList";
            var userRepoMock = UserMocks.GetMockUserProvider(UserMocks.AdminUser);

            var listRepoMock = new Mock<IShoppingLists>();
            listRepoMock.Setup(l => l.GetAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new ShoppingList() { Name = testListName }));

            var controller = new ShoppingListsController(listRepoMock.Object, userRepoMock, null);

            var response = await controller.GetList("");
            var resultObject = response.Result as OkObjectResult;
            var list = resultObject.Value as ShoppingListResult;

            Assert.Equal(testListName, list.ResultData[0].Name);
        }
        [Fact]
        public async Task GetList_UserIsNotAdminInList_ReturnsList()
        {
            var testListName = "TestList";
            var testListId = "abc-de";

            var testList = new ShoppingList
            {
                Id = testListId,
                Name = testListName
            };

            var userRepoMock = UserMocks.GetMockUserProvider(UserMocks.TestUser);

            var listRepoMock = new Mock<IShoppingLists>();
            listRepoMock.Setup(l => l.GetAsync(testListId))
                .Returns(Task.FromResult(testList));
            listRepoMock.Setup(l => l.IsOfUserAsync(testList.Id, UserMocks.TestUser.Id))
                .Returns(Task.FromResult(true));

            var controller = new ShoppingListsController(listRepoMock.Object, userRepoMock, null);

            var response = await controller.GetList(testListId);
            var resultObject = response.Result as OkObjectResult;
            var list = resultObject.Value as ShoppingListResult;

            Assert.Equal(testListName, list.ResultData[0].Name);
        }

        [Fact]
        public async Task GetList_SearchForNonExistingList_ReturnsNotFoundResult()
        {
            var testListId = "abc-def";
            var userRepoMock = UserMocks.GetMockUserProvider(UserMocks.AdminUser);

            var listRepoMock = new Mock<IShoppingLists>();
            listRepoMock.Setup(l => l.GetAsync(testListId))
                .Throws(new ItemNotFoundException());

            var controller = new ShoppingListsController(listRepoMock.Object, userRepoMock, null);
            var response = await controller.GetList(testListId);

            Assert.IsType<NotFoundObjectResult>(response.Result);
        }

        [Fact]
        public async Task CreateList_CreateNewList_OwnerIsCurrentUser()
        {
            var userRepoMock = UserMocks.GetMockUserProvider(UserMocks.AdminUser);

            var testList = new ShoppingList()
            {
                Name = "TestList"
            };

            var listRepoMock = new Mock<IShoppingLists>();
            listRepoMock.Setup(l => l.CreateAsync(testList))
                .Returns(Task.FromResult(testList));

            var controller = new ShoppingListsController(listRepoMock.Object, userRepoMock, null);

            var response = await controller.CreateList(testList);

            var result = response.Result as OkObjectResult;
            var list = result.Value as ShoppingListResult;

            Assert.Equal(UserMocks.AdminUser.Id, list.ResultData[0].OwnerId);
        }

        [Fact]
        public async Task CreateList_ItemAlreadyExists_ReturnsConflictResult()
        {
            var userRepoMock = UserMocks.GetMockUserProvider(UserMocks.AdminUser);

            var listRepoMock = new Mock<IShoppingLists>();
            listRepoMock.Setup(l => l.CreateAsync(It.IsAny<ShoppingList>()))
                .Throws(new ItemAlreadyExistsException());

            var controller = new ShoppingListsController(listRepoMock.Object, userRepoMock, null);

            var response = await controller.CreateList(new ShoppingList());

            Assert.IsType<ConflictObjectResult>(response.Result);
        }

        [Fact]
        public async Task AddItemToList_ListDoesNotExist_ReturnsNotFoundResult()
        {
            var userRepoMock = UserMocks.GetMockUserProvider(UserMocks.AdminUser);

            var listRepoMock = new Mock<IShoppingLists>();
            listRepoMock.Setup(l => l.IsOfUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new ItemNotFoundException());

            var controller = new ShoppingListsController(listRepoMock.Object, userRepoMock, null);

            var response = await controller.AddItemToList("", new ShoppingListItem());

            Assert.IsType<NotFoundObjectResult>(response.Result);
        }
        [Fact]
        public async Task AddItemToList_WhileAddingListNotFound_ReturnsNotFoundResult()
        {
            var listRepoMock = new Mock<IShoppingLists>();
            listRepoMock.Setup(l => l.GetAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new ShoppingList()));
            listRepoMock.Setup(l => l.AddOrUpdateItemAsync(It.IsAny<string>(), It.IsAny<ShoppingListItem>()))
                .Throws(new ItemNotFoundException());

            var controller = new ShoppingListsController(listRepoMock.Object, UserMocks.GetMockUserProvider(UserMocks.AdminUser), null);

            var response = await controller.AddItemToList("", new ShoppingListItem());

            Assert.IsType<NotFoundObjectResult>(response.Result);
        }

        [Fact]
        public async Task AddItemToList_WhileAddingPersistencyError_ReturnsConflictResult()
        {
            var listRepoMock = new Mock<IShoppingLists>();
            listRepoMock.Setup(l => l.GetAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new ShoppingList()));
            listRepoMock.Setup(l => l.AddOrUpdateItemAsync(It.IsAny<string>(), It.IsAny<ShoppingListItem>()))
                .Throws(new PersistencyException());

            var controller = new ShoppingListsController(listRepoMock.Object, UserMocks.GetMockUserProvider(UserMocks.AdminUser), null);

            var response = await controller.AddItemToList("", new ShoppingListItem());

            Assert.IsType<ConflictObjectResult>(response.Result);
        }

        [Fact]
        public async Task AddItemToList_AddItemToList_ReturnsOkResultWithItem()
        {
            var testList = new ShoppingList
            {
                Id = "abc-def"
            };

            var item = new ShoppingListItem()
            {
                Id = "123-456"
            };

            var listRepoMock = new Mock<IShoppingLists>();
            listRepoMock.Setup(l => l.GetAsync(testList.Id))
                .Returns(Task.FromResult(testList));
            listRepoMock.Setup(l => l.AddOrUpdateItemAsync(testList.Id, item))
                .Returns(Task.FromResult(item));

            var controller = new ShoppingListsController(listRepoMock.Object, UserMocks.GetMockUserProvider(UserMocks.AdminUser), null);

            var response = await controller.AddItemToList(testList.Id, item);

            var result = response.Result as OkObjectResult;

            var itemResult = result.Value as ShoppingListItemResult;

            Assert.Equal(item.Id, itemResult.ResultData[0].Id);
        }

        [Fact]
        public async Task UpdateList_IdsDoNotMatch_ReturnsBadRequestResult()
        {
            var controller = new ShoppingListsController(null, null, null);
            var response = await controller.UpdateList("123", new ShoppingList() { Id = "abc" });

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }
        [Fact]
        public async Task UpdateList_UpdateThrowsItemNotFoundException_ReturnsNotFoundResult()
        {
            var userRepoMock = UserMocks.GetMockUserProvider(UserMocks.AdminUser);

            var listRepoMock = new Mock<IShoppingLists>();
            listRepoMock.Setup(l => l.IsOfUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));
            listRepoMock.Setup(l => l.UpdateAsync(It.IsAny<string>(), It.IsAny<ShoppingList>()))
                .Throws(new ItemNotFoundException());

            var controller = new ShoppingListsController(listRepoMock.Object, userRepoMock, null);
            var response = await controller.UpdateList("123", new ShoppingList() { Id = "123" });

            Assert.IsType<NotFoundObjectResult>(response.Result);
        }

        [Fact]
        public async Task UpdateList_UpdateThrowsPersistencyException_ReturnsConflictResult()
        {
            var userRepoMock = UserMocks.GetMockUserProvider(UserMocks.AdminUser);

            var listRepoMock = new Mock<IShoppingLists>();
            listRepoMock.Setup(l => l.IsOfUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));
            listRepoMock.Setup(l => l.UpdateAsync(It.IsAny<string>(), It.IsAny<ShoppingList>()))
                .Throws(new PersistencyException());

            var controller = new ShoppingListsController(listRepoMock.Object, userRepoMock, null);
            var response = await controller.UpdateList("123", new ShoppingList() { Id = "123" });

            Assert.IsType<ConflictObjectResult>(response.Result);
        }

        [Fact]
        public async Task UpdateList_UpdateList_ReturnsOkObjectResultWithList()
        {
            var userRepoMock = UserMocks.GetMockUserProvider(UserMocks.AdminUser);

            var testList = new ShoppingList()
            {
                Id = "123"
            };

            var listRepoMock = new Mock<IShoppingLists>();
            listRepoMock.Setup(l => l.IsOfUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));
            listRepoMock.Setup(l => l.UpdateAsync(testList.Id, testList))
                .Returns(Task.FromResult(testList));

            var controller = new ShoppingListsController(listRepoMock.Object, userRepoMock, null);
            var response = await controller.UpdateList(testList.Id, testList);

            Assert.IsType<OkObjectResult>(response.Result);

            var result = response.Result as OkObjectResult;
            var list = result.Value as ShoppingListResult;

            Assert.Equal(testList.Id, list.ResultData[0].Id);
        }

        [Fact]
        public async Task DeleteList_DeleteThrowsItemNotFoundException_ReturnsNotFoundResult()
        {
            var userRepoMock = UserMocks.GetMockUserProvider(UserMocks.AdminUser);

            var listRepoMock = new Mock<IShoppingLists>();
            listRepoMock.Setup(l => l.IsOfUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));
            listRepoMock.Setup(l => l.DeleteByIdAsync(It.IsAny<string>()))
                .Throws(new ItemNotFoundException());

            var controller = new ShoppingListsController(listRepoMock.Object, userRepoMock, null);
            var response = await controller.DeleteList("123");

            Assert.IsType<NotFoundObjectResult>(response.Result);
        }

        [Fact]
        public async Task DeleteList_DeleteThrowsPersistencyException_ReturnsConflictResult()
        {
            var userRepoMock = UserMocks.GetMockUserProvider(UserMocks.AdminUser);

            var listRepoMock = new Mock<IShoppingLists>();
            listRepoMock.Setup(l => l.IsOfUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));
            listRepoMock.Setup(l => l.DeleteByIdAsync(It.IsAny<string>()))
                .Throws(new PersistencyException());

            var controller = new ShoppingListsController(listRepoMock.Object, userRepoMock, null);
            var response = await controller.DeleteList("123");

            Assert.IsType<ConflictObjectResult>(response.Result);
        }

        [Fact]
        public async Task DeleteList_DeleteList_ReturnsOkResult()
        {
            var userRepoMock = UserMocks.GetMockUserProvider(UserMocks.AdminUser);

            var testList = new ShoppingList()
            {
                Id = "123"
            };

            var listRepoMock = new Mock<IShoppingLists>();
            listRepoMock.Setup(l => l.IsOfUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));
            listRepoMock.Setup(l => l.DeleteByIdAsync(testList.Id))
                .Returns(Task.FromResult(true));

            var controller = new ShoppingListsController(listRepoMock.Object, userRepoMock, null);
            var response = await controller.DeleteList(testList.Id);

            Assert.IsType<OkObjectResult>(response.Result);
        }
    }
}
