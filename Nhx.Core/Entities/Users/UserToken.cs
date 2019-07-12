using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Nhx.Core.Entities.Users
{
    /// <summary>
    /// Represents a user token
    /// </summary>
    public class UserToken: IdentityUserToken<int>
    {
        public virtual User User { get; set; }
    }
}
