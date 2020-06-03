using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Client.Services.Interfaces
{
    public interface ITokenProvider
    {
        Task<string> GetTokenAsync();
    }
}
