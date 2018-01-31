using System;
using System.Threading.Tasks;
using Company.MobileApp.Services;
using Company.MobileApp.Views;
using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
#if (UseAcrDialogs)
using Acr.UserDialogs;
#endif
#if (AutofacContainer)
using Autofac;
using Prism.Autofac;
#endif
#if (DryIocContainer)
using DryIoc;
using Prism.DryIoc;
#endif
#if (UnityContainer)
using Microsoft.Practices.Unity;
using Prism.Unity;
#endif
#if (UseAppCenter || UseAzureMobileClient)
using Company.MobileApp.Helpers;
#endif
#if (UseAppCenter)
using FFImageLoading.Helpers;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using Microsoft.AppCenter.Push;
#endif
#if (UseAzureMobileClient)
#if (!NoAuth)
using Company.MobileApp.Auth;
#endif
using Company.MobileApp.Data;
using AzureMobileClient.Helpers;
using AzureMobileClient.Helpers.Accounts;
using Microsoft.WindowsAzure.MobileServices;
#endif
#if (UseAzureMobileClient && (AADAuth || AADB2CAuth))
using AzureMobileClient.Helpers.AzureActiveDirectory;
#endif
#if (AADAuth || AADB2CAuth)
using Microsoft.Identity.Client;
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
using DebugLogger = Company.MobileApp.Services.DebugLogger;

namespace Company.MobileApp
{
    public partial class App : PrismApplication
    {
        /* 
         * NOTE: 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() 
            : this(null)
        {
        }

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
#if (UseAppCenter)
            // https://docs.microsoft.com/en-us/mobile-center/sdk/distribute/xamarin
            Distribute.ReleaseAvailable = OnReleaseAvailable;
            // https://docs.microsoft.com/en-us/mobile-center/sdk/push/xamarin-forms
            Push.PushNotificationReceived += OnPushNotificationReceived;
            // Handle when your app starts
            AppCenter.Start(AppConstants.AppCenterStart,
                               typeof(Analytics), typeof(Crashes), typeof(Distribute), typeof(Push));
#endif
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            LogUnobservedTaskExceptions();

#if (Empty)
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
#else
            await NavigationService.NavigateAsync("SplashScreenPage");
#endif
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register the Popup Plugin Navigation Service
            containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterInstance(CreateLogger());

#if (UseRealm)
            //var serverURL = new Uri(Secrets.RealmServer);
            //var config = new SyncConfiguration(User.Current, serverURL);
            var config = RealmConfiguration.DefaultConfiguration;
    #if (AutofacContainer)
            containerRegistry.GetBuilder().Register(ctx => Realm.GetInstance(config)).As<Realm>().InstancePerDependency();
    #elseif (DryIocContainer)
            containerRegistry.GetContainer().Register(reuse: Reuse.Transient,
                               made: Made.Of(() => Realm.GetInstance(config)),
                               setup: Setup.With(allowDisposableTransient: true));
    #else
            containerRegistry.GetContainer().RegisterType<Realm>(new InjectionFactory(c => Realm.GetInstance(config)));
    #endif

#endif
#if (UseAzureMobileClient)
            containerRegistry.RegisterSingleton(typeof(ICloudTable<>), typeof(AzureCloudTable<>));
            containerRegistry.RegisterSingleton(typeof(ICloudSyncTable<>), typeof(AzureCloudSyncTable<>));
    #if (NoAuth)
            containerRegistry.RegisterInstance<IMobileServiceClient>(new MobileServiceClient(Secrets.AppServiceEndpoint));
    #else
    #if (AADAuth || AADB2CAuth)
            containerRegistry.RegisterInstance<IPublicClientApplication>(
                new PublicClientApplication(Secrets.AuthClientId, AppConstants.Authority)
                {
                    RedirectUri = AppConstants.RedirectUri
                });

        #if (AutofacContainer)
            containerRegistry.GetBuilder().RegisterType<AppDataContext>().As<IAppDataContext>().As<ICloudAppContext>().SingleInstance();
        #elseif (DryIocContainer)
            containerRegistry.GetContainer().RegisterMany<AppDataContext>(reuse: Reuse.Singleton,
                                                   serviceTypeCondition: type => 
                                                        type == typeof(IAppDataContext) ||
                                                        type == typeof(ICloudAppContext));
        #elseif (NinjectContainer)
            containerRegistry.GetContainer().Bind<IAppDataContext, ICloudAppContext>().To<AppDataContext>().InSingletonScope();
        #elseif (UnityContainer)
            containerRegistry.GetContainer().RegisterType<AppDataContext>(new ContainerControlledLifetimeManager());
            containerRegistry.GetContainer().RegisterType<IAppDataContext>(new InjectionFactory(c => c.Resolve<AppDataContext>()));
            containerRegistry.GetContainer().RegisterType<ICloudAppContext>(new InjectionFactory(c => c.Resolve<AppDataContext>()));
        #endif
    #endif
            containerRegistry.RegisterSingleton<IAzureCloudServiceOptions, AppServiceContextOptions>();
        #if (AutofacContainer)
            containerRegistry.GetBuilder().RegisterType<AppDataContext>().As<IAppDataContext>().As<ICloudService>().SingleInstance();
            containerRegistry.GetBuilder().Register(ctx => ctx.Resolve<ICloudService>().Client).As<IMobileServiceClient>().SingleInstance();
        #elseif (DryIocContainer)
            containerRegistry.GetContainer().RegisterMany<AppDataContext>(reuse: Reuse.Singleton,
                                                   serviceTypeCondition: type => 
                                                        type == typeof(IAppDataContext) ||
                                                        type == typeof(ICloudService));
            containerRegistry.GetContainer().RegisterDelegate<IMobileServiceClient>(factoryDelegate: r => r.Resolve<ICloudService>().Client,
                                                             reuse: Reuse.Singleton,
                                                             setup: Setup.With(allowDisposableTransient: true));
        #elseif (NinjectContainer)
            containerRegistry.Bind<IAppDataContext, ICloudService>().To<AppDataContext>().InSingletonScope();
            containerRegistry.Bind<IMobileServiceClient>().ToMethod(c => containerRegistry.Get<ICloudService>().Client).InSingletonScope();
        #elseif (UnityContainer)
            containerRegistry.GetContainer().RegisterType<IAppDataContext>(new InjectionFactory(c => c.Resolve<AppDataContext>()));
            containerRegistry.GetContainer().RegisterType<ICloudService>(new InjectionFactory(c => c.Resolve<AppDataContext>()));
            containerRegistry.GetContainer().RegisterType<IMobileServiceClient>(new InjectionFactory(c => c.Resolve<ICloudService>().Client));
        #endif

            containerRegistry.RegisterSingleton<ILoginProvider<AADAccount>, LoginProvider>();

#endif
#if (IncludeBarcodeService)
            // NOTE: Uses a Popup Page to contain the Scanner. You can optionally register 
            // the ContentPageBarcodeScannerService if you prefer a full screen approach.
            containerRegistry.RegisterSingleton<IBarcodeScannerService, PopupBarcodeScannerService>();
#endif
#if (UseAcrDialogs)
            containerRegistry.RegisterInstance<IUserDialogs>(UserDialogs.Instance);
#endif

            // Navigating to "TabbedPage?createTab=ViewA&createTab=ViewB&createTab=ViewC will generate a TabbedPage
            // with three tabs for ViewA, ViewB, & ViewC
            // Adding `selectedTab=ViewB` will set the current tab to ViewB
            containerRegistry.RegisterForNavigation<TabbedPage>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
#if (!Empty)
            containerRegistry.RegisterForNavigation<SplashScreenPage>();
            containerRegistry.RegisterForNavigation<TodoItemDetail>();
#endif
        }

#if (UseAppCenter)
        protected override async void OnStart()
        {
            // Handle when your app starts
            if (await Analytics.IsEnabledAsync())
            {
                System.Diagnostics.Debug.WriteLine("Analaytics is enabled");
                FFImageLoading.ImageService.Instance.Config.Logger = (IMiniLogger)Container.Resolve<ILoggerFacade>();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Analytics is disabled");
            }
        }
#else
        protected override void OnStart()
        {
            // Handle when your app starts
        }
#endif

        protected override void OnSleep()
        {
            // Handle IApplicationLifecycle
            base.OnSleep();

            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle IApplicationLifecycle
            base.OnResume();

            // Handle when your app resumes
        }

#if (UseAppCenter)
        private ILoggerFacade CreateLogger()
        {
            switch (Xamarin.Forms.Device.RuntimePlatform)
            {
                #if (IncludeAndroid)
                case "Android":
                    if (!string.IsNullOrWhiteSpace(Secrets.AppCenter_Android_Secret))
                        return CreateAppCenterLogger();
                    break;
                #endif
                #if (IncludeiOS)
                case "iOS":
                    if (!string.IsNullOrWhiteSpace(Secrets.AppCenter_iOS_Secret))
                        return CreateAppCenterLogger();
                    break;
                #endif
                #if (UWPSupported)
                case "UWP":
                    if (!string.IsNullOrWhiteSpace(Secrets.AppCenter_UWP_Secret))
                        return CreateAppCenterLogger();
                    break;
                #endif
            }
            return new DebugLogger();
        }

        private MCAnalyticsLogger CreateAppCenterLogger()
        {
            var logger = new MCAnalyticsLogger();
            FFImageLoading.ImageService.Instance.Config.Logger = (IMiniLogger)logger;
            return logger;
        }
#else
        private ILoggerFacade CreateLogger() => 
            new DebugLogger();
#endif

        private void LogUnobservedTaskExceptions()
        {
            TaskScheduler.UnobservedTaskException += ( sender, e ) =>
            {
                Container.Resolve<ILoggerFacade>().Log(e.Exception);
            };
        }
#if (UseAppCenter)

        private void OnPushNotificationReceived(object sender, PushNotificationReceivedEventArgs e)
        {
            // Add the notification message and title to the message
            var summary = $"Push notification received:" +
                $"\n\tNotification title: {e.Title}" +
                $"\n\tMessage: {e.Message}";

            // If there is custom data associated with the notification,
            // print the entries
            if(e.CustomData != null)
            {
                summary += "\n\tCustom data:\n";
                foreach(var key in e.CustomData.Keys)
                {
                    summary += $"\t\t{key} : {e.CustomData[key]}\n";
                }
            }

            // Send the notification summary to debug output
            System.Diagnostics.Debug.WriteLine(summary);
            Container.Resolve<ILoggerFacade>().Log(summary);
        }

        private bool OnReleaseAvailable(ReleaseDetails releaseDetails)
        {
            // Look at releaseDetails public properties to get version information, release notes text or release notes URL
            string versionName = releaseDetails.ShortVersion;
            string versionCodeOrBuildNumber = releaseDetails.Version;
            string releaseNotes = releaseDetails.ReleaseNotes;
            Uri releaseNotesUrl = releaseDetails.ReleaseNotesUrl;

            // custom dialog
            var title = "Version " + versionName + " available!";
            Task answer;

            // On mandatory update, user cannot postpone
            if(releaseDetails.MandatoryUpdate)
            {
                answer = Current.MainPage.DisplayAlert(title, releaseNotes, "Download and Install");
            }
            else
            {
                answer = Current.MainPage.DisplayAlert(title, releaseNotes, "Download and Install", "Maybe tomorrow...");
            }
            answer.ContinueWith((task) =>
            {
                // If mandatory or if answer was positive
                if(releaseDetails.MandatoryUpdate || (task as Task<bool>).Result)
                {
                    // Notify SDK that user selected update
                    Distribute.NotifyUpdateAction(UpdateAction.Update);
                }
                else
                {
                    // Notify SDK that user selected postpone (for 1 day)
                    // Note that this method call is ignored by the SDK if the update is mandatory
                    Distribute.NotifyUpdateAction(UpdateAction.Postpone);
                }
            });

            // Return true if you are using your own dialog, false otherwise
            return true;
        }
#endif
    }
}
