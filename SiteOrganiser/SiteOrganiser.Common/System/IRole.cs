using System;

namespace SiteOrganiser.Common.System
{
    public interface IRole
    {
        Guid RoleId { get; set; }
        string RoleName { get; set; }
    }
}
