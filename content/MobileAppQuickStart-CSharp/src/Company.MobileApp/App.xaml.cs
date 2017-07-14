using System.Threading.Tasks;
using Company.MobileApp.Services;
using Company.MobileApp.Views;
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
using Company.MobileApp.Auth;
using AzureMobileClient.Helpers;
using AzureMobileClient.Helpers.Accounts;
using Microsoft.WindowsAzure.MobileServices;
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
            var localize = Container.Resolve<i18n.ILocalize>();
            localize.SetLocale(Strings.Resources.Culture = localize.GetCurrentCultureInfo());

#endif
            await NavigationService.NavigateAsync("NavigationPage/MainPage?todo=Item1&todo=Item2&todo=Item3");
        }

        protected override void RegisterTypes()
        {
#if (AutofacContainer)
            var builder = new ContainerBuilder();
#endif
#if (UseAzureMobileClient)
    #if (DryIocContainer)
            // ICloudTable is only needed for Online Only data
            Container.Register(typeof(ICloudTable<>), typeof(AzureCloudTable<>), Reuse.Singleton);
            Container.Register(typeof(ICloudSyncTable<>), typeof(AzureCloudSyncTable<>), Reuse.Singleton);

            // If you are not using Authentication
            // Container.UseInstance<IMobileServiceClient>(new MobileServiceClient(Secrets.AppServiceEndpoint));

            // If you are using Authentication
            // If using Facebook or some other 3rd Party OAuth provider be sure to register ILoginProvider
            // in IPlatformServices in your Platform Project. If you are using a custom auth provider, you may
            // be able to author an ILoginProvider from shared code.
            Container.Register<IAzureCloudServiceOptions, AppServiceContextOptions>(Reuse.Singleton);
            Container.RegisterMany<AppDataContext>(Reuse.Singleton,
                                                   serviceTypeCondition: type => type == typeof(AppDataContext) ||
                                                   type == typeof(IAppDataContext) ||
                                                   type == typeof(ICloudService));
            Container.Register<IMobileServiceClient>(reuse: Reuse.Singleton,
                                                     made: Made.Of(() => Arg.Of<AppDataContext>().Client));
            Container.Register<IAccountStore,AccountStore>(Reuse.Singleton);
            Container.Register<ILoginProvider,LoginProvider>(Reuse.Singleton);
    #endif
    #if (UnityContainer)
            Container.RegisterType(typeof(IGenericClass<>), typeof(GenericClass<>));
            // ICloudTable is only needed for Online Only data
            Container.RegisterType(typeof(ICloudTable<>), typeof(AzureCloudTable<>), Reuse.Singleton);
            Container.RegisterType(typeof(ICloudSyncTable<>), typeof(AzureCloudSyncTable<>), Reuse.Singleton);

            // If you are not using Authentication
            Container.RegisterInstance<IMobileServiceClient>(new MobileServiceClient(Secrets.AppServiceEndpoint));

            // If you are using Authentication
            // If using Facebook or some other 3rd Party OAuth provider be sure to register ILoginProvider
            // in IPlatformServices in your Platform Project. If you are using a custom auth provider, you may
            // be able to author an ILoginProvider from shared code.
            // Container.RegisterType<IAzureCloudServiceOptions, AppServiceContextOptions>(new ContainerControlledLifetimeManager());
            Container.RegisterType<AppDataContext>(new ContainerControlledLifetimeManager());
            // Container.RegisterType<ICloudService>(reuse: Reuse.Singleton,
            //                                   made: Made.Of(() => Arg.Of<AppDataContext>()));
            // Container.RegisterType<IAppDataContext>(reuse: Reuse.Singleton,
                                                made: Made.Of(() => Arg.Of<AppDataContext>()));
            // Container.RegisterType<IMobileServiceClient>(reuse: Reuse.Singleton,
            //                                          made: Made.Of(() => Arg.Of<AppDataContext>().Client));
            Container.RegisterType<IAccountStore,AccountStore>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ILoginProvider,LoginProvider>(new ContainerControlledLifetimeManager());
    #endif
#endif

            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
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
