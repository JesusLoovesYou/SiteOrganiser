using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using SiteOrganiser.Common.System;

namespace SiteOrganiser.Business
{
    public class CustomPrincipal : ICustomPrincipal
    {
        private IIdentity _identity;

        public CustomPrincipal(IIdentity identity)
        {
            _identity = identity;
        }

        public CustomPrincipal(IIdentity identity, Guid userId, string userName, List<IRole> roles)
            : this(identity)
        {
            UserId = userId;
            UserName = userName;
            UserRoles = roles;
        }

        #region Implementation of IPrincipal
        public bool IsInRole(string role)
        {
            return UserRoles.Any(r => r.RoleName.Equals(role));
        }
        public IIdentity Identity
        {
            get { return _identity; }
        }
        #endregion

        #region Implementation of ICustomPrincipal

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public List<IRole> UserRoles { get; set; }

        #endregion
    }

    public class CustomIdentity : IIdentity
    {
        private bool _isAuthenticated;
        private string _name;
        private string _authenticationType = string.Empty;

        public CustomIdentity(bool isAuthenticated, string name)
        {
            _isAuthenticated = isAuthenticated;
            _name = name;
        }

        #region Implementation of IIdentity
        public string Name
        {
            get { return _name; }
        }
        public string AuthenticationType
        {
            get { return _authenticationType; }
        }
        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
        }
        #endregion
    }
}
