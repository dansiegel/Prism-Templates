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
#if (UseAzureMobileClient)
        private ICloudService _cloudService { get; }

        private IAppDataContext _dataContext { get; }

        public MainPageViewModel(INavigationService navigationService, IApplicationStore applicationStore, 
                                 IDeviceService deviceService, IAppDataContext dataContext, ICloudService cloudService)
#elseif (UseRealm)
        private Realm _realm { get; }

        public MainPageViewModel(INavigationService navigationService, IApplicationStore applicationStore, 
                                 IDeviceService deviceService, Realm realm)
#else
        public MainPageViewModel(INavigationService navigationService, IApplicationStore applicationStore, 
                                 IDeviceService deviceService)
#endif
            : base(navigationService, applicationStore, deviceService)
        {
#if (UseAzureMobileClient)
            _cloudService = cloudService;
            _dataContext = dataContext;

#elseif (UseRealm)
            _realm = realm;

#endif
#if (Localization)
            Title = Resources.MainPageTitle;
#else
            Title = "Main Page";
#endif
#if (UseRealm)
#elseif (UseMvvmHelpers)
            TodoItems = new ObservableRangeCollection<TodoItem>();
#else
            TodoItems = new ObservableCollection<TodoItem>();
#endif

            AddItemCommand = new DelegateCommand(OnAddItemCommandExecuted);
            DeleteItemCommand = new DelegateCommand<TodoItem>(OnDeleteItemCommandExecuted);
            TodoItemTappedCommand = new DelegateCommand<TodoItem>(OnTodoItemTappedCommandExecuted);
        }

#if (UseRealm)
        public IEnumerable<TodoItem> TodoItems { get; set; }
#elseif (UseMvvmHelpers)
        public ObservableRangeCollection<TodoItem> TodoItems { get; set; }
#else
        public ObservableCollection<TodoItem> TodoItems { get; set; }
#endif

        public DelegateCommand AddItemCommand { get; }

        public DelegateCommand<TodoItem> DeleteItemCommand { get; }

        public DelegateCommand<TodoItem> TodoItemTappedCommand { get; }

#if (UseAzureMobileClient)
        public override async void OnNavigatedTo(NavigationParameters parameters)
#else
        public override void OnNavigatedTo(NavigationParameters parameters)
#endif
        {
            IsBusy = true;
            switch(parameters.GetNavigationMode())
            {
                case NavigationMode.Back:
#if (UseAzureMobileClient)
    #if (UseMvvmHelpers)
                    TodoItems.ReplaceRange(await _dataContext.TodoItems.ReadAllItemsAsync());
    #else
                    Todoitems.Clear();
                    foreach (var item in await _dataContext.TodoItems.ReadAllItemsAsync())
                        TodoItems.Add(item);
    #endif
#elseif (UseRealm)
                    // Do anything you want to do only when Navigating Back to the View
#else
                    if(parameters.ContainsKey("todoItem"))
                    {
                        TodoItems.Add(parameters.GetValue<TodoItem>("todoItem"));
                    }
#endif
                    break;
                case NavigationMode.New:
#if (UseAzureMobileClient)
    #if (UseMvvmHelpers)
                    await _cloudService.LoginAsync();
                    TodoItems.AddRange(await _dataContext.TodoItems.ReadAllItemsAsync());
    #else
                    foreach (var item in await _dataContext.TodoItems.ReadAllItemsAsync())
                        TodoItems.Add(item);
    #endif
#elseif (UseRealm)
                    TodoItems = _realm.All<TodoItem>();
#else
    #if (UseMvvmHelpers)
                    TodoItems.AddRange(parameters.GetValues<string>("todo")
                                         .Select(n => new TodoItem { Name = n }));
    #else
                    foreach (var item in parameters.GetValues<string>("todo"))
                        TodoItems.Add(new TodoItem() { Name = item });
    #endif
#endif
                    break;
            }
            IsBusy = false;
        }

#if (UseRealm)
        private async void OnAddItemCommandExecuted()
        {
            var transaction = _realm.BeginWrite();
            var todoItem = _realm.Add(new TodoItem());
            await _navigationService.PushPopupPageAsync("TodoItemDetail", new NavigationParameters
            {
                { "new", true },
                { "transaction", transaction },
                { "todoItem", todoItem }
            });
        }

        private void OnDeleteItemCommandExecuted(TodoItem item) =>
            _realm.Write(() => _realm.Remove(item));

        private async void OnTodoItemTappedCommandExecuted(TodoItem item) =>
            await _navigationService.PushPopupPageAsync("TodoItemDetail", new NavigationParameters
            {
                { "todoItem", item },
                { "transaction", _realm.BeginWrite() }
            });
#else
        private async void OnAddItemCommandExecuted() => 
            await _navigationService.PushPopupPageAsync("TodoItemDetail", new NavigationParameters
            {
                { "new", true },
                { "todoItem", new TodoItem() }
            });

        private void OnDeleteItemCommandExecuted(TodoItem item) =>
            TodoItems.Remove(item);

        private async void OnTodoItemTappedCommandExecuted(TodoItem item) =>
            await _navigationService.PushPopupPageAsync("TodoItemDetail", "todoItem", item);
#endif
    }
}
