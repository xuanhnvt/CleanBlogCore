using System;
using System.Collections.Generic;
using System.Text;
using Nhx.Core.Entities.Security;
using Microsoft.AspNetCore.Identity;
using Nhx.Core.Entities.Users;

namespace Nhx.Core.Entities.Security
{
    /// <summary>
    /// Represents a claim of user or role in application
    /// </summary>
    public class ClaimRecord: BaseEntity
    {
        private ICollection<PermissionRecordClaimRecordMapping> _permissionRecordClaimRecordMappings;

        /// <summary>
        /// Gets or sets the claim name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the user role system name
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user role is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the claim type
        /// </summary>
        public string ClaimType { get; set; }

        /// <summary>
        /// Gets or sets the claim value type
        /// </summary>
        public string ClaimValueType { get; set; }

        //public virtual ICollection<UserClaim> UserClaims { get; set; }

        //public virtual ICollection<RoleClaim> RoleClaims { get; set; }

        /// <summary>
        /// Gets or sets the permission record-customer role mappings
        /// </summary>
        public virtual ICollection<PermissionRecordClaimRecordMapping> PermissionRecordClaimRecordMappings
        {
            get => _permissionRecordClaimRecordMappings ?? (_permissionRecordClaimRecordMappings = new List<PermissionRecordClaimRecordMapping>());
            protected set => _permissionRecordClaimRecordMappings = value;
        }
    }
}
