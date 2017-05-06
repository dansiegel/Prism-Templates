using System;
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
#if (Localization)
using MobileApp.i18n;
using MobileApp.iOS.i18n;
#endif

namespace MobileApp.UWP
{
    public class UWPInitializer : IPlatformInitializer
    {
#if (AutofacContainer || DryIocContainer)
        public void RegisterTypes(IContainer container)
#endif
#if (NinjectContainer)
        public void RegisterTypes(IKernel kernel)
#endif
#if (UnityContainer)
        public void RegisterTypes(IUnityContainer container)
#endif
        {
            // Register Any Platform Specific Implementations that you cannot 
            // access from Shared Code
#if (Localization && AutofacContainer)
            var builder = new ContainerBuilder();
            builder.Register(ctx => new Localize()).As<ILocalize>().SingleInstance();
            builder.Update(container);
#endif
#if (Localization && DryIocContainer)
            container.Register<ILocalize, Localize>(Reuse.Singleton);
#endif
#if (Localization && NinjectContainer)
            container.Bind<ILocalize>().To<Localize>().InSingletonScope();
#endif
#if (Localization && UnityContainer)
            container.RegisterType<ILocalize, Localize>(new ContainerControlledLifetimeManager());
#endif
        }
    }
}