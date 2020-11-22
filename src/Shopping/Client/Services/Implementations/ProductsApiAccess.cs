using Microsoft.Extensions.Logging;
using Shopping.Shared.Data;
using Shopping.Shared.Services.Interfaces;
using Shopping.Client.Services.Implementations.Base;
using Shopping.Shared.Results;

namespace Shopping.Client.Services.Implementations
{
    public class ProductsApiAccess : BaseShoppingApiImpl<ProductItem,ProductItemResult>, IProducts
    {
        public ProductsApiAccess(IAuthService authService, ILogger<ProductsApiAccess> logger) 
            : base("Product",authService, logger)
        {
        }
    }
}
