using Prism.Common;
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
                PageUtilities.OnNavigatingTo(child, parameters);
            }
        }
    }
}