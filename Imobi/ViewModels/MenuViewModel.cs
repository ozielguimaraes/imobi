using Imobi.Enums;
using Imobi.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Imobi.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public MenuViewModel()
        {
            MenuItems = new ObservableCollection<MainMenuItem>();
            LoadMenuItems();
        }

        private ObservableCollection<MainMenuItem> _menuItems;

        public ObservableCollection<MainMenuItem> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
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

        private void LoadMenuItems()
        {
            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Propostas",
                ViewModelToLoad = typeof(MainViewModel),
                MenuItemType = MenuItemType.ProposalList
            });
        }
    }
}