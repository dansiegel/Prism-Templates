using System;
using System.Threading.Tasks;
using Company.MobileApp.Services;
using Company.MobileApp.Views;
#if (UseAcrDialogs)
using Acr.UserDialogs;
#endif
#if (AutofacContainer)
using Autofac;
using Prism.Autofac;
using Prism.Autofac.Forms;
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
#if (UseMobileCenter || UseAzureMobileClient)
using Company.MobileApp.Data;
using Company.MobileApp.Helpers;
#endif
#if (UseMobileCenter)
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
#endif
#if (UseAzureMobileClient)
#if (!NoAuth)
using Company.MobileApp.Auth;
#endif
#if (AADAuth || AADB2CAuth)
using Microsoft.Identity.Client;
#endif
using AzureMobileClient.Helpers;
using AzureMobileClient.Helpers.Accounts;
using Microsoft.WindowsAzure.MobileServices;
#endif
#if (UseRealm)
using Company.MobileApp.Helpers;
using Realms;
using Realms.Sync;
#endif
#if (IncludeBarcodeService)
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using BarcodeScanner;
#endif
using Prism.Logging;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
// For compatibility with Prism 6.3. Prism 7 removes the non-working Debug Logger.
using DebugLogger = Company.MobileApp.Services.DebugLogger;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Company.MobileApp
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            SetupLogging();

#if (Localization)
            // determine the correct, supported .NET culture
            // set the RESX for resource localization
            // set the Thread for locale-aware methods
    #if (NinjectContainer)
            var localize = Container.Get<i18n.ILocalize>();
    #else
            var localize = Container.Resolve<i18n.ILocalize>();
    #endif
            localize.SetLocale(Strings.Resources.Culture = localize.GetCurrentCultureInfo());

#endif
#if (UseAzureMobileClient || UseRealm || Empty)
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
#else
            await NavigationService.NavigateAsync("NavigationPage/MainPage?todo=Item1&todo=Item2&todo=Item3");
#endif
        }

        protected override void RegisterTypes()
        {
#if (AutofacContainer)
            var builder = new ContainerBuilder();

#endif
#if (UseMobileCenter)
    #if (AutofacContainer)
            builder.Register(ctx => new MCAnalyticsLogger()).As<ILoggerFacade>().SingleInstance();
    #elseif (DryIocContainer)
            Container.Register<ILoggerFacade, MCAnalyticsLogger>(Reuse.Singleton);
    #elseif (NinjectContainer)
            Container.Bind<ILoggerFacade>().To<MCAnalyticsLogger>().InSingletonScope();
    #else
            Container.RegisterType<ILoggerFacade, MCAnalyticsLogger>(new ContainerControlledLifetimeManager());
    #endif
#endif
#if (UseAzureMobileClient)
    #if (AutofacContainer)
        // TODO: generically register ICloudTable<> as AzureCloudTable<>
        // TODO: generically register ICloudSyncTable<> as AzureCloudSyncTable<>
        #if (NoAuth)
        // TODO: if you aren't using authentication you just need to register an instance of IMobileServiceClient -> MobileServiceClient
        #else
            #if (AADAuth || AADB2CAuth)
            // TODO: Register an instance of IPublicClientApplication
            #endif
        // TODO: if you are using authentication see below
        /* 
         * Register IAzureCloudServiceOptions <-> AppServiceContextOptions
         * var context = new AppDataContext
         * Register Instance => IAppDataContext <-> context
         * Register Instance => ICloudCloudService <-> context
         * Register Instance => IMobileServiceClient <-> context.Client
         * Register => IAccountStore <-> AccountStore
         * Register => ILoginProvider <-> LoginProvider
         */
        #endif
    #elseif (DryIocContainer)
            // ICloudTable is only needed for Online Only data
            Container.Register(typeof(ICloudTable<>), typeof(AzureCloudTable<>), Reuse.Singleton);
            Container.Register(typeof(ICloudSyncTable<>), typeof(AzureCloudSyncTable<>), Reuse.Singleton);

        #if (NoAuth)
            Container.UseInstance<IMobileServiceClient>(new MobileServiceClient(Secrets.AppServiceEndpoint));
            Container.RegisterMany<AppDataContext>(reuse: Reuse.Singleton,
                                                   serviceTypeCondition: type => 
                                                        type == typeof(IAppDataContext) ||
                                                        type == typeof(ICloudAppContext));
        #else
            #if (AADAuth || AADB2CAuth)
            Container.UseInstance<IPublicClientApplication>(new PublicClientApplication(Secrets.AuthClientId, AppConstants.Authority)
            {
                RedirectUri = AppConstants.RedirectUri
            });
            #endif
            Container.Register<IAzureCloudServiceOptions, AppServiceContextOptions>(Reuse.Singleton);
            Container.RegisterMany<AppDataContext>(reuse: Reuse.Singleton,
                                                   serviceTypeCondition: type => 
                                                        type == typeof(IAppDataContext) ||
                                                        type == typeof(ICloudService));
            Container.RegisterDelegate<IMobileServiceClient>(factoryDelegate: r => r.Resolve<ICloudService>().Client,
                                                             reuse: Reuse.Singleton,
                                                             setup: Setup.With(allowDisposableTransient: true));
            Container.Register<IAccountStore,AccountStore>(Reuse.Singleton);
            Container.Register<ILoginProvider,LoginProvider>(Reuse.Singleton);
        #endif
    #elseif (NinjectContainer)
        // TODO: generically register ICloudTable<> as AzureCloudTable<>
        // TODO: generically register ICloudSyncTable<> as AzureCloudSyncTable<>
        #if (NoAuth)
        // TODO: if you aren't using authentication you just need to register an instance of IMobileServiceClient -> MobileServiceClient
        #else
            #if (AADAuth || AADB2CAuth)
            // TODO: Register an instance of IPublicClientApplication
            #endif
        // TODO: if you are using authentication see below
        /* 
         * Register IAzureCloudServiceOptions <-> AppServiceContextOptions
         * var context = new AppDataContext
         * Register Instance => IAppDataContext <-> context
         * Register Instance => ICloudCloudService <-> context
         * Register Instance => IMobileServiceClient <-> context.Client
         * Register => IAccountStore <-> AccountStore
         * Register => ILoginProvider <-> LoginProvider
         */
        #endif
    #elseif (UnityContainer)
            // ICloudTable is only needed for Online Only data
            Container.RegisterType(typeof(ICloudTable<>), typeof(AzureCloudTable<>), new ContainerControlledLifetimeManager());
            Container.RegisterType(typeof(ICloudSyncTable<>), typeof(AzureCloudSyncTable<>), new ContainerControlledLifetimeManager());

        #if (NoAuth)
            Container.RegisterInstance<IMobileServiceClient>(new MobileServiceClient(Secrets.AppServiceEndpoint));
            Container.RegisterType<AppDataContext>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IAppDataContext>(new InjectionFactory(c => c.Resolve<AppDataContext>()));
            Container.RegisterType<ICloudAppContext>(new InjectionFactory(c => c.Resolve<AppDataContext>()));
        #else
            #if (AADAuth || AADB2CAuth)
            Container.RegisterInstance<IPublicClientApplication>(new PublicClientApplication(Secrets.AuthClientId, AppConstants.Authority)
            {
                RedirectUri = AppConstants.RedirectUri
            });
            #endif
            
            Container.RegisterType<IAzureCloudServiceOptions, AppServiceContextOptions>(new ContainerControlledLifetimeManager());
            Container.RegisterType<AppDataContext>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IAppDataContext>(new InjectionFactory(c => c.Resolve<AppDataContext>()));
            Container.RegisterType<ICloudService>(new InjectionFactory(c => c.Resolve<AppDataContext>()));
            Container.RegisterType<IMobileServiceClient>(new InjectionFactory(c => c.Resolve<ICloudService>().Client));

            Container.RegisterType<IAccountStore,AccountStore>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ILoginProvider,LoginProvider>(new ContainerControlledLifetimeManager());
        #endif
    #endif
#endif
#if (UseRealm)
            //var serverURL = new Uri(Secrets.RealmServer);
            //var config = new SyncConfiguration(User.Current, serverURL);
            var config = RealmConfiguration.DefaultConfiguration;
    #if (AutofacContainer)
            // TODO: Register Realm as a Transient Service using the factory () => Realm.GetInstance()
    #elseif (DryIocContainer)
            Container.Register(reuse: Reuse.Transient,
                               made: Made.Of(() => Realm.GetInstance(config)),
                               setup: Setup.With(allowDisposableTransient: true));
    #elseif (NinjectContainer)
            // TODO: Register Realm as a Transient Service using the factory () => Realm.GetInstance()
    #else
            Container.RegisterType<Realm>(new InjectionFactory(c => Realm.GetInstance(config)));
    #endif
#endif
#if (IncludeBarcodeService)
            // Uses a Popup Page to contain the Scanner
    #if (AutofacContainer)
            builder.RegisterInstance(PopupNavigation.Instance).As<IPopupNavigation>().SingleInstance();
            builder.Register(ctx => new PopupBarcodeScannerService(Container.Resolve<IPopupNavigation>())).As<IBarcodeScannerService>().SingleInstance();
    #elseif (DryIocContainer)
            Container.UseInstance<IPopupNavigation>(PopupNavigation.Instance);
            Container.Register<IBarcodeScannerService, PopupBarcodeScannerService>();
    #elseif (NinjectContainer)
            Container.Bind<IPopupNavigation>().ToConstant(PopupNavigation.Instance).InSingletonScope();
            Container.Bind<IBarcodeScannerService>().To<PopupBarcodeScannerService>().InSingletonScope();
    #else
            Container.RegisterInstance<IPopupNavigation>(PopupNavigation.Instance);
            Container.RegisterType<IBarcodeScannerService, PopupBarcodeScannerService>();
    #endif
#endif
#if (UseAcrDialogs)
    #if (AutofacContainer)
            builder.RegisterInstance(UserDialogs.Instance).As<IUserDialogs>().SingleInstance();
    #elseif (DryIocContainer)
            Container.UseInstance<IUserDialogs>(UserDialogs.Instance);
    #elseif (NinjectContainer)
            Container.Bind<IUserDialogs>().ToConstant(UserDialogs.Instance).InSingletonScope();
    #else
            Container.RegisterInstance<IUserDialogs>(UserDialogs.Instance);
    #endif
#endif

            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
#if (!Empty)
            Container.RegisterTypeForNavigation<TodoItemDetail>();
#endif
            // Navigating to "TabbedPage?tab=ViewA&tab=ViewB&tab=ViewC will generate a TabbedPage
            // with three tabs for ViewA, ViewB, & ViewC
            Container.RegisterTypeForNavigation<DynamicTabbedPage>("TabbedPage");
#if (AutofacContainer)

            builder.Update(Container);
#endif
        }

        protected override void OnStart()
        {
            // Handle when your app starts
#if (UseMobileCenter)
            MobileCenter.Start(AppConstants.MobileCenterStart,
                                typeof(Analytics), typeof(Crashes));

            Logger = Container.Resolve<ILoggerFacade>();
#endif
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        protected override ILoggerFacade CreateLogger() => new DebugLogger();

        private void SetupLogging()
        {
            // By default, we set the logger to use the included DebugLogger,
            // which uses System.Diagnostics.Debug.WriteLine to print your message. If you have
            // overridden the default DebugLogger, you will need to update the Logger here to
            // ensure that any calls to your logger in the App.xaml.cs will use your logger rather
            // than the default DebugLogger.
#if (NinjectContainer)
            //Logger = Container.Get<ILoggerFacade>();
#else
            //Logger = Container.Resolve<ILoggerFacade>();
#endif
            TaskScheduler.UnobservedTaskException += ( sender, e ) =>
            {
                Logger.Log(e.Exception.ToString(), Category.Exception, Priority.High);
            };
        }
    }
}
