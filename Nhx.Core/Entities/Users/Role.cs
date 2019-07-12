using System;
using System.Collections.Generic;
using System.Text;
using Nhx.Core.Entities.Security;
using Microsoft.AspNetCore.Identity;

namespace Nhx.Core.Entities.Users
{
    /// <summary>
    /// Represents a role of user in application
    /// </summary>
    public class Role: IdentityRole<int>, IEntityId<int>
    {
        /// <summary>
        /// Gets or sets the user role system name
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user role is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user role is system
        /// </summary>
        public bool IsSystemRole { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
    }
}
