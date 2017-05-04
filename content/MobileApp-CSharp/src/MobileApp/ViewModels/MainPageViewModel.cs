using System;
using System.Collections.ObjectModel;
#if (UseMvvmHelpers)
using MvvmHelpers;
#endif
using Prism.Commands;
#if (!UseMvvmHelpers)
using Prism.Mvvm;
#endif
using Prism.Navigation;
using Prism.Services;
using MobileApp.Models;

namespace MobileApp.ViewModels
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
            Title = "Main Page";
            #endif
            TodoItems = new ObservableCollection<TodoItem>();
            TodoItemTappedCommand = new DelegateCommand<TodoItem>(OnTodoItemTappedCommandExecuted);
        }

        public ObservableCollection<TodoItem> TodoItems { get; set; }

        public DelegateCommand<TodoItem> TodoItemTappedCommand { get; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            foreach (var item in parameters.GetValues<string>("todo"))
                TodoItems.Add(new TodoItem() { Name = item });
        }

        private async void OnTodoItemTappedCommandExecuted(TodoItem item)
        {
            await _pageDialogService.DisplayAlertAsync("Item Tapped", item.Name, "Ok");
        }
    }
}
