using Moq;
using Shopping.Shared.Data;
using Shopping.Shared.Model.Account;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Shopping.Server.UnitTests.Controllers
{
    public class ShoppingListControllerTests
    {

        [Fact]
        public async Task GetLists_UserIsAdmin_ReturnsAllLists()
        {
            List<ShoppingList> lists = new List<ShoppingList>()
            {
            };
        }
    }
}
