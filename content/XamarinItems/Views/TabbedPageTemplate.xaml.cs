using Prism.Navigation;
using Xamarin.Forms;

namespace MobileApp.Views
{
    public partial class ItemTemplate : TabbedPage, INavigatingAware
    {
        public ItemTemplate()
        {
            InitializeComponent();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            foreach(var child in Children)
            {
                // Uncomment this if you actually require a child page to also be INavigatingAware.
                // (child as INavigatingAware)?.OnNavigatingTo(parameters);
                (child.BindingContext as INavigatingAware)?.OnNavigatingTo(parameters);
            }
        }
    }
}