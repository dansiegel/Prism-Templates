using AzureMobileClient.Helpers;
using Company.MobileApp.Helpers;
using Company.MobileApp.Models;
#if (AutofacContainer)
using Autofac;
#elseif (DryIocContainer)
using DryIoc;
#elseif (NinjectContainer)
using Ninject;
#elseif (UnityContainer)
using Microsoft.Practices.Unity;
#endif

namespace Company.MobileApp.Data
{
#if (DryIocContainer)
    // If you don't want to use authentication inherit from DryIocCloudAppContext
    public class AppDataContext : DryIocCloudServiceContext, IAppDataContext
#elseif (UnityContainer)
    // If you don't want to use authentication inherit from AzureCloudAppContext
    public class AppDataContext : AzureCloudServiceContext, IAppDataContext
#endif
    {
#if (AutofacContainer)
        private IContainer _container { get; }

        public AppDataContext(IContainer container, IAzureCloudServiceOptions options, ILoginProvider loginProvider, string offlineDbPath = "azureCloudAppContext.db") 
            : base(options, loginProvider, offlineDbPath)
        {
            _container = container;
        }

#elseif (DryIocContainer)
       public AppDataContext(IContainer container, IAzureCloudServiceOptions options, ILoginProvider loginProvider, string offlineDbPath = "azureCloudAppContext.db") 
            : base(container, options, loginProvider, offlineDbPath)
        {
        }

#elseif (NinjectContainer)
        private IKernel _kernel { get; }

        public AppDataContext(IKernel kernel, IAzureCloudServiceOptions options, ILoginProvider loginProvider, string offlineDbPath = "azureCloudAppContext.db") 
            : base(options, loginProvider, offlineDbPath)
        {
            _kernel = kernel;
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
#if (AutofacContainer)

        public override ICloudSyncTable<T> SyncTable<T>() =>
            _container.Resolve<ICloudSyncTable<T>>();

        public override ICloudTable<T> Table<T>() =>
            _container.Resolve<ICloudTable<T>>();
#elseif (NinjectContainer)

        public override ICloudSyncTable<T> SyncTable<T>() =>
            _kernel.Get<ICloudSyncTable<T>>();

        public override ICloudTable<T> Table<T>() =>
            _kernel.Get<ICloudTable<T>>();
#elseif (UnityContainer)

        public override ICloudSyncTable<T> SyncTable<T>() =>
            Container.Resolve<ICloudSyncTable<T>>();

        public override ICloudTable<T> Table<T>() =>
            Container.Resolve<ICloudTable<T>>();
#endif
    }
}