using System.Threading.Tasks;
using MobileApp.Views;
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
using MobileApp.Helpers;
#endif
#if (UseMobileCenter)
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
#endif
#if (UseAzureMobileClient)
using AzureMobileClient.Helpers;
using Microsoft.WindowsAzure.MobileServices;
#endif
using Prism.Logging;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MobileApp
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
#if (UseAzureMobileClient)
            // ICloudTable is only needed for Online Only data
            Container.Register(typeof(ICloudTable<>), typeof(AzureCloudTable<>));
            Container.Register(typeof(ICloudSyncTable<>), typeof(AzureCloudSyncTable<>));

            // If you are not using Authentication
            Container.UseInstance<IMobileServiceClient>(new MobileServiceClient(AppConstants.AppServiceEndpoint));

            // If you are using Authentication
            // If using Facebook or some other 3rd Party OAuth provider be sure to register ILoginProvider
            // in IPlatformServices in your Platform Project. If you are using a custom auth provider, you may
            // be able to author an ILoginProvider from shared code.
            // Container.Register<IAzureCloudServiceOptions, MobileAppServiceContextOptions>(Reuse.Singleton);
            var dataContext = new AppDataContext(Container);
            // Container.UseInstance<ICloudService>(dataContext);
            Container.UseInstance<IAppDataContext>(dataContext);
            // Container.Register<IMobileServiceClient>(reuse: Reuse.Singleton,
            //                                         made: Made.Of(() => Arg.Of<ICloudService>().Client));

#endif
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
#if (UseMobileCenter)
            MobileCenter.Start($"ios={AppConstants.MobileCenter_iOS_Secret};" +
                                $"android={AppConstants.MobileCenter_Android_Secret};" +
                                $"uwp={AppConstants.MobileCenter_UWP_Secret}",
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

        private void SetupLogging()
        {
            // By default, PrismApplication sets the logger to use the included DebugLogger,
            // which uses System.Diagnostics.Debug.WriteLine to print your message. If you have
            // overridden the default DebugLogger, you will need to update the Logger here to
            // ensure that any calls to your logger in the App.xaml.cs will use your logger rather
            // than the default DebugLogger.
#if (NinjectContainer)
            Logger = Container.Get<ILoggerFacade>();
#else
            Logger = Container.Resolve<ILoggerFacade>();
#endif
            TaskScheduler.UnobservedTaskException += ( sender, e ) =>
            {
                Logger.Log(e.Exception.ToString(), Category.Exception, Priority.High);
            };
        }
    }
}
