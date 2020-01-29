using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Imobi.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand SigninCommand => new Command(async () => await SigninAsync());

        private async Task SigninAsync()
        {
            await NavigationService.NavigateToAsync<MainViewModel>();
        }
    }
}