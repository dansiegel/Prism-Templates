using System;
using System.Linq;
using Prism;
using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace Company.MobileApp.ViewModels
{
    public class ViewModelBase : BindableBase, IApplicationLifecycle, IActiveAware, INavigationAware, IDestructible, IConfirmNavigation, IConfirmNavigationAsync, IApplicationLifecycleAware, IPageLifecycleAware
    {
        protected IPageDialogService _pageDialogService { get; }

        protected IDeviceService _deviceService { get; }

        protected INavigationService _navigationService { get; }

        public ViewModelBase(INavigationService navigationService, IPageDialogService pageDialogService, 
                             IDeviceService deviceService)
        {
            _pageDialogService = pageDialogService;
            _deviceService = deviceService;
            _navigationService = navigationService;
        }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Icon { get; set; }

        public bool IsBusy { get; set; }

        public bool IsNotBusy { get; set; }

        public bool CanLoadMore { get; set; }

        public string Header { get; set; }

        public string Footer { get; set; }

        private void OnIsBusyChanged() => IsNotBusy = !IsBusy;

        private void OnIsNotBusyChanged() => IsBusy = !IsNotBusy;

#region IActiveAware

        public bool IsActive { get; set; }

        public event EventHandler IsActiveChanged;

        private void OnIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);

            if(IsActive)
            {
                OnIsActive();
            }
            else
            {
                OnIsNotActive();
            }
        }

        protected virtual void OnIsActive() { }

        protected virtual void OnIsNotActive() { }

#endregion IActiveAware

#region IApplicationLifecycle

        public virtual void OnResume() { }

        public virtual void OnSleep() { }

#endregion IApplicationLifecycle

#region INavigationAware

        public virtual void OnNavigatingTo(NavigationParameters parameters) { }

        public virtual void OnNavigatedTo(NavigationParameters parameters) { }

        public virtual void OnNavigatedFrom(NavigationParameters parameters) { }

#endregion INavigationAware

#region IDestructible

        public virtual void Destroy() { }

#endregion IDestructible

#region IConfirmNavigation

        public virtual bool CanNavigate(NavigationParameters parameters) => true;

        public virtual Task<bool> CanNavigateAsync(NavigationParameters parameters) =>
            Task.FromResult(CanNavigate(parameters));

#endregion IConfirmNavigation

#region IApplicationLifecycleAware

        public virtual void OnResume() { }

        public virtual void OnSleep() { }

#endregion IApplicationLifecycleAware

#region IPageLifecycleAware

        public virtual void OnAppearing() { }

        public virtual void OnDisappearing() { }

#endregion IPageLifecycleAware
    }
}