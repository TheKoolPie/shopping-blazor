
using Shopping.Shared.Data;
using Shopping.Shared.Services.Interfaces;
using Shopping.Shared.Results;
using Microsoft.Extensions.Logging;
using Shopping.Client.Services.Implementations.Base;

namespace Shopping.Client.Services.Implementations
{
    public class ProductCategoryApiAccess : BaseShoppingApiImpl<ProductCategory, ProductCategoryResult>, IProductCategories
    {
        public ProductCategoryApiAccess(IAuthService authService, ILogger<ProductCategoryApiAccess> logger)
            : base("ProductCategory", authService, logger)
        {
        }


    }
}
