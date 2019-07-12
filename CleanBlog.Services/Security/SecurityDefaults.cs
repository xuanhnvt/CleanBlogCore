namespace CleanBlog.Services.Security
{
    /// <summary>
    /// Represents default values related to security services
    /// </summary>
    public static partial class SecurityDefaults
    {
        #region Access control list

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : entity ID
        /// {1} : entity name
        /// </remarks>
        public static string AclRecordByEntityIdNameCacheKey => "Aclrecord.entityid-name-{0}-{1}";

        /// <summary>
        /// Gets a key pattern to clear cache
        /// </summary>
        public static string AclRecordPatternCacheKey => "Aclrecord.";

        #endregion

        #region Permissions

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : customer role ID
        /// {1} : permission system name
        /// </remarks>
        public static string PermissionsAllowedCacheKey => "Permission.allowed-{0}-{1}";

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : customer role ID
        /// </remarks>
        public static string PermissionsAllByClaimRecordIdCacheKey => "Permission.allbyclaimrecordid-{0}";

        /// <summary>
        /// Gets a key pattern to clear cache
        /// </summary>
        public static string PermissionsPatternCacheKey => "Permission.";

        #endregion
    }
}