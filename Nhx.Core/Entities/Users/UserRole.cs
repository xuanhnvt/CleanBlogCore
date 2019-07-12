using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Nhx.Core.Entities.Users
{
    /// <summary>
    /// /// Represents a mapping between user and role
    /// </summary>
    public class UserRole : IdentityUserRole<int>
    {
        /// <summary>
        /// Gets or sets the user
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the user role
        /// </summary>
        public virtual Role Role { get; set; }
    }
}
