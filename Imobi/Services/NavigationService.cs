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
                    await NavigateToAsync<LoginViewModel>();
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
                    CurrentApplication.MainPage = page;
                }
                //When the user was already login and dindt pass throw login page or pass throw login page with success
                else if (page is MasterDetailPage masterDetail && (CurrentApplication.MainPage is null || CurrentApplication.MainPage is LoginView))
                {
                    masterDetail.Detail = new ImobiNavigationPage(GetHomePage(parameter));
                    CurrentApplication.MainPage = masterDetail;
                }
                else
                {
                    if (CurrentApplication.MainPage is MasterDetailPage masterDetail1)
                    {
                        var master = new MainView
                        {
                            Detail = new ImobiNavigationPage(page),
                            Master = GetMenuPage()
                        };
                        
                        (CurrentApplication.MainPage as MasterDetailPage).IsPresented = false;
                        await (CurrentApplication.MainPage as MasterDetailPage).Navigation.PushModalAsync(master);
                    }
                    else
                    {
                        (CurrentApplication.MainPage as MasterDetailPage).IsPresented = false;
                        await (CurrentApplication.MainPage as MasterDetailPage).Navigation.PushAsync(new ImobiNavigationPage(page));
                    } 
                }

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
        }

        private Page GetHomePage(object parameter)
        {
            return CreateAndBindPage(typeof(MyWalletViewModel), parameter);
        }
        

        private Page GetMenuPage()
        {
            return CreateAndBindPage(typeof(MenuViewModel), null);
        }


        #endregion Private Methods
    }
}