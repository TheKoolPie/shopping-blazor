using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shopping.Shared.Services;
using Shopping.Shared.Services.ShoppingList;

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

            builder.Services.AddApiAuthorization();

            builder.Services.AddSingleton<IProductCategories, ProductCategoryApiAccess>();
            builder.Services.AddSingleton<IProducts, ProductsApiAccess>();
            builder.Services.AddSingleton<IShoppingListItems, ShoppingListItemsApiAccess>();
            builder.Services.AddSingleton<IShoppingLists, ShoppingListsApiAccess>();


            await builder.Build().RunAsync();
        }
    }
}
