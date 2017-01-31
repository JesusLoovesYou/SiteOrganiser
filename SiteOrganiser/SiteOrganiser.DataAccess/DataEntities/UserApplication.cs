using System;
using SiteOrganiser.Common.UserManagement;

namespace SiteOrganiser.DataAccess.DataEntities
{
    public class UserApplication : IUserApplication
    {
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string VerifyGuid { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}
