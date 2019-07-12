using System;
using System.Collections.Generic;
using System.Linq;
using Nhx.Core;
using Nhx.Core.Caching;
using Nhx.Core.Data;
using Nhx.Core.Entities.Users;
using Nhx.Core.Entities.Security;
//using CleanBlog.Services.Customers;
using CleanBlog.Services.Localization;

namespace CleanBlog.Services.Security
{
    /// <summary>
    /// Permission service
    /// </summary>
    public partial class PermissionService : IPermissionService
    {
        #region Fields

        private readonly ICacheManager _cacheManager;
        //private readonly ICustomerService _customerService;
        //private readonly ILocalizationService _localizationService;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<ClaimRecord> _claimRecordRepository;
        private readonly IRepository<PermissionRecord> _permissionRecordRepository;
        private readonly IRepository<PermissionRecordClaimRecordMapping> _permissionRecordClaimRecordMappingRepository;
        private readonly IStaticCacheManager _staticCacheManager;
        //private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public PermissionService(ICacheManager cacheManager,
            //ICustomerService customerService,
            //ILocalizationService localizationService,
            IRepository<Role> roleRepository,
            IRepository<ClaimRecord> claimRecordRepository,
            IRepository<PermissionRecord> permissionRecordRepository,
            IRepository<PermissionRecordClaimRecordMapping> permissionRecordClaimRecordMappingRepository,
            IStaticCacheManager staticCacheManager
            //IWorkContext workContext
            )
        {
            this._cacheManager = cacheManager;
            //this._customerService = customerService;
            this._roleRepository = roleRepository;
            this._claimRecordRepository = claimRecordRepository;
            //this._localizationService = localizationService;
            this._permissionRecordRepository = permissionRecordRepository;
            this._permissionRecordClaimRecordMappingRepository = permissionRecordClaimRecordMappingRepository;
            this._staticCacheManager = staticCacheManager;
            //this._workContext = workContext;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Get permission records by claim record identifier
        /// </summary>
        /// <param name="claimId">Claim record identifier</param>
        /// <returns>Permissions</returns>
        protected virtual IList<PermissionRecord> GetPermissionRecordsByClaimRecordId(int claimId)
        {
            var key = string.Format(SecurityDefaults.PermissionsAllByClaimRecordIdCacheKey, claimId);
            return _cacheManager.Get(key, () =>
            {
                var query = from pr in _permissionRecordRepository.Table
                            join prcrm in _permissionRecordClaimRecordMappingRepository.Table on pr.Id equals prcrm.PermissionRecordId
                            where prcrm.ClaimRecordId == claimId
                            orderby pr.Id
                            select pr;

                return query.ToList();
            });
        }

        #endregion

        #region Methods

        /// <summary>
        /// Delete a permission
        /// </summary>
        /// <param name="permission">Permission</param>
        public virtual void DeletePermissionRecord(PermissionRecord permission)
        {
            if (permission == null)
                throw new ArgumentNullException(nameof(permission));

            _permissionRecordRepository.Delete(permission);

            _cacheManager.RemoveByPattern(SecurityDefaults.PermissionsPatternCacheKey);
            _staticCacheManager.RemoveByPattern(SecurityDefaults.PermissionsPatternCacheKey);
        }

        /// <summary>
        /// Gets a permission
        /// </summary>
        /// <param name="permissionId">Permission identifier</param>
        /// <returns>Permission</returns>
        public virtual PermissionRecord GetPermissionRecordById(int permissionId)
        {
            if (permissionId == 0)
                return null;

            return _permissionRecordRepository.GetById(permissionId);
        }

        /// <summary>
        /// Gets a permission
        /// </summary>
        /// <param name="systemName">Permission system name</param>
        /// <returns>Permission</returns>
        public virtual PermissionRecord GetPermissionRecordBySystemName(string systemName)
        {
            if (string.IsNullOrWhiteSpace(systemName))
                return null;

            var query = from pr in _permissionRecordRepository.Table
                        where pr.SystemName == systemName
                        orderby pr.Id
                        select pr;

            var permissionRecord = query.FirstOrDefault();
            return permissionRecord;
        }

        /// <summary>
        /// Gets all permissions
        /// </summary>
        /// <returns>Permissions</returns>
        public virtual IList<PermissionRecord> GetAllPermissionRecords()
        {
            var query = from pr in _permissionRecordRepository.Table
                        orderby pr.Name
                        select pr;
            var permissions = query.ToList();
            return permissions;
        }

        /// <summary>
        /// Inserts a permission
        /// </summary>
        /// <param name="permission">Permission</param>
        public virtual void InsertPermissionRecord(PermissionRecord permission)
        {
            if (permission == null)
                throw new ArgumentNullException(nameof(permission));

            _permissionRecordRepository.Insert(permission);

            _cacheManager.RemoveByPattern(SecurityDefaults.PermissionsPatternCacheKey);
            _staticCacheManager.RemoveByPattern(SecurityDefaults.PermissionsPatternCacheKey);
        }

        /// <summary>
        /// Updates the permission
        /// </summary>
        /// <param name="permission">Permission</param>
        public virtual void UpdatePermissionRecord(PermissionRecord permission)
        {
            if (permission == null)
                throw new ArgumentNullException(nameof(permission));

            _permissionRecordRepository.Update(permission);

            _cacheManager.RemoveByPattern(SecurityDefaults.PermissionsPatternCacheKey);
            _staticCacheManager.RemoveByPattern(SecurityDefaults.PermissionsPatternCacheKey);
        }

        /// <summary>
        /// Install permissions
        /// </summary>
        /// <param name="permissionProvider">Permission provider</param>
        public virtual void InstallPermissions(IPermissionProvider permissionProvider)
        {
            //install new permissions
            var permissions = permissionProvider.GetPermissions();
            //default user role mappings
            var defaultPermissions = permissionProvider.GetDefaultPermissions().ToList();

            foreach (var permission in permissions)
            {
                var permission1 = GetPermissionRecordBySystemName(permission.SystemName);
                if (permission1 != null)
                    continue;

                //new permission (install it)
                permission1 = new PermissionRecord
                {
                    Name = permission.Name,
                    SystemName = permission.SystemName,
                    Category = permission.Category
                };

                foreach (var defaultPermission in defaultPermissions)
                {
                    var claim = _claimRecordRepository.Table.Where(role => role.SystemName.Equals(defaultPermission.CustomerRoleSystemName)).FirstOrDefault();
                    if (claim == null)
                    {
                        //new role (save it)
                        claim = new ClaimRecord
                        {
                            Name = defaultPermission.CustomerRoleSystemName,
                            Active = true,
                            SystemName = defaultPermission.CustomerRoleSystemName
                        };
                        _claimRecordRepository.Insert(claim);
                    }

                    var defaultMappingProvided = (from p in defaultPermission.PermissionRecords
                                                  where p.SystemName == permission1.SystemName
                                                  select p).Any();
                    var mappingExists = (from mapping in claim.PermissionRecordClaimRecordMappings
                                         where mapping.PermissionRecord.SystemName == permission1.SystemName
                                         select mapping.PermissionRecord).Any();
                    if (defaultMappingProvided && !mappingExists)
                    {
                        //permission1.UserRoles.Add(claim);
                        permission1.PermissionRecordClaimRecordMappings.Add(new PermissionRecordClaimRecordMapping { ClaimRecord = claim });
                    }
                }

                //save new permission
                InsertPermissionRecord(permission1);

                //save localization
                //_localizationService.SaveLocalizedPermissionName(permission1);
            }
        }

        /// <summary>
        /// Uninstall permissions
        /// </summary>
        /// <param name="permissionProvider">Permission provider</param>
        public virtual void UninstallPermissions(IPermissionProvider permissionProvider)
        {
            var permissions = permissionProvider.GetPermissions();
            foreach (var permission in permissions)
            {
                var permission1 = GetPermissionRecordBySystemName(permission.SystemName);
                if (permission1 == null) 
                    continue;

                DeletePermissionRecord(permission1);

                //delete permission locales
                //_localizationService.DeleteLocalizedPermissionName(permission1);
            }
        }

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <returns>true - authorized; otherwise, false</returns>
        /*public virtual bool Authorize(PermissionRecord permission)
        {
            return Authorize(permission, _workContext.CurrentCustomer);
        }*/

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <param name="user">User</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(PermissionRecord permission, User user)
        {
            if (permission == null)
                return false;

            if (user == null)
                return false;

            //old implementation of Authorize method
            //var userRoles = user.UserRoles.Where(cr => cr.Active);
            //foreach (var role in userRoles)
            //    foreach (var permission1 in role.PermissionRecords)
            //        if (permission1.SystemName.Equals(permission.SystemName, StringComparison.InvariantCultureIgnoreCase))
            //            return true;

            //return false;

            return Authorize(permission.SystemName, user);
        }

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <returns>true - authorized; otherwise, false</returns>
        /*public virtual bool Authorize(string permissionRecordSystemName)
        {
            return Authorize(permissionRecordSystemName, _workContext.CurrentCustomer);
        }*/

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <param name="user">User</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(string permissionRecordSystemName, User user)
        {
            if (string.IsNullOrEmpty(permissionRecordSystemName))
                return false;

            var userRoles = user.UserRoles.Where(cr => cr.Role.Active);
            foreach (var role in userRoles)
                //if (Authorize(permissionRecordSystemName, role.RoleId))
                    //yes, we have such permission
                    return true;

            //no permission found
            return false;
        }


        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <param name="claimType">Claim type</param>
        /// <param name="claimValue">Claim value</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(string permissionRecordSystemName, string claimType, object claimValue = null)
        {
            if (string.IsNullOrEmpty(permissionRecordSystemName))
                return false;

            var claimRecord = _claimRecordRepository.TableNoTracking.FirstOrDefault(c => c.ClaimType.Equals(claimType));
            if (claimRecord != null)
            {
                var key = string.Format(SecurityDefaults.PermissionsAllowedCacheKey, claimRecord.Id, permissionRecordSystemName);
                // get permission record
                var permissionRecord = _staticCacheManager.Get<PermissionRecord>(key, () =>
                {
                    var permissions = GetPermissionRecordsByClaimRecordId(claimRecord.Id);
                    foreach (var permission in permissions)
                    {
                        if (permission.SystemName.Equals(permissionRecordSystemName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            return permission;
                        }
                    }
                    return null;
                });

                if (permissionRecord != null)
                {
                    var prcrm = permissionRecord.PermissionRecordClaimRecordMappings.First();
                    if (prcrm != null)
                    {
                        if (String.IsNullOrEmpty(prcrm.RequiredClaimValue))
                        {
                            // if permission not require claim value, just enable
                            return true;
                        }
                        else
                        {
                            // if permission require claim value, should compare claim value with permissed value
                            if (prcrm.RequiredClaimValue.Equals(claimValue))
                                return true;
                        }
                    }
                }
            }
            return false;
        }

        #endregion
    }
}