using System;
using SiteOrganiser.Common.UserManagement;

namespace SiteOrganiser.DataAccess.DataEntities
{
    public class ResetRequest : IResetRequest
    {
        public string UserEmail { get; set; }
        public DateTime RequestDate { get; set; }
        public string ResetGuid { get; set; }
    }
}
