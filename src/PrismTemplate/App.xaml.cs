using Xamarin.Forms;
#if AutofacContainer
#elif DryIocContainer
using DryIoc;
using Prism.DryIoc;
#elif NinjectContainer
#else
using Microsoft.Practices.Unity;
using Prism.Unity;
#endif
using Prism.Logging;
using PrismTemplate.Extensions;
using PrismTemplate.Views;

namespace PrismTemplate
{
    public partial class App : PrismApplication
    {
        public App()
        {
            InitializeComponent();
        }

        protected override async void OnInitialized()
        {
            try
            {
                await NavigationService.NavigateAsync("NavigationPage/MainPage?message=Hello%20from%Prism%20Forms");
            }
            catch (System.Exception ex)
            {
                Logger.Log(ex);
            }
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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
