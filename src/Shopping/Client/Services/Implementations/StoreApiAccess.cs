using Microsoft.Extensions.Logging;
using Shopping.Client.Services.Implementations.Base;
using Shopping.Shared.Data;
using Shopping.Shared.Results.Entities;
using Shopping.Shared.Services;

namespace Shopping.Client.Services.Implementations
{
    public class StoreApiAccess : BaseShoppingApiImpl<Store, StoreResult>
    {
        public StoreApiAccess(IAuthService authService, ILogger logger) 
            : base("Store", authService, logger)
        {
        }
    }
}
