﻿using Shopping.Shared.Model.Account;
using System.Net.Http;
using System.Threading.Tasks;
using Shopping.Shared.Results;

namespace Shopping.Client.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel model);
        Task Logout();
        Task<RegisterResult> Register(RegisterModel model);
        Task<HttpClient> GetHttpClientAsync();
    }
}