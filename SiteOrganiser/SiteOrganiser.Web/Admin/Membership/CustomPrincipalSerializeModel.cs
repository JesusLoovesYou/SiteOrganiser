using System;
using System.Collections.Generic;

namespace SiteOrganiser.Web.Admin.Membership
{
    public class CustomPrincipalSerializeModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public List<Role> UserRoles { get; set; }
    }
}