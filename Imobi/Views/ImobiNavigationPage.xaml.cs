using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Imobi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImobiNavigationPage : NavigationPage
    {
        public ImobiNavigationPage()
        {
            InitializeComponent();
        }

        public ImobiNavigationPage(Page root) : base(root)
        {
            InitializeComponent();
        }
    }
}