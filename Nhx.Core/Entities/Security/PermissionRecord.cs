using System.Collections.Generic;

namespace Nhx.Core.Entities.Security
{
    /// <summary>
    /// Represents a permission record
    /// </summary>
    public partial class PermissionRecord : BaseEntity
    {
        private ICollection<PermissionRecordClaimRecordMapping> _permissionRecordClaimRecordMappings;

        /// <summary>
        /// Gets or sets the permission name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the permission system name
        /// </summary>
        public string SystemName { get; set; }
        
        /// <summary>
        /// Gets or sets the permission category
        /// </summary>
        public string Category { get; set; }

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