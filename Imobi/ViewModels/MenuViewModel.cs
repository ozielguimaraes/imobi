using Imobi.Enums;
using Imobi.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Imobi.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public MenuViewModel()
        {
            LoadItems();
        }

        public ICommand LoadMenuItemsCommand => new Command(LoadItems);
        private ObservableCollection<MainMenuItem> _items = new ObservableCollection<MainMenuItem>();

        public ObservableCollection<MainMenuItem> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private void OnMenuItemTapped(object menuItemTappedEventArgs)
        {
            var menuItem = ((menuItemTappedEventArgs as ItemTappedEventArgs)?.Item as MainMenuItem);

            if (!(menuItem is null) && menuItem.MenuText == "Log out")
            {
                NavigationService.ClearBackStack();
            }

            var type = menuItem?.ViewModelToLoad;
            NavigationService.NavigateToAsync(type);
        }

        private void LoadItems()
        {
            Items.Add(new MainMenuItem
            {
                MenuText = "Propostas",
                ViewModelToLoad = typeof(ProposalListViewModel),
                MenuItemType = MenuItemType.ProposalList
            });
            Items.Add(new MainMenuItem
            {
                MenuText = "Nova proposta",
                ViewModelToLoad = typeof(ProposalViewModel),
                MenuItemType = MenuItemType.ProposalNew
            });
        }
    }
}