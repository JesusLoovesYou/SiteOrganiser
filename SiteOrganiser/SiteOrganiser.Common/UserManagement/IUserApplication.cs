using System;

namespace SiteOrganiser.Common.UserManagement
{
    public interface IUserApplication
    {
        string UserEmail { get; set; }
        string UserPassword { get; set; }
        string VerifyGuid { get; set; }
        DateTime ApplicationDate { get; set; }
    }
}
