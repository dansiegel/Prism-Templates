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
#if (UseMobileCenter)
using MobileApp.Helpers;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
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

#if (Localization)
            // determine the correct, supported .NET culture
            // set the RESX for resource localization
            // set the Thread for locale-aware methods
            var localize = Container.Resolve<i18n.ILocalize>();
            localize.SetLocale(Resx.Resources.Culture = localize.GetCurrentCultureInfo());

#endif
            await NavigationService.NavigateAsync("NavigationPage/MainPage?todo=Item1&todo=Item2&todo=Item3");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
#if (UseMobileCenter)
            MobileCenter.Start($"ios={AppConstants.MobileCenter_iOS_Secret};" +
                               $"android={AppConstants.MobileCenter_Android_Secret}",
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
    }
}
