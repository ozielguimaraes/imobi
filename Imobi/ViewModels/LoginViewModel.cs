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
            if (!Application.Current.Properties.ContainsKey("Logged"))
                Application.Current.Properties.Add("Logged", true);

            await NavigationService.NavigateToAsync<MainViewModel>();
        }
    }
}