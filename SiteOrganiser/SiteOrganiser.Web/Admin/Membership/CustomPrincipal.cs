using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using SiteOrganiser.Common.System;

namespace SiteOrganiser.Web.Admin.Membership
{
    public class CustomPrincipal : ICustomPrincipal
    {
        public CustomPrincipal(Guid userId, string userName, List<IRole> roles)
        {
            UserId = userId;
            UserName = userName;
            UserRoles = roles;
            this.Identity = new GenericIdentity(userName);
        }

        #region Implementation of IPrincipal

        public bool IsInRole(string role)
        {
            return UserRoles.Any(r => r.RoleName.Equals(role));
        }

        public IIdentity Identity { get; private set; }

        #endregion

        #region Implementation of ICustomPrincipal

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public List<IRole> UserRoles { get; set; }

        #endregion
    }
}