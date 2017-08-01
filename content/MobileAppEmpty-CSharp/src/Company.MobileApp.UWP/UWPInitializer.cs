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

namespace Company.MobileApp.UWP
{
    public class UWPInitializer : IPlatformInitializer
    {
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
        }
    }
}