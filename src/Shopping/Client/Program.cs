using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shopping.Shared.Services;
using Shopping.Shared.Services.ShoppingList;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Shopping.Client.Provider;
using Shopping.Client.Services.Interfaces;
using Shopping.Client.Services.Implementations;

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

            builder.Services.AddSingleton<IProductCategories, ProductCategoryApiAccess>();
            builder.Services.AddSingleton<IProducts, ProductsApiAccess>();
            builder.Services.AddSingleton<IShoppingListItems, ShoppingListItemsApiAccess>();
            builder.Services.AddSingleton<IShoppingLists, ShoppingListsApiAccess>();

            await builder.Build().RunAsync();
        }
    }
}
