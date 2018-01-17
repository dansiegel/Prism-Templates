using Company.MobileApp.ModuleName.Services;
using Company.MobileApp.ModuleName.Views;
using Prism.Ioc;
using Prism.Modularity;
#if (AutofacContainer)
using Autofac;
using Prism.Autofac;
#endif
#if (DryIocContainer)
using DryIoc;
using Prism.DryIoc;
#endif
#if (UnityContainer)
using Unity;
using Prism.Unity;
#endif
using Xamarin.Forms;

namespace Company.MobileApp.ModuleName
{
    public class Company_MobileApp_ModuleName : IModule
    {
        // Deprecated
        public void Initialize() { }

        public void OnInitialized()
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IFooService, FooService>();
            containerRegistry.RegisterForNavigation<ViewA>();
        }
    }
}