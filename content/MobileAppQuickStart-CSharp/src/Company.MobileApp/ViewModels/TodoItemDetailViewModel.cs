using System;
using System.Collections.Generic;
#if (UseMvvmHelpers)
using System.Linq;
using MvvmHelpers;
#else
using System.Collections.ObjectModel;
using System.Linq;
#endif
#if (UseAcrDialogs)
using Acr.UserDialogs;
#endif
using Prism.AppModel;
using Prism.Commands;
using Prism.Events;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
#if (UseRealm)
using Realms;
#endif
#if (UseAzureMobileClient)
using Company.MobileApp.Data;
#endif
using Company.MobileApp.Models;
using Company.MobileApp.Strings;

namespace Company.MobileApp.ViewModels
{
    public class TodoItemDetailViewModel : ViewModelBase
    {
#if (UseAzureMobileClient)
        private IAppDataContext _dataContext { get; }

        public TodoItemDetailViewModel(INavigationService navigationService, IApplicationStore applicationStore, 
                                       IDeviceService deviceService, IAppDataContext dataContext)
#else
        public TodoItemDetailViewModel(INavigationService navigationService, IApplicationStore applicationStore, 
                                       IDeviceService deviceService)
#endif
            : base(navigationService, applicationStore, deviceService)
        {
#if (UseAzureMobileClient)
            _dataContext = dataContext;

#endif
#if (Localization)
            Title = Resources.TodoItemDetailTitle;
#else
            Title = "Item Detail";
#endif
            SaveCommand = new DelegateCommand(OnSaveCommandExecuted);
        }

        public TodoItem Model { get; set; }

        public DelegateCommand SaveCommand { get; }

#if (UseRealm)
        private Transaction _transaction;
#else
        private bool _isNew;
#endif

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
#if (UseRealm)
            _transaction = parameters.GetValue<Transaction>("transaction");
#else
            _isNew = parameters.GetValue<bool>("new");
#endif
            Model = parameters.GetValue<TodoItem>("todoItem");
        }

#if (UseRealm)
        private async void OnSaveCommandExecuted()
        {
            _transaction.Commit();
    #if (UseAcrDialogs)
            Toast("Item Saved");
    #endif
            await _navigationService.PopupGoBackAsync("todoItem", Model);
        }

        public override void Destroy()
        {
            _transaction.Dispose();
        }
#elseif (UseAzureMobileClient)
        private async void OnSaveCommandExecuted()
        {
            if(_isNew)
            {
                await _dataContext.TodoItems.CreateItemAsync(Model);
    #if (UseAcrDialogs)
                Toast("New Item Saved");
    #endif
            }
            else
            {
                await _dataContext.TodoItems.UpdateItemAsync(Model);
    #if (UseAcrDialogs)
                Toast("Item Updated");
    #endif
            }

            await _navigationService.PopupGoBackAsync();
        }
#else
        private async void OnSaveCommandExecuted()
        {
            if(_isNew)
            {
    #if (UseAcrDialogs)
                Toast("New Item Saved");
    #endif
                await _navigationService.PopupGoBackAsync("todoItem", Model);
            }
            else
            {
    #if (UseAcrDialogs)
                Toast("Item Updated");
    #endif
                await _navigationService.PopupGoBackAsync();
            }
        }
#endif
#if (UseAcrDialogs)
        private void Toast(string message)
        {
            _userDialogs.Toast(new ToastConfig(message)
            {
                BackgroundColor = System.Drawing.Color.Green,
                MessageTextColor = System.Drawing.Color.White,
                Position = ToastPosition.Top
            });
        }
#endif
    }
}