using System;
using SiteOrganiser.Common.System;

namespace SiteOrganiser.DataAccess.DataEntities
{
    public class Role : IRole
    {
        public Role() { }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
