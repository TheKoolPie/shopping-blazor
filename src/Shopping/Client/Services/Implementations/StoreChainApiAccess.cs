using Microsoft.Extensions.Logging;
using Shopping.Client.Services.Implementations.Base;
using Shopping.Shared.Data;
using Shopping.Shared.Results.Entities;
using Shopping.Shared.Services;
using Shopping.Shared.Services.Interfaces.Repos;

namespace Shopping.Client.Services.Implementations
{
    public class StoreChainApiAccess : BaseShoppingApiImpl<StoreChain, StoreChainResult>, IStoreChainRepository
    {
        public StoreChainApiAccess(IAuthService authService, ILogger<StoreChainApiAccess> logger)
            : base("api/StoreChain", authService, logger) { }
    }
}
