using System;
#if (UseAzureMobileClient || AADAuth || AADB2CAuth)
using Android.App;
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
#if (AutofacContainer)
        public void RegisterTypes(ContainerBuilder builder)
#elseif (DryIocContainer)
        public void RegisterTypes(IContainer container)
#elseif (NinjectContainer)
        public void RegisterTypes(IKernel kernel)
#elseif (UnityContainer)
        public void RegisterTypes(IUnityContainer container)
#endif
        {
            // Register Any Platform Specific Implementations that you cannot 
            // access from Shared Code
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
        }
    }
}
