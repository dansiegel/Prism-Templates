using System;
#if (UseAzureMobileClient)
using AzureMobileClient.Helpers.Accounts;
#endif
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
using Company.MobileApp.i18n;
using Company.MobileApp.Droid.i18n;
#endif

namespace Company.MobileApp.Droid
{
    public class AndroidInitializer : IPlatformInitializer
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
#if(AutofacContainer)
            var builder = new ContainerBuilder();
#endif
#if (Localization && AutofacContainer)
            builder.Register(ctx => new Localize()).As<ILocalize>().SingleInstance();
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
#if (UseAzureMobileClient && AutofacContainer)
            builder.Register(ctx => new SecureStore()).As<ISecureStore>().SingleInstance();
#endif
#if (UseAzureMobileClient && DryIocContainer)
            container.Register<ISecureStore, SecureStore>(Reuse.Singleton);
#endif
#if (UseAzureMobileClient && NinjectContainer)
            container.Bind<ISecureStore>().To<SecureStore>().InSingletonScope();
#endif
#if (UseAzureMobileClient && UnityContainer)
            container.RegisterType<ISecureStore, SecureStore>(new ContainerControlledLifetimeManager());
#endif
#if (AutofacContainer)
            builder.Update(container);
#endif
        }
    }
}
