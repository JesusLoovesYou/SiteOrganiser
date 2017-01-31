using System.Collections.Generic;
using SiteOrganiser.Common.Messages;
using SiteOrganiser.Common.System;

namespace SiteOrganiser.Common.DAL
{
    public interface IUserDAL
    {
        ICredential GetUserCredentials(string userName);
        List<IRole> GetUserRoles(string userId);
    }
}
