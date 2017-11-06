using System;
using System.Linq;
using System.Threading.Tasks;
using Prism;
using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace Company.MobileApp.ViewModels
{
    public class ViewModelBase : BindableBase, IApplicationLifecycleAware, IActiveAware, INavigationAware, IDestructible, IConfirmNavigation, IConfirmNavigationAsync, IPageLifecycleAware
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

#if (IncludeFody)
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _subtitle;
        public string Subtitle
        {
            get => _subtitle;
            set => SetProperty(ref _subtitle, value);
        }

        private string _icon;
        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value, onChanged: OnIsBusyChanged);
        }

        private bool _isNotBusy;
        public bool IsNotBusy
        {
            get => _isNotBusy;
            set => SetProperty(ref _isNotBusy, value, onChanged: OnIsNotBusyChanged);
        }

        private bool _canLoadMore;
        public bool CanLoadMore
        {
            get => _canLoadMore;
            set => SetProperty(ref _canLoadMore, value);
        }

        private string _header;
        public string Header
        {
            get => _header;
            set => SetProperty(ref _header, value);
        }

        private string _footer;
        public string Footer
        {
            get => _footer;
            set => SetProperty(ref _footer, value);
        }
#else
        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Icon { get; set; }

        public bool IsBusy { get; set; }

        public bool IsNotBusy { get; set; }

        public bool CanLoadMore { get; set; }

        public string Header { get; set; }

        public string Footer { get; set; }
#endif

        private void OnIsBusyChanged() => IsNotBusy = !IsBusy;

        private void OnIsNotBusyChanged() => IsBusy = !IsNotBusy;

#region IActiveAware

#if (IncludeFody)
        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value, onChanged: OnIsActiveChanged);
        }
#else
        public bool IsActive { get; set; }
#endif

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