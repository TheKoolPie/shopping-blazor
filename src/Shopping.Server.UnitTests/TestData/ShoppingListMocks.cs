using Moq;
using Shopping.Shared.Data;
using Shopping.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Server.UnitTests.TestData
{
    public static class ShoppingListMocks
    {

        public static List<ShoppingList> GetTwoEmptyListsOfSeperateOwners()
        {
            var adminList = new ShoppingList();
            adminList.Owner = UserMocks.AdminUser;

            var userList = new ShoppingList();
            userList.Owner = UserMocks.TestUser;

            return new List<ShoppingList>
            {
                adminList,
                userList
            };
        }
    }
}
