using System;
using System.Collections.Generic;
using System.Text;

namespace CleanBlog.Services.Security
{
    public static class PermissionTypes
    {

        public class Common
        {
            public const string AccessAdminPanel = "Permission.Common.AccessAdminPanel";
        }

        public class ManageUsers
        {
            public const string ViewList = "Permission.ManageUsers.ViewList";
            public const string Create = "Permission.ManageUsers.Create";
            public const string Edit = "Permission.ManageUsers.Edit";
            public const string Delete = "Permission.ManageUsers.Delete";
        }

        public class ManageBlogPosts
        {
            public const string ViewList = "Permission.ManageBlogPosts.ViewList";
            public const string Create = "Permission.ManageBlogPosts.Create";
            public const string Edit = "Permission.ManageBlogPosts.Edit";
            public const string Delete = "Permission.ManageBlogPosts.Delete";
        }
    }
}
