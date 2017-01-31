using System;
using System.Collections.Generic;
using SiteOrganiser.Common.System;

namespace SiteOrganiser.DataAccess.DataEntities
{
    public class User : IUser
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public DateTime LastActivityDate { get; set; }
        public List<IRole> UserRoles { get; set; }
    }
}
