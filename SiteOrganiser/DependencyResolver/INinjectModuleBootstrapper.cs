using Ninject.Modules;
using System.Collections.Generic;

namespace DependencyResolver
{
    public interface INinjectModuleBootstrapper
    {
        IList<INinjectModule> GetModules();
    }
}
