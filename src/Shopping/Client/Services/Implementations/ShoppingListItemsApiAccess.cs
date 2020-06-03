using Microsoft.Extensions.Logging;
using Shopping.Shared.Data;
using System.Net.Http;
using Shopping.Client.Services.Interfaces;
using Shopping.Shared.Services;

namespace Shopping.Client.Services.Implementations
{
    public class ShoppingListItemsApiAccess : CRUDAccessBaseImpl<ShoppingListItem>, IShoppingListItems
    {
        public ShoppingListItemsApiAccess(HttpClient httpClient,
            ITokenProvider tokenProvider,
            ILogger<ShoppingListItemsApiAccess> logger) : base(httpClient, tokenProvider, logger)
        {
            BaseAddress = "api/ShoppingListItems";
        }
    }
}
