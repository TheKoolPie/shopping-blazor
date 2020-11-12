using Microsoft.Extensions.Logging;
using Shopping.Client.Services.Implementations.Base;
using Shopping.Shared.Data;
using Shopping.Shared.Results.Entities;
using Shopping.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Client.Services.Implementations
{
    public class StoreProductCatApiAccess : BaseShoppingApiImpl<StoreProductCategory, StoreProductCategoryResult>
    {
        public StoreProductCatApiAccess(IAuthService authService, ILogger logger)
            : base("StoreProductCategory", authService, logger)
        {
        }
    }
}
