using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopping.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;
using Shopping.Server.Models;
using Shopping.Server.Configuration;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Shopping.Shared.Model.Account;
using Shopping.Server.Services;

namespace Shopping.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(o =>
                o.UseMySql(Configuration.GetConnectionString("IdentityMySQL")));

            services.AddDefaultIdentity<ShoppingUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            var connString = Configuration.GetConnectionString("Shopping_Azure");
            var connData = new CosmosDbConnStringData(connString);

            services.AddDbContext<ShoppingDbContext>(o => o.UseCosmos(connData.Endpoint, connData.Key, "shopping-list"));

            services.AddHealthChecks();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityKey"]))
                    };
                });
            services.AddAuthorization(o =>
            {
                o.AddPolicy(ShoppingUserPolicies.IsAdmin, ShoppingUserPolicies.IsAdminPolicy());
                o.AddPolicy(ShoppingUserPolicies.IsProductModifier, ShoppingUserPolicies.IsProductModifierPolicy());
                o.AddPolicy(ShoppingUserPolicies.IsProductCategoryModifier, ShoppingUserPolicies.IsProductCategoryModifierPolicy());
                o.AddPolicy(ShoppingUserPolicies.IsUserManager, ShoppingUserPolicies.IsUserManagerPolicy());
                o.AddPolicy(ShoppingUserPolicies.IsUserCreator, ShoppingUserPolicies.IsUserCreatorPolicy());
                o.AddPolicy(ShoppingUserPolicies.IsUserRoleManager, ShoppingUserPolicies.IsUserRoleManagerPolicy());
                o.AddPolicy(ShoppingUserPolicies.IsDatabaseManager, ShoppingUserPolicies.IsDatabaseManagerPolicy());
            });

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddHttpContextAccessor();
            services.AddTransient<IUserProvider, UserFromHttpContextProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            UserManager<ShoppingUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
                endpoints.MapFallbackToFile("index.html");
            });

            AdminSettings admin = Configuration.GetSection("AdminSettings").Get<AdminSettings>();
            List<string> userRoles = Configuration.GetSection("UserRoles").Get<List<string>>();
            ApplicationDbInitializer.SeedRoles(roleManager, userRoles, logger);
            ApplicationDbInitializer.SeedUsers(userManager, admin, userRoles, logger);
        }
    }
}
