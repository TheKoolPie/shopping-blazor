using Microsoft.Extensions.Logging;
using Shopping.Shared.Data;
using Shopping.Shared.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.Services
{
    public class ProductCategoryApiAccess : CRUDAccessBaseImpl<ProductCategory>, IProductCategories
    {
        public ProductCategoryApiAccess(HttpClient httpClient, ILogger<ProductCategoryApiAccess> logger) : base(httpClient, logger)
        {
            BaseAddress = "api/ProductCategory";
        }
    }
}
