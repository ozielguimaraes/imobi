﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Imobi.Services;
using Imobi.Views;
using System.Threading.Tasks;
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

        private async Task InitializeNavigation()
        {
            var navigationService = Bootstraper.AppContainer.Resolve<INavigationService>();
            await navigationService.InitializeAsync();
        }

        private void InitializeApp()
        {
            Bootstrape  r.AppContainer.RegisterDependencies();
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