using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Shopping.Client.Provider;
using Shopping.Client.Services.Interfaces;
using Shopping.Client.Services.Implementations;
using Shopping.Shared.Services;

namespace Shopping.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddScoped<ITokenProvider, TokenProviderLocalStorage>();

            builder.Services.AddTransient<IProductCategories, ProductCategoryApiAccess>();
            builder.Services.AddTransient<IProducts, ProductsApiAccess>();

            await builder.Build().RunAsync();
        }
    }
}
