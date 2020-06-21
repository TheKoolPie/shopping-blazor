
using Shopping.Shared.Data;
using Shopping.Client.Services.Interfaces;
using Shopping.Shared.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shopping.Shared.Results;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using System.Linq;
using Shopping.Client.Services.Implementations.Base;

namespace Shopping.Client.Services.Implementations
{
    public class ProductCategoryApiAccess : BaseShoppingApiImpl<ProductCategory, ProductCategoryResult>, IProductCategories
    {
        public ProductCategoryApiAccess(IAuthService authService, ILogger<ProductCategoryApiAccess> logger)
            : base("api/ProductCategory", authService, logger)
        {
        }


    }
}
