using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanBlog.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Nhx.Core.Entities.Users;

namespace CleanBlog.Services.Users
{
    public class CustomRoleStore : RoleStore<Role, CleanBlogDbContext, int, UserRole, RoleClaim>
    {
        public CustomRoleStore(CleanBlogDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {

        }

        /// <summary>
        /// Adds the <paramref name="claim"/> given to the specified <paramref name="role"/>.
        /// </summary>
        /// <param name="role">The role to add the claim to.</param>
        /// <param name="claim">The claim to add to the role.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
        public override Task AddClaimAsync(Role role, Claim claim, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            Context.Set<RoleClaim>().Add(CreateRoleClaim(role, claim));

            return Task.FromResult(false);
        }
    }
}
