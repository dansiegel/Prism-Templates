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

namespace MobileApp.iOS
{
    public class iOSInitializer : IPlatformInitializer
    {
#if (NinjectContainer)
        public void RegisterTypes(IKernel kernel)
#elif (UnityContainer)
        public void RegisterTypes(IUnityContainer container)
#else
        public void RegisterTypes(IContainer container)
#endif
        {
            // Register Any Platform Specific Implementations that you cannot 
            // access from Shared Code
        }
    }
}
