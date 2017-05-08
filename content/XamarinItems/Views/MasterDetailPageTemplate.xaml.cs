using Prism.Navigation;
using Xamarin.Forms;

namespace MobileApp.Views
{
    public partial class ItemTemplate : MasterDetailPage, IMasterDetailPageOptions
    {
        public ItemTemplate()
        {
            InitializeComponent();
        }

        public bool IsPresentedAfterNavigation
        {
            get { return Device.Idiom != TargetIdiom.Phone; }
        }
    }
}