using Microsoft.Extensions.Logging;
using Shopping.Shared.Data;
using Shopping.Shared.Services.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Shopping.Shared.Services
{
    public class ShoppingListItemsApiAccess : CRUDAccessBaseImpl<ShoppingListItem>, IShoppingListItems
    {
        public ShoppingListItemsApiAccess(HttpClient httpClient, ILogger<ShoppingListItemsApiAccess> logger) : base(httpClient, logger)
        {
            BaseAddress = "api/ShoppingListItems";
        }
    }
}
