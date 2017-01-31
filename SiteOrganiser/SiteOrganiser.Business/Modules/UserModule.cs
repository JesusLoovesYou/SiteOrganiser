using SiteOrganiser.Common.Business;
using Ninject.Modules;

namespace SiteOrganiser.Business.Modules
{
    public class UserModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserBusiness>().To<UserBusiness>();
            Bind<IUserManagementBusiness>().To<UserManagementBusiness>();
        }
    }
}
