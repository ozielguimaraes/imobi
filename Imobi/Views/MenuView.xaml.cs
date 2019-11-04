using Imobi.IoC;
using Imobi.Models;
using Imobi.Services.Interfaces;
using Imobi.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Imobi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuView : ContentPage
    {
        public MenuView()
        {
            InitializeComponent();
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
            var item = (MainMenuItem)e.SelectedItem;
            if (item is null) return;

            var vm = (BaseViewModel)Bootstraper.Resolve(item.ViewModelToLoad);
            var nav = Bootstraper.Resolve<INavigationService>();
            Device.BeginInvokeOnMainThread(() => nav.NavigateToAsync(vm.GetType()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is null) return;
            var vm = (MenuViewModel)BindingContext;
            if (vm.Items.Count == 0) vm.LoadMenuItemsCommand.Execute(null);
        }
    }
}