using System;
using Prism.Mvvm;

namespace Company.MobileApp.ModuleName.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        public ViewAViewModel()
        {
#if (Localization)
            Title = Strings.Resources.ViewATitle;
#else
            Title = "View A";
#endif
        }

        public string Title { get; set; }
    }
}