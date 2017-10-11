using AzureMobileClient.Helpers;
using Company.MobileApp.Auth;
using Company.MobileApp.Helpers;
using Company.MobileApp.Models;
#if (AutofacContainer)
using Autofac;
using Microsoft.WindowsAzure.MobileServices;
#elseif (DryIocContainer)
using DryIoc;
#elseif (NinjectContainer)
using Microsoft.WindowsAzure.MobileServices;
using Ninject;
#elseif (UnityContainer)
using Microsoft.Practices.Unity;
using Microsoft.WindowsAzure.MobileServices;
#endif

namespace Company.MobileApp.Data
{
#if (NoAuth)
    #if (AutofacContainer)
    // If you do want to use authentication inherit from AutofacCloudServiceContext
    public class AppDataContext : AutofacCloudAppContext, IAppDataContext
    #elseif (DryIocContainer)
    // If you do want to use authentication inherit from DryIocCloudServiceContext
    public class AppDataContext : DryIocCloudAppContext, IAppDataContext
    #elseif (UnityContainer)
    // If you do want to use authentication inherit from UnityCloudServiceContext
    public class AppDataContext : UnityCloudAppContext, IAppDataContext
    #else
    // If you do want to use authentication inherit from AzureCloudServiceContext
    public class AppDataContext : AzureCloudAppContext, IAppDataContext
    #endif
#else
    #if (AutofacContainer)
    // If you don't want to use authentication inherit from AutofacCloudAppContext
    public class AppDataContext : AutofactCloudServiceContext<MobileAppUser>, IAppDataContext
    #elseif (DryIocContainer)
    // If you don't want to use authentication inherit from DryIocCloudAppContext
    public class AppDataContext : DryIocCloudServiceContext<MobileAppUser>, IAppDataContext
    #elseif (UnityContainer)
    // If you don't want to use authentication inherit from UnityCloudAppContext
    public class AppDataContext : UnityCloudServiceContext<MobileAppUser>, IAppDataContext
    #else
    // If you don't want to use authentication inherit from AzureCloudAppContext
    public class AppDataContext : AzureCloudServiceContext<MobileAppUser>, IAppDataContext
    #endif
#endif
    {
#if (NinjectContainer)
        private IReadOnlyKernel _kernel { get; }

#endif
#if (NoAuth)
    #if (AutofacContainer)
        public AppDataContext(IComponentContext context) 
            : base(context) // you can optionally pass in the data store name
        {
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
        public AppDataContext(IUnityContainer container) 
            : base(container) // you can optionally pass in the data store name
        {
        }
    #endif
#else
    #if (AutofacContainer)
        public AppDataContext(IComponentContext context, IAzureCloudServiceOptions options, ILoginProvider<MobileAppUser> loginProvider) 
            : base(context, options, loginProvider) // you can optionally pass in the data store name
        {
        }
    #elseif (DryIocContainer)
       public AppDataContext(IContainer container, IAzureCloudServiceOptions options, ILoginProvider<MobileAppUser> loginProvider) 
            : base(container, options, loginProvider) // you can optionally pass in the data store name
        {
        }
    #elseif (NinjectContainer)
        public AppDataContext(IReadOnlyKernel kernel, IAzureCloudServiceOptions options, ILoginProvider<MobileAppUser> loginProvider) 
            : base(options, loginProvider) // you can optionally pass in the data store name
        {
            _kernel = kernel;
        }
    #elseif (UnityContainer)
        public AppDataContext(IUnityContainer container, IAzureCloudServiceOptions options, ILoginProvider<MobileAppUser> loginProvider) 
            : base(container, options, loginProvider) // you can optionally pass in the data store name
        {
        }
    #endif
#endif

        // Any ICloudSyncTable's that you have here will be automatically registered with the local store.
#if (Empty)
        // TODO: Create ICloudSyncTable<Model> Models { get; }
#else
        public ICloudSyncTable<TodoItem> TodoItems => SyncTable<TodoItem>();
#endif
#if (NinjectContainer)

        public override ICloudSyncTable<T> SyncTable<T>() =>
            _kernel.Get<ICloudSyncTable<T>>();

        public override ICloudTable<T> Table<T>() =>
            _kernel.Get<ICloudTable<T>>();
#endif
    }
}