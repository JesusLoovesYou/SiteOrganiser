using DependencyResolver;
using Ninject.Modules;
using System.Collections.Generic;

namespace SiteOrganiser.DataAccess
{
    public class DataAccessBootstrapper : INinjectModuleBootstrapper
    {
        public IList<INinjectModule> GetModules()
        {
            return new List<INinjectModule>()
            {
                new DataAccessModule()
            };
        }
    }
}
