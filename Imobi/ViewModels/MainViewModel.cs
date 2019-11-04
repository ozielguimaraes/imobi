namespace Imobi.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            Title = "Carteira";
            Menu = Menu ?? new MenuViewModel();
        }

        private MenuViewModel _menu;

        public MenuViewModel Menu
        {
            get => _menu;
            set => SetProperty(ref _menu, value);
        }
    }
}