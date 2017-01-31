using System;
using System.Collections.Generic;

namespace SiteOrganiser.Common.System
{
    public interface IUser
    {
        Guid UserId { get; set; }
        string UserName { get; set; }
        DateTime LastActivityDate { get; set; }
        List<IRole> UserRoles { get; set; }
    }
}
