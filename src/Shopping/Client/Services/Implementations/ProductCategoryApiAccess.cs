using Microsoft.Extensions.Logging;
using Shopping.Shared.Data;
using System.Net.Http;
using Shopping.Client.Services.Interfaces;
using Shopping.Shared.Services;

namespace Shopping.Client.Services.Implementations
{
    public class ProductCategoryApiAccess : CRUDApiAccessBaseImpl<ProductCategory>, IProductCategories
    {
        public ProductCategoryApiAccess(IAuthService authService, ILogger<ProductCategoryApiAccess> logger) : base(authService, logger)
        {
            BaseAddress = "api/ProductCategory";
        }
    }
}
