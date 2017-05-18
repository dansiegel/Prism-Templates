using AzureMobileClient.Helpers;
using MobileApp.Helpers;
using MobileApp.Models;
#if (DryIocContainer)
using DryIoc;
#elseif (UnityContainer)
using Microsoft.Practices.Unity;
#endif

namespace MobileApp.Data
{
#if (DryIocContainer)
    // To use authentication inherit from DryIocCloudServiceContext
    public class AppDataContext : DryIocCloudAppContext, IAppDataContext
#elseif (UnityContainer)
    // To use authentication inherit from AzureCloudServiceContext
    public class AppDataContext : AzureCloudAppContext, IAppDataContext
#endif
    {
#if (DryIocContainer)
        public AppDataContext(IContainer container)
            : base(container)
        {

        }
#elseif (UnityContainer)
        private IUnityContainer _container { get; }

        // Be sure to remove the reference to the IMobileServiceClient if using authentication
        public AppDataContext(IUnityContainer container, IMobileServiceClient client)
            : base(client) // you can optionally pass in the data store name
        {
            _container = container;
        }
#endif
        // Any ICloudSyncTable's that you have here will be automatically registered with the local store.
        public ICloudSyncTable<TodoItem> TodoItems => SyncTable<TodoItem>();
#if (UnityContainer)

        public override ICloudSyncTable<T> SyncTable<T>() =>
            Container.Resolve<ICloudSyncTable<T>>();

        public override ICloudTable<T> Table<T>() =>
            Container.Resolve<ICloudTable<T>>();
#endif
    }
}