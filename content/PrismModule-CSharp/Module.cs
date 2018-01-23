using Company.MobileApp.ModuleName.Services;
using Company.MobileApp.ModuleName.Views;
using Prism.Ioc;
using Prism.Modularity;
using Xamarin.Forms;

namespace Company.MobileApp.ModuleName
{
    public class Company_MobileApp_ModuleName : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            // Handle post initialization tasks like resolving IEventAggregator to subscribe events
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IFooService, FooService>();
            containerRegistry.RegisterForNavigation<ViewA>();
        }
    }
}