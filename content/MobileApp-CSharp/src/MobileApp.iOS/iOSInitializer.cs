using System;
#if (AutofacContainer)
using Autofac;
using Prism.Autofac;
#elif (DryIocContainer)
using DryIoc;
using Prism.DryIoc;
#elif (NinjectContainer)
using Ninject;
using Prism.Ninject;
#else
using Microsoft.Practices.Unity;
using Prism.Unity;
#endif
#if (Localization)
using MobileApp.i18n;
using MobileApp.iOS.i18n;
#endif

namespace MobileApp.iOS
{
    public class iOSInitializer : IPlatformInitializer
    {
#if (AutofacContainer || DryIocContainer)
        public void RegisterTypes(IContainer container)
#elif (NinjectContainer)
        public void RegisterTypes(IKernel kernel)
#else
        public void RegisterTypes(IUnityContainer container)
#endif
        {
            // Register Any Platform Specific Implementations that you cannot 
            // access from Shared Code
#if (AutofacContainer && Localization)
            var builder = new ContainerBuilder();
            builder.Register(ctx => new Localize()).As<ILocalize>().SingleInstance();
            builder.Update(container);
#elif (DryIocContainer && Localization)
            container.Register<ILocalize, Localize>(Reuse.Singleton);
#elif (NinjectContainer && Localization)
            container.Bind<ILocalize>().To<Localize>().InSingletonScope();
#elif (Localization)
            container.RegisterType<ILocalize, Localize>(new ContainerControlledLifetimeManager());
#endif
        }
    }
}
