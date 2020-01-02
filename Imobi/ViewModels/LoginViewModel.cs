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

            EventTrackerService.SendEvent("Usuário efetuando o login", "testeparam", "AQUI TEM QUE APARECER");

            if (!Application.Current.Properties.ContainsKey("Logged"))
                Application.Current.Properties.Add("Logged", true);

            EventTrackerService.SendEvent("Usuário efetuando o login");
            await NavigationService.NavigateToAsync<MainViewModel>();
        }
    }
}