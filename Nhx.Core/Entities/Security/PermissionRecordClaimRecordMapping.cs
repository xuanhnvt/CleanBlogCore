namespace Nhx.Core.Entities.Security
{
    /// <summary>
    /// Represents a permission record - claim record mapping class
    /// </summary>
    public partial class PermissionRecordClaimRecordMapping : BaseEntity
    {
        /// <summary>
        /// Gets or sets the permission record identifier
        /// </summary>
        public int PermissionRecordId { get; set; }

        /// <summary>
        /// Gets or sets the claim record identifier
        /// </summary>
        public int ClaimRecordId { get; set; }

        /// <summary>
        /// Gets or sets the required claim value
        /// </summary>
        public string RequiredClaimValue { get; set; }

        /// <summary>
        /// Gets or sets the rule to check permission (base on claim value)
        /// </summary>
        public string RuleToCheckPermission { get; set; }

        /// <summary>
        /// Gets or sets the permission record
        /// </summary>
        public virtual PermissionRecord PermissionRecord { get; set; }

        /// <summary>
        /// Gets or sets the claim record
        /// </summary>
        public virtual ClaimRecord ClaimRecord { get; set; }
    }
}