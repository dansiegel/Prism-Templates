using System;
using System.Linq;
#if (UseMvvmHelpers)
using MvvmHelpers;
#endif
#if (IsIActiveAware)
using Prism;
#endif
using Prism.Commands;
using Prism.Events;
using Prism.Logging;
#if (!UseMvvmHelpers)
using Prism.Mvvm;
#endif
using Prism.Navigation;
using Prism.Services;
using PropertyChanged;
using MobileApp.i18n;
using MobileApp.Models;

namespace MobileApp.ViewModels
{
    [ImplementPropertyChanged]
#if (UseMvvmHelpers)
    #if (IsIActiveAware && IsINavigationAware)
    #if (IsIDestructible)
    public class ItemTemplateViewModel : BaseViewModel, IActiveAware, INavigationAware, IDestructible
    #else
    public class ItemTemplateViewModel : BaseViewModel, IActiveAware, INavigationAware
    #endif
    #elseif (IsIActiveAware)
    #if (IsIDestructible)
    public class ItemTemplateViewModel : BaseViewModel, IActiveAware, INavigatingAware, IDestructible
    #else
    public class ItemTemplateViewModel : BaseViewModel, IActiveAware, INavigatingAware
    #endif
    #elseif (IsINavigatedAware)
    #if (IsIDestructible)
    public class ItemTemplateViewModel : BaseViewModel, INavigatedAware, IDestructible
    #else
    public class ItemTemplateViewModel : BaseViewModel, INavigatedAware
    #endif
    #elseif (IsINavigationAware)
    #if (IsIDestructible)
    public class ItemTemplateViewModel : BaseViewModel, INavigationAware, IDestructible
    #else
    public class ItemTemplateViewModel : BaseViewModel, INavigationAware
    #endif
    #else
    #if (IDestructible)
    public class ItemTemplateViewModel : BaseViewModel, IDestructible
    #else
    public class ItemTemplateViewModel : BaseViewModel
    #endif
    #endif
#else
    #if (IsIActiveAware && IsINavigationAware)
    #if (IsIDestructible)
    public class ItemTemplateViewModel : BindableBase, IActiveAware, INavigationAware, IDestructible
    #else
    public class ItemTemplateViewModel : BindableBase, IActiveAware, INavigationAware
    #endif
    #elseif (IsIActiveAware)
    #if (IsIDestructible)
    public class ItemTemplateViewModel : BindableBase, IActiveAware, INavigatingAware, IDestructible
    #else
    public class ItemTemplateViewModel : BindableBase, IActiveAware, INavigatingAware
    #endif
    #elseif (IsINavigatedAware)
    #if (IsIDestructible)
    public class ItemTemplateViewModel : BindableBase, INavigatedAware, IDestructible
    #else
    public class ItemTemplateViewModel : BindableBase, INavigatedAware
    #endif
    #elseif (IsINavigationAware)
    #if (IsIDestructible)
    public class ItemTemplateViewModel : BindableBase, INavigationAware, IDestructible
    #else
    public class ItemTemplateViewModel : BindableBase, INavigationAware
    #endif
    #else
    #if (IDestructible)
    public class ItemTemplateViewModel : BindableBase, IDestructible
    #else
    public class ItemTemplateViewModel : BindableBase
    #endif
    #endif
#endif
    {
        private INavigationService _navigationService { get; }

        public ItemTemplateViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            #if (UseMvvmHelpers)
            Title = "ItemTemplate";
            #endif
            #if (IsMasterDetailPage)
            NavigateCommand = new DelegateCommand<string>(OnNavigateCommandExecuted);
            #endif
        }
#if (IsMasterDetailPage)

        public DelegateCommand<string> NavigateCommand { get; }
#endif
#if (IsIActiveAware)

        public event EventHandler IsActiveChanged;

        public bool IsActive { get; set; }
#endif
#if (IsINavigatingAware || IsIActiveAware || IsINavigationAware)

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            // TODO: Implement your initialization logic
        }
#endif
#if (IsINavigatedAware || IsINavigationAware)

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            // TODO: Handle any final tasks before you navigate away
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            // TODO: Handle anything tasks you need to now that the page has been pushed to the Navigation Stack
        }
#endif
#if (IsIDestructible)

        public void Destroy()
        {
            // TODO: Dispose of any objects you need to for memory management
        }
#endif
#if (IsMasterDetailPage)

        private async void OnNavigateCommandExecuted(string pageName) =>
            await _navigationService.NavigateAsync($"NavigationPage/{pageName}");
#endif
#if (IsIActiveAware)

        private void OnIsActiveChanged()
        {
            #if (UseMvvmHelpers)
            IsBusy = true;
            #endif
            // TODO: Implement your refresh logic
            #if (UseMvvmHelpers)

            IsBusy = false;
            #endif
            
            // Notify anything that might be listening to the Event
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
        }
#endif
    }
}