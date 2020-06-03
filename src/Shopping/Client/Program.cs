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

namespace Shopping.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("Shopping.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Shopping.ServerAPI"));

            builder.Services.AddApiAuthorization()
                .AddAccountClaimsPrincipalFactory<CustomUserFactory>();

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddScoped<ITokenProvider, TokenProviderLocalStorage>();

            builder.Services.AddTransient<IProductCategories, ProductCategoryApiAccess>();
            builder.Services.AddTransient<IProducts, ProductsApiAccess>();
            builder.Services.AddTransient<IShoppingListItems, ShoppingListItemsApiAccess>();
            builder.Services.AddTransient<IShoppingLists, ShoppingListsApiAccess>();



            await builder.Build().RunAsync();
        }
    }
}
