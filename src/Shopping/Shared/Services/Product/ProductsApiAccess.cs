using Microsoft.Extensions.Logging;
using Shopping.Shared.Data;
using Shopping.Shared.Services.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.Services
{
    public class ProductsApiAccess : CRUDAccessBaseImpl<ProductItem>, IProducts
    {
        public ProductsApiAccess(HttpClient httpClient, ILogger<ProductsApiAccess> logger) : base(httpClient, logger)
        {
            BaseAddress = "api/Product";
        }
    }
}
