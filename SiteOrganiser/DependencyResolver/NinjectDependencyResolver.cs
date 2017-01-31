using Ninject;
using Ninject.Parameters;

namespace DependencyResolver
{
    public static class NinjectDependencyResolver
    {
        private static IKernel _kernel;

        static NinjectDependencyResolver() { }

        public static void SetKernel(IKernel kernel)
        {
            _kernel = kernel;
        }
        public static T GetBL<T>()
        {
            T bl = _kernel.Get<T>();
            return bl;
        }
        public static T GetBL<T>(object database)
        {
            T bl = _kernel.Get<T>(new IParameter[] { new ConstructorArgument("database", database) });
            return bl;
        }

        public static IKernel CurrentKernel
        {
            get
            {
                return _kernel;
            }
        }
    }
}
