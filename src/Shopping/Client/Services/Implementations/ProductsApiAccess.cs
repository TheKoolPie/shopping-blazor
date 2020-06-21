using Microsoft.Extensions.Logging;
using Shopping.Shared.Data;
using System.Net.Http;
using Shopping.Client.Services.Interfaces;
using Shopping.Shared.Services;
using Shopping.Client.Services.Implementations.Base;
using Shopping.Shared.Results;

namespace Shopping.Client.Services.Implementations
{
    public class ProductsApiAccess : BaseShoppingApiImpl<ProductItem,ProductItemResult>, IProducts
    {
        public ProductsApiAccess(IAuthService authService, ILogger<ProductsApiAccess> logger) 
            : base("api/Product",authService, logger)
        {
        }
    }
}
