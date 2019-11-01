using Imobi.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Imobi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProposalListView : ContentPage
    {
        public ProposalListView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is null) return;
            var vm = (ProposalListViewModel)BindingContext;
            if (vm.Items.Count == 0) vm.LoadItemsCommand.Execute(null);
        }
    }
}