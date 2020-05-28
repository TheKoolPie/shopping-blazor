using Shopping.Shared.Data;
using Shopping.Shared.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Shared.Services
{
    public interface IProducts : ICRUDAccess<ProductItem>
    {
    }
}
