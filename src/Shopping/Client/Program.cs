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
using Shopping.Shared.Model.Account;
using Shopping.Shared.Services.Interfaces;

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
            builder.Services.AddAuthorizationCore(o=> 
            {
                o.AddPolicy(ShoppingUserPolicies.IsAdmin, ShoppingUserPolicies.IsAdminPolicy());
                o.AddPolicy(ShoppingUserPolicies.IsProductModifier, ShoppingUserPolicies.IsProductModifierPolicy());
                o.AddPolicy(ShoppingUserPolicies.IsProductCategoryModifier, ShoppingUserPolicies.IsProductCategoryModifierPolicy());
                o.AddPolicy(ShoppingUserPolicies.IsUserManager, ShoppingUserPolicies.IsUserManagerPolicy());
                o.AddPolicy(ShoppingUserPolicies.IsUserCreator, ShoppingUserPolicies.IsUserCreatorPolicy());
                o.AddPolicy(ShoppingUserPolicies.IsUserRoleManager, ShoppingUserPolicies.IsUserRoleManagerPolicy());
                o.AddPolicy(ShoppingUserPolicies.IsDatabaseManager, ShoppingUserPolicies.IsDatabaseManagerPolicy());
            });

            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddScoped<ITokenProvider, TokenProviderLocalStorage>();

            builder.Services.AddTransient<IProductCategories, ProductCategoryApiAccess>();
            builder.Services.AddTransient<IProducts, ProductsApiAccess>();
            builder.Services.AddTransient<IShoppingLists, ShoppingListsApiAccess>();
            builder.Services.AddTransient<IUserGroupRepository, UserGroupsApiAccess>();
            builder.Services.AddTransient<IUserGroupShoppingLists, UserGroupsShoppingListsApiAccess>();

            builder.Services.AddTransient<IUserRepository, UserRepositoryApiAccess>();
            builder.Services.AddTransient<ICurrentUserProvider, CurrentUserApiAccess>();

            await builder.Build().RunAsync();
        }
    }
}
