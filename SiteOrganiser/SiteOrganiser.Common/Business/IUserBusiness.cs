using SiteOrganiser.Common.Messages;

namespace SiteOrganiser.Common.Business
{
    public interface IUserBusiness
    {
        ILoginMessages LoginUser(string userName, string password);
    }
}
