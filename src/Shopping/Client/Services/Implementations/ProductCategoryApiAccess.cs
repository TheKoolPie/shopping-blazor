using Microsoft.Extensions.Logging;
using Shopping.Shared.Data;
using System.Net.Http;
using Shopping.Client.Services.Interfaces;

namespace Shopping.Client.Services.Implementations
{
    public class ProductCategoryApiAccess : CRUDAccessBaseImpl<ProductCategory>, IProductCategories
    {
        public ProductCategoryApiAccess(HttpClient httpClient,
            ITokenProvider tokenProvider,
            ILogger<ProductCategoryApiAccess> logger) : base(httpClient,tokenProvider, logger)
        {
            BaseAddress = "api/ProductCategory";
        }
    }
}
