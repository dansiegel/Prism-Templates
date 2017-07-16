using Rg.Plugins.Popup.Pages;

namespace Company.MobileApp.Views
{
    public partial class TodoItemDetail : PopupPage
    {
        public TodoItemDetail()
        {
            InitializeComponent();
        }

        // Prevent hide popup
        protected override bool OnBackButtonPressed() => true;
    }
}