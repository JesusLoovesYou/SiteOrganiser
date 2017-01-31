using SiteOrganiser.Common.Management;
using SiteOrganiser.Common.Messages;
using System.Collections.Generic;

namespace SiteOrganiser.Common.Business
{
    public interface IAdminBusiness
    {
        IBLMessage ChangeUserLock(string userEmail);
        IBLMessage CreateUser(string userEmail, string newPassword);
        IBLMessage UpdateAccountInfo(string userEmail, string oldPassword, string newPassword, string userKey);
        IRegisteredUser GetUser(string userEmail);
        IBLMessage EditUser(string userEmail, string newPassword, string userKey);
        IBLMessage DeleteUser(string userEmail);
        List<IRegisteredUser> GetMembers();
    }
}
