using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Nhx.Core.Entities.Users
{
    /// <summary>
    /// Represents a user login
    /// </summary>
    public class UserLogin: IdentityUserLogin<int>
    {
        public virtual User User { get; set; }
    }
}
