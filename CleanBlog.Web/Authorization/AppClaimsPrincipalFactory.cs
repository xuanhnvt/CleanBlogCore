// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nhx.Core.Entities.Users;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace CleanBlog.Web.Authorization
{
    /// <summary>
    /// Provides methods to create a claims principal for a given user.
    /// </summary>
    /// <typeparam name="TUser">The type used to represent a user.</typeparam>
    /// <typeparam name="Role">The type used to represent a role.</typeparam>
    /*public class AppClaimsPrincipalFactory<User> : UserClaimsPrincipalFactory<User>
        where TUser : class
        //where TRole : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaimsPrincipalFactory{TUser, Role}"/> class.
        /// </summary>
        /// <param name="userManager">The <see cref="UserManager{TUser}"/> to retrieve user information from.</param>
        /// <param name="roleManager">The <see cref="RoleManager{Role}"/> to retrieve a user's roles from.</param>
        /// <param name="options">The configured <see cref="IdentityOptions"/>.</param>
        public AppClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<Role> roleManager, IOptions<IdentityOptions> options)
            : base(userManager, options)
        {
            if (roleManager == null)
            {
                throw new ArgumentNullException(nameof(roleManager));
            }
            RoleManager = roleManager;
        }

        /// <summary>
        /// Gets the <see cref="RoleManager{Role}"/> for this factory.
        /// </summary>
        /// <value>
        /// The current <see cref="RoleManager{Role}"/> for this factory instance.
        /// </value>
        public RoleManager<Role> RoleManager { get; private set; }

        /// <summary>
        /// Generate the claims for a user.
        /// </summary>
        /// <param name="user">The user to create a <see cref="ClaimsIdentity"/> from.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous creation operation, containing the created <see cref="ClaimsIdentity"/>.</returns>
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(TUser user)
        {
            var id = await base.GenerateClaimsAsync(user);
            if (UserManager.SupportsUserRole)
            {
                var roles = await UserManager.GetRolesAsync(user);
                foreach (var roleName in roles)
                {
                    id.AddClaim(new Claim(Options.ClaimsIdentity.RoleClaimType, roleName));
                    if (RoleManager.SupportsRoleClaims)
                    {
                        var role = await RoleManager.FindByNameAsync(roleName);
                        if (role != null)
                        {
                            id.AddClaims(await RoleManager.GetClaimsAsync(role));
                        }
                    }
                }
            }
            return id;
        }
    }*/

    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
    {
        public AppClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<Role> roleManager, IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {

        }

        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var principal = await base.CreateAsync(user);
            // remove duplicate claim
            var newClaims = ((ClaimsIdentity)principal.Identity).Claims.Distinct(new ClaimsComparer());
            return new ClaimsPrincipal(new ClaimsIdentity(newClaims, principal.Identity.AuthenticationType));

            /*var userId = await UserManager.GetUserIdAsync(user);
            var userName = await UserManager.GetUserNameAsync(user);
            var id = new ClaimsIdentity("Identity.Application", // REVIEW: Used to match Application scheme
                Options.ClaimsIdentity.UserNameClaimType,
                Options.ClaimsIdentity.RoleClaimType);
            id.AddClaim(new Claim(Options.ClaimsIdentity.UserIdClaimType, userId));
            id.AddClaim(new Claim(Options.ClaimsIdentity.UserNameClaimType, userName));
            if (UserManager.SupportsUserSecurityStamp)
            {
                id.AddClaim(new Claim(Options.ClaimsIdentity.SecurityStampClaimType,
                    await UserManager.GetSecurityStampAsync(user)));
            }
            if (UserManager.SupportsUserClaim)
            {
                id.AddClaims(await UserManager.GetClaimsAsync(user));
            }

            if (UserManager.SupportsUserRole)
            {
                var roleClaims = new List<Claim>();
                var roles = await UserManager.GetRolesAsync(user);
                foreach (var roleName in roles)
                {
                    id.AddClaim(new Claim(Options.ClaimsIdentity.RoleClaimType, roleName));
                    if (RoleManager.SupportsRoleClaims)
                    {
                        var role = await RoleManager.FindByNameAsync(roleName);
                        if (role != null)
                        {
                            roleClaims.AddRange(await RoleManager.GetClaimsAsync(role));
                        }
                    }
                }
                // remove duplicate claim
                roleClaims = roleClaims.Distinct(new ClaimsComparer()).ToList();
                id.AddClaims(roleClaims);
            }

            return new ClaimsPrincipal(id);*/
        }
    }

    public class ClaimsComparer : IEqualityComparer<Claim>
    {
        public bool Equals(Claim x, Claim y)
        {
            return x.Value == y.Value;
        }
        public int GetHashCode(Claim claim)
        {
            var claimValue = claim.Value?.GetHashCode() ?? 0;
            return claimValue;
        }
    }
}
