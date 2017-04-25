using System;
using PrismTemplate.i18n;
using PropertyChanged;
using Xamarin.Forms;

namespace PrismTemplate.ViewModels
{
    [ImplementPropertyChanged]
    public class MainPageViewModel
    {
        public MainPageViewModel()
        {
            Title = AppResources.MainPageTitle;
        }

        public string Title { get; }

        public string Message { get; set; }
    }
}

