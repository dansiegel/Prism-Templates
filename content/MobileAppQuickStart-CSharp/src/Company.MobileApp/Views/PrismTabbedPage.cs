using Prism.Common;
using Prism.Navigation;
using Xamarin.Forms;

namespace Company.MobileApp.Views
{
    // NOTE: This class is being provided to better assist app development with Tabbed Pages until this feature is
    // included out of the box in the Navigation Service with Prism 7
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