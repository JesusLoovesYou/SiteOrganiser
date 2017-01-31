using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace SiteOrganiser.Common.System
{
    public interface ICustomPrincipal : IPrincipal
    {
        Guid UserId { get; set; }
        string UserName { get; set; }
        List<IRole> UserRoles { get; set; }
    }
}
