using System;
using SiteOrganiser.Common.System;

namespace SiteOrganiser.Web.Admin.Membership
{
    public class Role : IRole
    {
        public Role() { }

        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}