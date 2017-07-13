using System;
#if (UseMvvmHelpers)
using System.Linq;
using MvvmHelpers;
#else
using System.Collections.ObjectModel;
#endif
using Prism.Commands;
#if (!UseMvvmHelpers)
using Prism.Mvvm;
#endif
using Prism.Navigation;
using Prism.Services;
using Company.MobileApp.Models;
#if (Localization)
using Company.MobileApp.Strings;
#endif

namespace Company.MobileApp.ViewModels
{
#if (UseMvvmHelpers)
    public class MainPageViewModel : BaseViewModel, INavigatedAware
#else
    public class MainPageViewModel : BindableBase, INavigatedAware
#endif
    {
        private IPageDialogService _pageDialogService { get; }
        public MainPageViewModel(IPageDialogService pageDialogService)
        {
            _pageDialogService = pageDialogService;

#if (UseMvvmHelpers)
    #if (Localization)
            Title = Resources.MainPageTitle;
    #else
            Title = "Main Page";
    #endif
            TodoItems = new ObservableRangeCollection<TodoItem>();
#else
            TodoItems = new ObservableCollection<TodoItem>();
#endif
            TodoItemTappedCommand = new DelegateCommand<TodoItem>(OnTodoItemTappedCommandExecuted);
        }

#if (UseMvvmHelpers)
        public ObservableRangeCollection<TodoItem> TodoItems { get; set; }
#else
        public ObservableCollection<TodoItem> TodoItems { get; set; }
#endif

        public DelegateCommand<TodoItem> TodoItemTappedCommand { get; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
#if (UseMvvmHelpers)
            TodoItems.AddRange(parameters.GetValues<string>("todo")
                                         .Select(n => new TodoItem { Name = n }));
#else
            foreach (var item in parameters.GetValues<string>("todo"))
                TodoItems.Add(new TodoItem() { Name = item });
#endif
        }

        private async void OnTodoItemTappedCommandExecuted(TodoItem item)
        {
            await _pageDialogService.DisplayAlertAsync("Item Tapped", item.Name, "Ok");
        }
    }
}
