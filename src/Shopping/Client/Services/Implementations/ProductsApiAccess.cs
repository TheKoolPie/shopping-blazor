using Microsoft.Extensions.Logging;
using Shopping.Shared.Data;
using System.Net.Http;
using Shopping.Client.Services.Interfaces;
using Shopping.Shared.Services;

namespace Shopping.Client.Services.Implementations
{
    public class ProductsApiAccess : CRUDApiAccessBaseImpl<ProductItem>, IProducts
    {
        public ProductsApiAccess(IAuthService authService, ILogger<ProductsApiAccess> logger) : base(authService, logger)
        {
            BaseAddress = "api/Product";
        }
    }
}
