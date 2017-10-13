using System.Threading.Tasks;
using Prism.AppModel;
using Prism.Navigation;
using Prism.Services;

namespace Company.MobileApp.ViewModels
{

    public class SplashScreenPageViewModel : ViewModelBase
    {
        public SplashScreenPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService) 
            : base(navigationService, pageDialogService, deviceService)
        {
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            // TODO: Implement any initialization logic you need here. Example would be to handle automatic user login

            // Simulated long running task. You should remove this in your app.
            await Task.Delay(4000);

            // After performing the long running task we perform an absolute Navigation to remove the SplashScreen from
            // the Navigation Stack.
#if (UseAzureMobileClient || UseRealm || Empty)
            await _navigationService.NavigateAsync("/NavigationPage/MainPage");
#else
            await _navigationService.NavigateAsync("/NavigationPage/MainPage?todo=Item1&todo=Item2&todo=Item3");
#endif
        }
    }
}