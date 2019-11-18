using Xamarin.Forms;
using Imobi.IoC;
using Imobi.Services.Interfaces;
using System.Threading.Tasks;

namespace Imobi
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitializeApp();
        }

        private void InitializeApp()
        {
            Bootstraper.RegisterDependencies();
        }

        private async Task InitializeNavigationAsync()
        {
            var navigationService = Bootstraper.Resolve<INavigationService>();
            await navigationService.InitializeAsync();
        }

        protected override async void OnStart()
        {
            base.OnStart();

            await InitializeNavigationAsync();

            base.OnResume();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}