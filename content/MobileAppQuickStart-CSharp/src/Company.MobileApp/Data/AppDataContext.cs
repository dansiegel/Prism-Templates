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
#if (NoAuth)
     #if (DryIocContainer)
    // If you do want to use authentication inherit from DryIocCloudServiceContext
    public class AppDataContext : DryIocCloudAppContext, IAppDataContext
    #else
    // If you do want to use authentication inherit from AzureCloudServiceContext
    public class AppDataContext : AzureCloudAppContext, IAppDataContext
    #endif
#else
    #if (DryIocContainer)
    // If you don't want to use authentication inherit from DryIocCloudAppContext
    public class AppDataContext : DryIocCloudServiceContext, IAppDataContext
    #else
    // If you don't want to use authentication inherit from AzureCloudAppContext
    public class AppDataContext : AzureCloudServiceContext, IAppDataContext
    #endif
#endif
    {
#if (AutofacContainer)
        private IContainer _container { get; }

#elseif (NinjectContainer)
        private IReadOnlyKernel _kernel { get; }

#elseif (UnityContainer)
        private IUnityContainer _container { get; }

#endif
#if (NoAuth)
    #if (AutofacContainer)
        public AppDataContext(IContainer container, IMobileServiceClient client) 
            : base(client) // you can optionally pass in the data store name
        {
            _container = container;
        }
    #elseif (DryIocContainer)
        public AppDataContext(IContainer container) 
            : base(container) // you can optionally pass in the data store name
        {
        }
    #elseif (NinjectContainer)
        public AppDataContext(IReadOnlyKernel kernel, IMobileServiceClient client) 
            : base(client) // you can optionally pass in the data store name
        {
            _kernel = kernel;
        }
    #elseif (UnityContainer)
        public AppDataContext(IUnityContainer container, IMobileServiceClient client) 
            : base(client) // you can optionally pass in the data store name
        {
            _container = container;
        }
    #endif
#else
    #if (AutofacContainer)
        public AppDataContext(IContainer container, IAzureCloudServiceOptions options, ILoginProvider loginProvider) 
            : base(options, loginProvider) // you can optionally pass in the data store name
        {
            _container = container;
        }
    #elseif (DryIocContainer)
       public AppDataContext(IContainer container, IAzureCloudServiceOptions options, ILoginProvider loginProvider) 
            : base(container, options, loginProvider) // you can optionally pass in the data store name
        {
        }
    #elseif (NinjectContainer)
        public AppDataContext(IReadOnlyKernel kernel, IAzureCloudServiceOptions options, ILoginProvider loginProvider) 
            : base(options, loginProvider) // you can optionally pass in the data store name
        {
            _kernel = kernel;
        }
    #elseif (UnityContainer)
        public AppDataContext(IUnityContainer container, IAzureCloudServiceOptions options, ILoginProvider loginProvider) 
            : base(options, loginProvider) // you can optionally pass in the data store name
        {
            _container = container;
        }
    #endif
#endif

        // Any ICloudSyncTable's that you have here will be automatically registered with the local store.
#if (Empty)
        // TODO: Create ICloudSyncTable<Model> Models { get; }
#else
        public ICloudSyncTable<TodoItem> TodoItems => SyncTable<TodoItem>();
#endif
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