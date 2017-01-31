using SiteOrganiser.Common.Business;
using Ninject.Modules;

namespace SiteOrganiser.Business.Modules
{
    public class AdminModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAdminBusiness>().To<AdminBusiness>();
        }
    }
}
