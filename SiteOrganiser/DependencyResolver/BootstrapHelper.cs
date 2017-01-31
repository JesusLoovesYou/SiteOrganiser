using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DependencyResolver
{
    public static class BootstrapHelper
    {
        public static StandardKernel LoadNinjectKernel(IEnumerable<Assembly> assemblies)
        {
            var standardKernel = new StandardKernel();
            foreach (var assembly in assemblies)
            {
                try
                {
                    var types = assembly.GetTypes();
                    var modules = types.Where(t => t.GetInterfaces().Any(i => i.Name == typeof(INinjectModuleBootstrapper).Name)).ToList();
                    modules.ForEach(t =>
                    {
                        var ninjectModuleBootstrapper = (INinjectModuleBootstrapper)Activator.CreateInstance(t);
                        standardKernel.Load(ninjectModuleBootstrapper.GetModules());
                    });
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return standardKernel;
        }
    }
}
