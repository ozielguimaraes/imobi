using Xamarin.Forms;
using Imobi.IoC;
using Imobi.Services.Interfaces;
using System.Threading.Tasks;

namespace Imobi
{
    public partial class App : Application
    {
        #region Public Constructors + Destructors

        public App()
        {
            InitializeComponent();
            InitializeApp();
        }

        #endregion Public Constructors + Destructors



        #region Protected Methods

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override async void OnStart()
        {
            base.OnStart();

            await InitializeNavigationAsync();

            base.OnResume();
        }

        #endregion Protected Methods

        #region Private Methods

        private void InitializeApp()
        {
            Bootstraper.RegisterDependencies();
        }

        private async Task InitializeNavigationAsync()
        {
            var navigationService = Bootstraper.Resolve<INavigationService>();
            await navigationService.InitializeAsync();
        }

        #endregion Private Methods
    }
}