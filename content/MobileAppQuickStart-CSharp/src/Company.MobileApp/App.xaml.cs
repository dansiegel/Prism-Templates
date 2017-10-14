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
using Company.MobileApp.Helpers;
#endif
#if (UseMobileCenter)
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using Microsoft.Azure.Mobile.Distribute;
using Microsoft.Azure.Mobile.Push;
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
#if (UseAzureMobileClient && AADAuth || AADB2CAuth)
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
using Xamarin.Forms.Xaml;
using DebugLogger = Company.MobileApp.Services.DebugLogger;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
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
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            SetupLogging();

#if (Empty)
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
#else
            await NavigationService.NavigateAsync("SplashScreenPage");
#endif
        }

        protected override void RegisterTypes()
        {
#if (UseMobileCenter)
            if(!string.IsNullOrWhiteSpace(AppConstants.MobileCenterStart))
            {
    #if (AutofacContainer)
                Builder.RegisterType<MCAnalyticsLogger>().As<ILoggerFacade>().SingleInstance();
    #elseif (DryIocContainer)
                Container.Register<ILoggerFacade, MCAnalyticsLogger>(reuse: Reuse.Singleton,
                                                                     ifAlreadyRegistered: IfAlreadyRegistered.Replace);
    #elseif (NinjectContainer)
                Container.Bind<ILoggerFacade>().To<MCAnalyticsLogger>().InSingletonScope();
    #else
                Container.RegisterType<ILoggerFacade, MCAnalyticsLogger>(new ContainerControlledLifetimeManager());
    #endif
            }
#endif
#if (UseAzureMobileClient)
            // ICloudTable is only needed for Online Only data
    #if (AutofacContainer)
            Builder.RegisterGeneric(typeof(AzureCloudTable<>)).As(typeof(ICloudTable<>)).InstancePerLifetimeScope();
            Builder.RegisterGeneric(typeof(AzureCloudSyncTable<>)).As(typeof(ICloudSyncTable<>)).InstancePerLifetimeScope();

        #if (NoAuth)
            Builder.RegisterInstance(new MobileServiceClient(Secrets.AppServiceEndpoint)).As<IMobileServiceClient>().SingleInstance();
            Builder.RegisterType<AppDataContext>().As<IAppDataContext>().As<ICloudAppContext>().SingleInstance();
        #else
            #if (AADAuth || AADB2CAuth)
            Builder.RegisterInstance(new PublicClientApplication(Secrets.AuthClientId, AppConstants.Authority)
            {
                RedirectUri = AppConstants.RedirectUri
            }).As<IPublicClientApplication>().SingleInstance();

            #endif
            Builder.RegisterType<AppServiceContextOptions>().As<IAzureCloudServiceOptions>().SingleInstance();
            Builder.RegisterType<AppDataContext>().As<IAppDataContext>().As<ICloudService>().SingleInstance();
            Builder.Register(ctx => ctx.Resolve<ICloudService>().Client).As<IMobileServiceClient>().SingleInstance();

            Builder.RegisterType<LoginProvider>().As<ILoginProvider<AADAccount>>().SingleInstance();
        #endif
    #elseif (DryIocContainer)
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
            Container.Register<ILoginProvider<AADAccount>,LoginProvider>(Reuse.Singleton);
        #endif
    #elseif (NinjectContainer)
            Container.Bind(typeof(ICloudTable<>)).To(typeof(AzureCloudTable<>)).InSingletonScope();
            Container.Bind(typeof(ICloudSyncTable<>)).To(typeof(AzureCloudSyncTable<>)).InSingletonScope();
        #if (NoAuth)
            Container.Bind<IMobileServiceClient>().ToConstant(new MobileServiceClient(Secrets.AppServiceEndpoint)).InSingletonScope();
            Container.Bind<IAppDataContext, ICloudAppContext>().To<AppDataContext>().InSingletonScope();
        #else
            #if (AADAuth || AADB2CAuth)
            Container.Bind<IPublicClientApplication>()
                     .ToConstant(
                        new PublicClientApplication(Secrets.AuthClientId, AppConstants.Authority)
                        {
                            RedirectUri = AppConstants.RedirectUri
                        })
                    .InSingletonScope();
            #endif
            Container.Bind<IAzureCloudServiceOptions>().To<AppServiceContextOptions>().InSingletonScope();
            Container.Bind<IAppDataContext, ICloudService>().To<AppDataContext>().InSingletonScope();
            Container.Bind<IMobileServiceClient>().ToMethod(c => Container.Get<ICloudService>().Client).InSingletonScope();

            Container.Bind<ILoginProvider<AADAccount>>().To<LoginProvider>().InSingletonScope();
        #endif
    #elseif (UnityContainer)
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

            Container.RegisterType<ILoginProvider<AADAccount>,LoginProvider>(new ContainerControlledLifetimeManager());
        #endif
    #endif
#endif
#if (UseRealm)
            //var serverURL = new Uri(Secrets.RealmServer);
            //var config = new SyncConfiguration(User.Current, serverURL);
            var config = RealmConfiguration.DefaultConfiguration;
    #if (AutofacContainer)
            Builder.Register(ctx => Realm.GetInstance(config)).As<Realm>().InstancePerDependency();
    #elseif (DryIocContainer)
            Container.Register(reuse: Reuse.Transient,
                               made: Made.Of(() => Realm.GetInstance(config)),
                               setup: Setup.With(allowDisposableTransient: true));
    #elseif (NinjectContainer)
            Container.Bind<Realm>().ToMethod(c => Realm.GetInstance(config)).InTransientScope();
    #else
            Container.RegisterType<Realm>(new InjectionFactory(c => Realm.GetInstance(config)));
    #endif
#endif
#if (IncludeBarcodeService)
            // NOTE: Uses a Popup Page to contain the Scanner. You can optionally register 
            // the ContentPageBarcodeScannerService if you prefer a full screen approach.
    #if (AutofacContainer)
            Builder.RegisterInstance(PopupNavigation.Instance).As<IPopupNavigation>().SingleInstance();
            Builder.RegisterType<PopupBarcodeScannerService>().As<IBarcodeScannerService>().SingleInstance();
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
            Builder.RegisterInstance(UserDialogs.Instance).As<IUserDialogs>().SingleInstance();
    #elseif (DryIocContainer)
            Container.UseInstance<IUserDialogs>(UserDialogs.Instance);
    #elseif (NinjectContainer)
            Container.Bind<IUserDialogs>().ToConstant(UserDialogs.Instance).InSingletonScope();
    #else
            Container.RegisterInstance<IUserDialogs>(UserDialogs.Instance);
    #endif
#endif

            // Navigating to "TabbedPage?createTab=ViewA&createTab=ViewB&createTab=ViewC will generate a TabbedPage
            // with three tabs for ViewA, ViewB, & ViewC
            // Adding `selectedTab=ViewB` will set the current tab to ViewB
#if (AutofacContainer)
            Builder.RegisterTypeForNavigation<TabbedPage>();
            Builder.RegisterTypeForNavigation<NavigationPage>();
            Builder.RegisterTypeForNavigation<MainPage>();
#else
            Container.RegisterTypeForNavigation<TabbedPage>();
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
#endif
#if (!Empty)
    #if (AutofacContainer)
            Builder.RegisterTypeForNavigation<SplashScreenPage>();
            Builder.RegisterTypeForNavigation<TodoItemDetail>();
    #else
            Container.RegisterTypeForNavigation<SplashScreenPage>();
            Container.RegisterTypeForNavigation<TodoItemDetail>();
    #endif
#endif
        }

        protected override void OnStart()
        {
            // Handle when your app starts
#if (UseMobileCenter)
            // https://docs.microsoft.com/en-us/mobile-center/sdk/distribute/xamarin
            Distribute.ReleaseAvailable = OnReleaseAvailable;
            // https://docs.microsoft.com/en-us/mobile-center/sdk/push/xamarin-forms
            Push.PushNotificationReceived += OnPushNotificationReceived;
            // Handle when your app starts
            MobileCenter.Start(AppConstants.MobileCenterStart,
                               typeof(Analytics), typeof(Crashes), typeof(Distribute), typeof(Push));

    #if (NinjectContainer)
            Logger = Container.Get<ILoggerFacade>();
    #else
            Logger = Container.Resolve<ILoggerFacade>();
    #endif
#endif
        }

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

        protected override ILoggerFacade CreateLogger() => 
            new DebugLogger();

        private void SetupLogging()
        {
            // By default, we set the logger to use the included DebugLogger,
            // which uses System.Diagnostics.Debug.WriteLine to print your message. If you have
            // overridden the default DebugLogger, you will need to update the Logger here to
            // ensure that any calls to your logger in the App.xaml.cs will use your logger rather
            // than the default DebugLogger.
            TaskScheduler.UnobservedTaskException += ( sender, e ) =>
            {
                Logger.Log(e.Exception.ToString(), Category.Exception, Priority.High);
            };
        }
#if (UseMobileCenter)

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
            Logger.Log(summary);
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
