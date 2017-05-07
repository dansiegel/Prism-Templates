using Prism.Navigation;
using Xamarin.Forms;

namespace MobileApp.Views
{
    public partial class Template : TabbedPage, INavigatingAware
    {
        public Template()
        {
            InitializeComponent();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            foreach(var child in Children)
            {
                (child as INavigatingAware)?.OnNavigatingTo(parameters);
                (child.BindingContext as INavigatingAware)?.OnNavigatingTo(parameters);
            }
        }
    }
}