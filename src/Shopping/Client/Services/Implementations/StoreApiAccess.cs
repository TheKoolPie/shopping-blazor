﻿using Microsoft.Extensions.Logging;
using Shopping.Client.Services.Implementations.Base;
using Shopping.Shared.Data;
using Shopping.Shared.Results.Entities;
using Shopping.Shared.Services;
using Shopping.Shared.Services.Interfaces;

namespace Shopping.Client.Services.Implementations
{
    public class StoreApiAccess : BaseShoppingApiImpl<Store, StoreResult>, IStoreRepository
    {
        public StoreApiAccess(IAuthService authService, ILogger<StoreApiAccess> logger) 
            : base("Store", authService, logger)
        {
        }
    }
}
