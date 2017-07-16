using System;
#if (UseAzureMobileClient)
using Android.App;
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
#if (AADAuth || AADB2CAuth)
using Microsoft.Identity.Client;
#endif
#if (Localization)
using Company.MobileApp.i18n;
using Company.MobileApp.Droid.i18n;
#endif

namespace Company.MobileApp.Droid
{
    public class AndroidInitializer : IPlatformInitializer
    {
#if (UseAzureMobileClient)
        private Application CurrentApplication { get; }

        public AndroidInitializer(Application application)
        {
            CurrentApplication = application;
        }

#endif
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
#if (Localization)
  #if (AutofacContainer)
            builder.Register(ctx => new Localize()).As<ILocalize>().SingleInstance();
  #elseif (DryIocContainer)
            container.Register<ILocalize, Localize>(Reuse.Singleton);
  #elseif (NinjectContainer)
            kernel.Bind<ILocalize>().To<Localize>().InSingletonScope();
  #elseif (UnityContainer)
            container.RegisterType<ILocalize, Localize>(new ContainerControlledLifetimeManager());
  #endif
#endif
#if (UseAzureMobileClient)
  #if (AutofacContainer)
            builder.RegisterInstance(CurrentApplication).As<Application>().SingleInstance();
            builder.Register(ctx => new SecureStore()).As<ISecureStore>().SingleInstance();
  #elseif (DryIocContainer)
            container.UseInstance(CurrentApplication);
            container.Register<ISecureStore, SecureStore>(Reuse.Singleton);
  #elseif (NinjectContainer)
            kernel.Bind<Application>().ToConstant(CurrentApplication).InSingletonScope();
            kernel.Bind<ISecureStore>().To<SecureStore>().InSingletonScope();
  #elseif (UnityContainer)
            container.RegisterInstance(CurrentApplication);
            container.RegisterType<ISecureStore, SecureStore>(new ContainerControlledLifetimeManager());
  #endif
#endif
#if (AADAuth || AADB2CAuth) 
  #if (AutofacContainer)
            builder.RegisterInstance(new UIParent(Xamarin.Forms.Forms.Context as Activity)).As<UIParent>().SingleInstance();
  #elseif (DryIocContainer)
            container.UseInstance(new UIParent(Xamarin.Forms.Forms.Context as Activity));
  #elseif (NinjectContainer)
            kernel.Bind<UIParent>().ToConstant(new UIParent(Xamarin.Forms.Forms.Context as Activity)).InSingletonScope();
  #elseif (UnityContainer)
            container.RegisterInstance(new UIParent(Xamarin.Forms.Forms.Context as Activity));
  #endif
#endif
#if (AutofacContainer)

            builder.Update(container);
#endif
        }
    }
}
