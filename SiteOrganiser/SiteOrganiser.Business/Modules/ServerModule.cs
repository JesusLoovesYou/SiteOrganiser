using SiteOrganiser.Common.Business;
using Ninject.Modules;

namespace SiteOrganiser.Business.Modules
{
    public class ServerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IServerBusiness>().To<ServerBusiness>();
        }
    }
}
