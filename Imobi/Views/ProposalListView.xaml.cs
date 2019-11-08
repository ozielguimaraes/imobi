using Imobi.Dtos;
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

        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is ProposalDto item)) return;
            // Manually deselect item.
            ItemsListView.SelectedItem = null;

            var vm = (ProposalListViewModel)BindingContext;

            await vm.NavigationService.NavigateToAsync<ProposalViewModel>(item);
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