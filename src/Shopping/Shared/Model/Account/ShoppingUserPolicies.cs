using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Shared.Model.Account
{
    public static class ShoppingUserPolicies
    {
        public const string IsAdmin = "IsAdmin";

        public const string IsProductModifier = "IsProductModifier";
        public const string IsProductCategoryModifier = "IsProductCategoryModifier";

        public const string IsUserManager = "IsUserManager";
        public const string IsUserCreator = "IsUserCreator";
        public const string IsUserRoleManager = "IsUserRoleManager";

        public const string IsDatabaseManager = "IsDatabaseManager";

        public static AuthorizationPolicy IsAdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                .RequireRole(ShoppingUserRoles.Admin)
                .Build();
        }

        public static AuthorizationPolicy IsProductModifierPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                .RequireRole(ShoppingUserRoles.Admin, ShoppingUserRoles.Manager, ShoppingUserRoles.Creator)
                .Build();
        }
        public static AuthorizationPolicy IsProductCategoryModifierPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                .RequireRole(ShoppingUserRoles.Admin, ShoppingUserRoles.Manager, ShoppingUserRoles.Creator)
                .Build();
        }
        public static AuthorizationPolicy IsUserManagerPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                .RequireRole(ShoppingUserRoles.Admin, ShoppingUserRoles.Manager)
                .Build();
        }
        public static AuthorizationPolicy IsUserCreatorPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                .RequireRole(ShoppingUserRoles.Admin)
                .Build();
        }
        public static AuthorizationPolicy IsUserRoleManagerPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                .RequireRole(ShoppingUserRoles.Admin)
                .Build();
        }
        public static AuthorizationPolicy IsDatabaseManagerPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                .RequireRole(ShoppingUserRoles.Admin)
                .Build();
        }
    }
}
