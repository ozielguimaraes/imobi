using Imobi.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Imobi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProposalView : ContentPage
    {
        public ProposalView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is null) return;
            var vm = (ProposalViewModel)BindingContext;
            if (vm.Flow.PreKeys.Count == 0) vm.Flow.LoadPreKeysCommand.Execute(null);
        }
    }
}