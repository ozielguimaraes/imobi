using Imobi.IoC;
using Imobi.Services.Interfaces;
using Imobi.ViewModels;
using Imobi.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Imobi.Services
{
    public class NavigationService : INavigationService
    {
        #region Protected Properties

        protected Application CurrentApplication => Application.Current;

        #endregion Protected Properties



        #region Private Fields + Structs

        private readonly Dictionary<Type, Type> _mappings;
        private readonly Dictionary<Type, Type> _mappingsDetailPage;

        #endregion Private Fields + Structs

        #region Public Constructors + Destructors

        public NavigationService()
        {
            _mappings = new Dictionary<Type, Type>();
            _mappingsDetailPage = new Dictionary<Type, Type>();
            CreatePageViewModelMappings();
            CreatePageDetailsViewModelMappings();
        }

        #endregion Public Constructors + Destructors



        #region Public Methods

        public async Task ClearBackStack()
        {
            await CurrentApplication.MainPage.Navigation.PopToRootAsync();
        }

        public async Task InitializeAsync()
        {
            try
            {
                if (Application.Current.Properties?.ContainsKey("Logged") ?? false)
                {
                    await NavigateToAsync<MainViewModel>();
                }
                else
                {
                    await NavigateToAsync<AttendanceChannelViewModel>();
                    //await NavigateToAsync<RegistrationViewModel>();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public async Task NavigateBackAsync()
        {
            if (CurrentApplication.MainPage is MainView mainPage)
            {
                await mainPage.Detail.Navigation.PopAsync();
            }
            else if (CurrentApplication.MainPage != null)
            {
                await CurrentApplication.MainPage.Navigation.PopAsync();
            }
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            return InternalNavigateToAsync(viewModelType, parameter);
        }

        public async Task PopToRootAsync()
        {
            if (CurrentApplication.MainPage is MainView mainPage)
            {
                await mainPage.Detail.Navigation.PopToRootAsync();
            }
        }

        public virtual Task RemoveLastFromBackStackAsync()
        {
            if (CurrentApplication.MainPage is MainView mainPage)
            {
                mainPage.Detail.Navigation.RemovePage(
                  mainPage.Detail.Navigation.NavigationStack[mainPage.Detail.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        #endregion Public Methods



        #region Protected Methods

        protected Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType is null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            BaseViewModel viewModel = Bootstraper.Resolve(viewModelType) as BaseViewModel;
            page.BindingContext = viewModel;

            return page;
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return _mappings[viewModelType];
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            try
            {
                Page page = CreateAndBindPage(viewModelType, parameter);

                if (page is RegisterView)
                {
                    CurrentApplication.MainPage = page;
                }
                else if (page is LoginView)
                {
                    //   //When the app load for the first time
                    //if (CurrentApplication.MainPage is null)
                    //   {
                    //       var mainView = page as MainView;
                    //       Page detailPage = CreateAndBindPage(typeof(MyWalletViewModel), parameter);

                    //       mainView.Detail = new ImobiNavigationPage(detailPage);
                    //       CurrentApplication.MainPage = mainView;
                    //   }
                    CurrentApplication.MainPage = page;
                }
                //When the user was already login and dindt pass throw login page
                else if (page is MasterDetailPage masterDetail && CurrentApplication.MainPage is null)
                {
                    masterDetail.Detail = new ImobiNavigationPage(GetHomePage(parameter));
                    CurrentApplication.MainPage = masterDetail;
                }
                //When the user pass throw login page with success
                else if (CurrentApplication.MainPage is LoginView && page is MasterDetailPage masterDetailPage)
                {
                    masterDetailPage.Detail = new ImobiNavigationPage(GetHomePage(parameter));

                    await RemoveLastFromBackStackAsync();
                    await CurrentApplication.MainPage.Navigation.PushModalAsync(masterDetailPage);
                }
                else
                {
                    (CurrentApplication.MainPage as MasterDetailPage).IsPresented = false;
                    await (CurrentApplication.MainPage as MasterDetailPage).Navigation.PushModalAsync(new ImobiNavigationPage(page));
                }
                // else
                //{
                //        if (page is MasterDetailPage masterDetailPage)
                //            await masterDetailPage.Detail.Navigation.PushAsync(page);
                //        else await page.Navigation.PushAsync(page);

                //}
                //else if (IsMasterDetailPage(page))
                //{
                //}
                ////Is detail page
                //else if (_mappingsDetailPage.ContainsKey(viewModelType))
                //{
                //    await CurrentApplication.MainPage.Navigation.PushAsync(page);
                //}
                //else if (CurrentApplication.MainPage is MyWalletView)
                //{
                //    var mainPage = CurrentApplication.MainPage as MainView;

                //    if (mainPage.Detail is ImobiNavigationPage navigationPage)
                //    {
                //        var currentPage = navigationPage.CurrentPage;

                //        if (currentPage.GetType() != page.GetType())
                //        {
                //            await navigationPage.PushAsync(page);
                //        }
                //    }
                //    else
                //    {
                //        navigationPage = new ImobiNavigationPage(page);
                //        mainPage.Detail = navigationPage;
                //    }

                //    mainPage.IsPresented = false;
                //}
                //else
                //{
                //    if (CurrentApplication.MainPage is ImobiNavigationPage navigationPage)
                //        await navigationPage.PushAsync(page);
                //    else
                //    {
                //        if (page is MainView mainView)
                //        {
                //            Page detailPage = CreateAndBindPage(typeof(MyWalletViewModel), parameter);

                //            mainView.Detail = new ImobiNavigationPage(detailPage);
                //            CurrentApplication.MainPage = mainView;
                //        }
                //        else
                //        {
                //            CurrentApplication.MainPage = new ImobiNavigationPage(page);
                //        }
                //        //else CurrentApplication.MainPage = new ImobiNavigationPage(page);
                //    }
                //}

                await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
            }
            catch (Exception ex)
            {
                var exceptionService = Bootstraper.Resolve<IExceptionService>();

                exceptionService.TrackError(ex, nameof(NavigationService), nameof(InternalNavigateToAsync));
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private void CreatePageDetailsViewModelMappings()
        {
            _mappingsDetailPage.Add(typeof(ProposalViewModel), typeof(ProposalView));
        }

        //map to join VM and VIEW dont need ViewModel Locator others cases is resolve trought Utility.ViewModelLocator
        private void CreatePageViewModelMappings()
        {
            _mappings.Add(typeof(MainViewModel), typeof(MainView));//Master Detail
            _mappings.Add(typeof(HomeViewModel), typeof(HomeView));
            _mappings.Add(typeof(MenuViewModel), typeof(MenuView));
            _mappings.Add(typeof(LoginViewModel), typeof(LoginView));
            _mappings.Add(typeof(MyWalletViewModel), typeof(MyWalletView));
            _mappings.Add(typeof(ProposalDocsViewModel), typeof(ProposalDocsView));
            _mappings.Add(typeof(ProposalViewModel), typeof(ProposalView));
            _mappings.Add(typeof(ProposalListViewModel), typeof(ProposalListView));
            _mappings.Add(typeof(RegisterViewModel), typeof(RegisterView));
            _mappings.Add(typeof(AttendanceChannelViewModel), typeof(AttendanceChannelView));
        }

        private Page GetHomePage(object parameter)
        {
            return CreateAndBindPage(typeof(MyWalletViewModel), parameter);
        }

        #endregion Private Methods
    }
}