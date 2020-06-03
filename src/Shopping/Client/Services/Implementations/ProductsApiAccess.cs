using Microsoft.Extensions.Logging;
using Shopping.Shared.Data;
using System.Net.Http;
using Shopping.Client.Services.Interfaces;

namespace Shopping.Client.Services.Implementations
{
    public class ProductsApiAccess : CRUDAccessBaseImpl<ProductItem>, IProducts
    {
        public ProductsApiAccess(HttpClient httpClient,
            ITokenProvider tokenProvider,
            ILogger<ProductsApiAccess> logger)
            : base(httpClient, tokenProvider, logger)
        {
            BaseAddress = "api/Product";
        }
    }
}
