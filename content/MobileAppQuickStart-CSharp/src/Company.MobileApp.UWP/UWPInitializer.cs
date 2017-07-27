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
#if (AADAuth || AADB2CAuth)
using Microsoft.Identity.Client;
#endif
#if (Localization)
using Company.MobileApp.i18n;
using Company.MobileApp.iOS.i18n;
#endif

namespace Company.MobileApp.UWP
{
    public class UWPInitializer : IPlatformInitializer
    {
#if (AutofacContainer || DryIocContainer)
        public void RegisterTypes(IContainer container)
#elseif (NinjectContainer)
        public void RegisterTypes(IKernel kernel)
#elseif (UnityContainer)
        public void RegisterTypes(IUnityContainer container)
#endif
        {
            // Register Any Platform Specific Implementations that you cannot 
            // access from Shared Code
#if(AutofacContainer)
            var builder = new ContainerBuilder();
#endif
#if (UseAzureMobileClient)
  #if (AutofacContainer)
            builder.Register(ctx => new SecureStore()).As<ISecureStore>().SingleInstance();
  #elseif (DryIocContainer)
            container.Register<ISecureStore, SecureStore>(Reuse.Singleton);
  #elseif (NinjectContainer)
            kernel.Bind<ISecureStore>().To<SecureStore>().InSingletonScope();
  #elseif (UnityContainer)
            container.RegisterType<ISecureStore, SecureStore>(new ContainerControlledLifetimeManager());
  #endif
#endif
#if (AADAuth || AADB2CAuth) 
  #if (AutofacContainer)
            builder.RegisterInstance(new UIParent()).As<UIParent>().SingleInstance();
  #elseif (DryIocContainer)
            container.UseInstance(new UIParent());
  #elseif (NinjectContainer)
            kernel.Bind<UIParent>().ToConstant(new UIParent()).InSingletonScope();
  #elseif (UnityContainer)
            container.RegisterInstance(new UIParent());
  #endif
#endif
#if (AutofacContainer)

            builder.Update(container);
#endif
        }
    }
}