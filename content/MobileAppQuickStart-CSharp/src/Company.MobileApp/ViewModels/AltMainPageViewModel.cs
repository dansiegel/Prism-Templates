using System;
using System.Collections.Generic;
#if (UseMvvmHelpers)
using System.Linq;
using MvvmHelpers;
#else
using System.Collections.ObjectModel;
using System.Linq;
#endif
using Prism.AppModel;
using Prism.Commands;
#if (!UseMvvmHelpers)
using Prism.Mvvm;
#endif
using Prism.Navigation;
using Prism.Services;
#if (UseRealm)
using Realms;
#endif
#if (UseAzureMobileClient)
using AzureMobileClient.Helpers;
using Company.MobileApp.Data;
#endif
using Company.MobileApp.Models;
#if (Localization)
using Company.MobileApp.Strings;
#endif

namespace Company.MobileApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, 
                                 IDeviceService deviceService)
            : base(navigationService, pageDialogService, deviceService)
        {
#if (Localization)
            Title = Resources.MainPageTitle;
#else
            Title = "Main Page";
#endif
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            // TODO: Implement your initialization logic
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            // TODO: Handle any final tasks before you navigate away
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            switch(parameters.GetNavigationMode())
            {
                case NavigationMode.Back:
                    // TODO: Handle any tasks that should occur only when navigated back to
                    break;
                case NavigationMode.New:
                    // TODO: Handle any tasks that should occur only when navigated to for the first time
                    break;
            }

            // TODO: Handle any tasks that should be done every time OnNavigatedTo is triggered
        }
    }
}