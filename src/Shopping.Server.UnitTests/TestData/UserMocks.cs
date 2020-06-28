using Moq;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Server.UnitTests.TestData
{
    public static class UserMocks
    {
        public static ShoppingUserModel AdminUser = new ShoppingUserModel()
        {
            Id = "123-456",
            Email = "admin@test.de",
            UserName = "admin"
        };
        public static ShoppingUserModel TestUser = new ShoppingUserModel()
        {
            Id = "abc-def",
            Email = "test@test.de",
            UserName = "test"
        };

        public static ICurrentUserProvider GetMockUserProvider(ShoppingUserModel user)
        {
            var userRepoMock = new Mock<ICurrentUserProvider>();

            userRepoMock.Setup(m => m.GetUserAsync())
                .Returns(Task.FromResult(user));

            bool isAdmin = user.UserName.Contains("admin", StringComparison.InvariantCultureIgnoreCase);

            userRepoMock.Setup(m => m.IsUserAdminAsync())
                .Returns(Task.FromResult(isAdmin));

            return userRepoMock.Object;
        }
    }
}
