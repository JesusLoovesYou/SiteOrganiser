using SiteOrganiser.Common.Messages;

namespace SiteOrganiser.Common.Business
{
    public interface IUserManagementBusiness
    {
        IBLMessage SendApplication(string userEmail, string userPassword, string title, string content);
        IBLMessage VerifyApplication(string verifyGuid);
        IBLMessage SendResetLink(string userEmail, string title, string content);
        bool CheckResetPasswordGuid(string resetGuid);
        IBLMessage ResetPassword(string resetGuid, string newPassword);
    }
}
