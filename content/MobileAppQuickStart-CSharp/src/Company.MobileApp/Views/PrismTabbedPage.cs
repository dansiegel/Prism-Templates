using Prism.Common;
using Prism.Navigation;
using Xamarin.Forms;

namespace Company.MobileApp.Views
{
    public class PrismTabbedPage : TabbedPage, INavigatingAware
    {
        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
            foreach(var child in Children)
            {
                PageUtilities.OnNavigatingTo(child, parameters);

                if(child is NavigationPage navPage)
                {
                    PageUtilities.OnNavigatingTo(navPage.CurrentPage, parameters);
                }
            }
        }
    }
}