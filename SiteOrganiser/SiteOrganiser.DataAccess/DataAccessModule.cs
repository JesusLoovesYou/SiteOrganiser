using SiteOrganiser.Common.DAL;
using SiteOrganiser.Common.DBServer;
using Ninject.Modules;

namespace SiteOrganiser.DataAccess
{
    public class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IServerDAL>().To<ServerDAL>();
            Bind<IUserDAL>().To<UserDAL>();
            Bind<IUserManagementDAL>().To<UserManagementDAL>();
        }
    }
}
