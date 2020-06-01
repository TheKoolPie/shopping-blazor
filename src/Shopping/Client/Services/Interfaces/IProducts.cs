﻿using Shopping.Shared.Data;
using Shopping.Client.Services.Interfaces;

namespace Shopping.Client.Services.Interfaces
{
    public interface IProducts : ICRUDAccess<ProductItem>
    {
    }
}
