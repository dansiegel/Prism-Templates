using System;
using Prism.Navigation;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using MobileApp.Models;

namespace MobileApp.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigatedAware
    {
        public MainPageViewModel()
        {
            TodoItems = new ObservableCollection<TodoItem>();
        }

        public ObservableCollection<TodoItem> TodoItems { get; set; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            foreach (var item in parameters.GetValues<string>("todo"))
                TodoItems.Add(new TodoItem() { Name = item });
        }
    }
}
