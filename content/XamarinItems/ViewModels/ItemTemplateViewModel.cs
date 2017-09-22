using System;
using System.Collections.Generic;
using System.Linq;
#if (IsIActiveAware)
using Prism;
#endif
using Prism.AppModel;
using Prism.Commands;
using Prism.Events;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using MobileApp.Strings;
using MobileApp.Models;

namespace MobileApp.ViewModels
{
    public class ItemTemplateViewModel : ViewModelBase
    {
        public ItemTemplateViewModel(INavigationService navigationService, IApplicationStore applicationStore, 
                                     IDeviceService deviceService) 
            : base(navigationService, applicationStore, deviceService)
        {
            Title = Resources.ItemTemplateTitle;
            #if (IsMasterDetailPage)

            NavigateCommand = new DelegateCommand<string>(OnNavigateCommandExecuted);
            #endif
        }
#if (IsMasterDetailPage)

        public DelegateCommand<string> NavigateCommand { get; }
#endif
#if (IsIActiveAware)

        protected override void OnIsActive()
        { 
            // TODO: Handle anything that needs to be done when the View goes active
        }

        protected override void OnIsNotActive()
        { 
            // TODO: Handle anything that needs to be done when the View goes inactive
        }
#endif
#if (IsINavigatingAware || IsIActiveAware || IsINavigationAware)

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            // TODO: Implement your initialization logic
        }
#endif
#if (IsINavigatedAware || IsINavigationAware)

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
#endif
#if (IsIDestructible)

        public override void Destroy()
        {
            // TODO: Dispose of any objects you need to for memory management
        }
#endif
#if (IsMasterDetailPage)

        private async void OnNavigateCommandExecuted(string pageName) =>
            await _navigationService.NavigateAsync($"NavigationPage/{pageName}");
#endif
    }
}