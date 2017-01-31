using SiteOrganiser.Common.Messages;
using SiteOrganiser.Common.UserManagement;

namespace SiteOrganiser.Common.DAL
{
    public interface IUserManagementDAL
    {
        IBLMessage SaveApplication(string userEmail, string userPassword);
        IBLMessage DeleteApplication(string userEmail);
        void DeleteCustomer(string userEmail);
        IUserApplication GetApplicationByEmail(string userEmail);
        IUserApplication GetApplicationByVerifyGuid(string verifyGuid);
        IBLMessage SaveResetRequest(string userEmail);
        IResetRequest GetResetRequestByEmail(string userEmail);
        IResetRequest GetResetRequestByResetGuid(string resetGuid);
        IBLMessage DeleteResetRequest(string resetGuid);
        void SetCustomerInfo(string userEmail);
    }
}
