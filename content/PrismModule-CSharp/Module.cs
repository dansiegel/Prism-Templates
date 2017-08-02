using Company.MobileApp.ModuleName.Services;
using Company.MobileApp.ModuleName.Views;
using Prism.Modularity;
#if (AutofacContainer)
using Autofac;
using Prism.Autofac;
#endif
#if (DryIocContainer)
using DryIoc;
using Prism.DryIoc;
#endif
#if (NinjectContainer)
using Ninject;
using Prism.Ninject;
#endif
#if (UnityContainer)
using Microsoft.Practices.Unity;
using Prism.Unity;
#endif
using Xamarin.Forms;

namespace Company.MobileApp.ModuleName
{
    public class Company_MobileApp_ModuleName : IModule
    {
#if (AutofacContainer)
        public void Initialize() { }

        public static void Initialize(ContainerBuilder builder)
        {
            builder.Register<IFooService, FooService>();

            builder.RegisterTypeForNavigation<ViewA>();
        }
#elseif (DryIocContainer)
        private IContainer _container { get; }

        public Company_MobileApp_ModuleName(IContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.Register<IFooService, FooService>();

            _container.RegisterTypeForNavigation<ViewA>();
        }
#elseif (NinjectContainer)
        private IKernel _kernel { get; }

        public Company_MobileApp_ModuleName(IKernel kernel)
        {
            _kernel = kernel;
        }

        public void Initialize()
        {
            _kernel.Bind<IFooService>().To<FooService>();

            _kernel.RegisterTypeForNavigation<ViewA>();
        }
#elseif (UnityContainer)
        private IUnityContainer _container { get; }

        public Company_MobileApp_ModuleName(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<IFooService, FooService>();

            _container.RegisterTypeForNavigation<ViewA>();
        }
#endif
    }
}