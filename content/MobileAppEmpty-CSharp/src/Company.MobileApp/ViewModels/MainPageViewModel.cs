using System;
using System.Linq;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Company.MobileApp.Models;

namespace Company.MobileApp.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigatedAware
    {
        private IPageDialogService _pageDialogService { get; }
        public MainPageViewModel(IPageDialogService pageDialogService)
        {
            _pageDialogService = pageDialogService;

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
