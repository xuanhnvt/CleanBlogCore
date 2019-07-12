using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Nhx.Core.Entities.Security;

namespace Nhx.Core.Entities.Users
{
    /// <summary>
    /// Represents a role claim
    /// </summary>
    public class RoleClaim: IdentityRoleClaim<int>
    {
        public virtual Role Role { get; set; }

        /// <summary>
        /// Gets or sets the claim record identifier
        /// </summary>
        public virtual int ClaimRecordId { get; set; }

        /// <summary>
        /// Gets or sets the claim record
        /// </summary>
        //public virtual ClaimRecord ClaimRecord { get; set; }
    }
}
