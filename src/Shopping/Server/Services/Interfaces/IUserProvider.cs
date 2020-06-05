using Shopping.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Server.Services
{
    public interface IUserProvider
    {
        Task<ShoppingUser> GetUserAsync();
    }
}
