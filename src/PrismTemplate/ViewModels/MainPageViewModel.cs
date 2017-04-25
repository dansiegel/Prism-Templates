using System;
using PropertyChanged;
using Xamarin.Forms;

namespace PrismTemplate.ViewModels
{
    [ImplementPropertyChanged]
    public class MainPageViewModel
    {
        public string Message { get; set; }
    }
}

