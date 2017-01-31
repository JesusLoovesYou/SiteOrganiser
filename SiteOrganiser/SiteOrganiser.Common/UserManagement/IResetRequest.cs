using System;

namespace SiteOrganiser.Common.UserManagement
{
    public interface IResetRequest
    {
        string UserEmail { get; set; }
        DateTime RequestDate { get; set; }
        string ResetGuid { get; set; }
    }
}
