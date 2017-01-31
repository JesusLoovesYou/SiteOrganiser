using DependencyResolver;
using SiteOrganiser.Business.Modules;
using Ninject.Modules;
using System.Collections.Generic;

namespace SiteOrganiser.Business
{
    public class BusinessBootstrapper : INinjectModuleBootstrapper
    {
        public IList<INinjectModule> GetModules()
        {
            return new List<INinjectModule>()
            {
                new ServerModule(),
                new AdminModule()
            };
        }
    }
}
