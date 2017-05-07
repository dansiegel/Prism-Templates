using Prism.Navigation;
using Xamarin.Forms;

namespace MobileApp.Views
{
    public partial class Template : MasterDetailPage, IMasterDetailPageOptions
    {
        public Template()
        {
            InitializeComponent();
        }

        public bool IsPresentedAfterNavigation
        {
            get { return Device.Idiom != TargetIdiom.Phone; }
        }
    }
}