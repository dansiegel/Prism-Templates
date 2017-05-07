using Rg.Plugins.Popup.Pages;

namespace MobileApp.Views
{
    public class Template : PopupPage
    {
        public Template()
        {
            InitializeComponent();
        }

        // Prevent hide popup
        protected override bool OnBackButtonPressed() => true;
    }
}