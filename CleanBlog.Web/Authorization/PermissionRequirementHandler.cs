using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CleanBlog.Services.Security;
using Nhx.Core.Entities.Security;
using Nhx.Core.Data;

namespace CleanBlog.Web.Authorization
{
    public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>,
        IAuthorizationRequirement
    {
        private readonly IPermissionService _permissionService;
        private readonly IRepository<ClaimRecord> _claimRecordRepository;

        public PermissionRequirementHandler(IPermissionService permissionService, IRepository<ClaimRecord> claimRecordRepository)
        {
            _permissionService = permissionService;
            _claimRecordRepository = claimRecordRepository;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            // get claims
            var claims = context.User.Claims;
            foreach (var claim in claims)
            {
                if (_permissionService.Authorize(requirement.Permission, claim.Type, claim.Value))
                {
                    context.Succeed(requirement);
                    break;
                }
                /*var claimRecord = _claimRecordRepository.TableNoTracking.FirstOrDefault(c => c.ClaimType.Equals(claim.Type));
                if (claimRecord != null)
                {
                    if (_permissionService.Authorize(requirement.Permission, claimRecord.Id))
                    {
                        context.Succeed(requirement);
                        break;
                    }
                }*/
                //if (claims.FirstOrDefault(c => c.Value.Equals(requirement.Permission)) != null)
                //{
                    //context.Succeed(requirement);
                //}
            }

            return Task.CompletedTask;
        }
    }
}
