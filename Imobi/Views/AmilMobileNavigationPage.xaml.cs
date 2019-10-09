using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Imobi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AmilMobileNavigationPage : NavigationPage
    {
        public AmilMobileNavigationPage()
        {
            InitializeComponent();
        }

        public AmilMobileNavigationPage(Page root) : base(root)
        {
            InitializeComponent();
        }
    }
}