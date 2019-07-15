using System;
using System.Reflection;
using Ninject;

namespace MyParking.Droid.DependencyInjection
{
    public class ServiceLocatorApp
    {
        private static IKernel kernel;

        public static void SetupKernel()
        {
            if (kernel == null)
            {
                var settings = new NinjectSettings { LoadExtensions = false };
                kernel = new StandardKernel(settings);
                kernel.Load(Assembly.GetExecutingAssembly());
            }
        }

        public static T Get<T>()
        {
            if (kernel == null)
            {
                throw new InvalidOperationException();
            }
            return kernel.Get<T>();
        }
    }
}
