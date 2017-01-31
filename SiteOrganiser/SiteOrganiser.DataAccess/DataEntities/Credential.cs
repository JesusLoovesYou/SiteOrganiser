using System;
using System.Collections.Generic;
using SiteOrganiser.Common.Messages;
using SiteOrganiser.Common.System;

namespace SiteOrganiser.DataAccess.DataEntities
{
    public class Credential : ICredential
    {
        private bool _success;

        public Credential(Guid userId, string userName, List<IRole> roles, bool success)
        {
            UserId = userId;
            UserName = userName;
            UserRoles = roles;
            _success = success;
        }

        #region Implementation of ICredential
        public Guid UserId { get; private set; }
        public string UserName { get; private set; }
        public List<IRole> UserRoles { get; private set; }

        public bool Success
        {
            get { return _success; }
        }
        #endregion
    }
}
