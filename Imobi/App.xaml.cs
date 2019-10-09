using Xamarin.Forms;
using Imobi.IoC;
using Imobi.Services.Interfaces;

namespace Imobi
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitializeApp();
            InitializeNavigation();
        }

        private void InitializeApp()
        {
            Bootstraper.RegisterDependencies();
        }

        private async void InitializeNavigation()
        {
            var navigationService = Bootstraper.Resolve<INavigationService>();
            await navigationService.InitializeAsync();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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