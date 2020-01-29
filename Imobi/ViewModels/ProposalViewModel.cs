using Imobi.Dtos;
using Imobi.IoC;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Imobi.ViewModels
{
    public class ProposalViewModel : BaseViewModel
    {
        #region Public Properties

        public bool AllFiledsFilled
        {
            get => _allFiledsFilled;
            set => SetProperty(ref _allFiledsFilled, value);
        }

        public ObservableCollection<BuyerViewModel> Buyers
        {
            get => _buyers;
            set
            {
                _buyers = value;
                OnPropertyChanged();

                ShowBuyers = Buyers?.Any() ?? false;
            }
        }

        public BuyerViewModel BuyerSelected
        {
            get => _buyerSelected;
            set
            {
                _buyerSelected = value;
                OnPropertyChanged(nameof(BuyerSelected));
                ShowBuyerOptions = !(BuyerSelected is null);
                CanGoToFormTab = value?.Documents.Any() ?? false;
                CanGoToFlowTab = CanGoToFormTab && AllFiledsFilled;
            }
        }

        public bool CanGoToFlowTab
        {
            get => _canGoToFlowTab;
            set => SetProperty(ref _canGoToFlowTab, value);
        }

        public bool CanGoToFormTab
        {
            get => _canGoToFormTab;
            set => SetProperty(ref _canGoToFormTab, value);
        }

        public ICommand EnableBuyerRegisterCommand => new Command(async () => await EnableBuyerRegisterAsync());

        public ProposalFlowViewModel Flow
        {
            get => _flow;
            set => SetProperty(ref _flow, value);
        }

        public ICommand GoToDocsTabCommand => new Command(async () => GoToDocsTab());

        public ICommand GoToFlowTabCommand => new Command(async () => GoToFlowTab());

        public ICommand GoToFormTabCommand => new Command(async () => GoToFormTab());

        public bool IsDocsTabVisible
        {
            get => _isDocsTabVisible;
            set => SetProperty(ref _isDocsTabVisible, value);
        }

        public bool IsFlowTabVisible
        {
            get => _isFlowTabVisible;
            set => SetProperty(ref _isFlowTabVisible, value);
        }

        public bool IsFormTabVisible
        {
            get => _isFormTabVisible;
            set => SetProperty(ref _isFormTabVisible, value);
        }

        public bool ShowBuyerOptions
        {
            get => _showBuyerOptions;
            set => SetProperty(ref _showBuyerOptions, value);
        }

        public bool ShowBuyers
        {
            get => _showBuyers;
            set => SetProperty(ref _showBuyers, value);
        }

        #endregion Public Properties



        #region Private Fields + Structs

        private bool _allFiledsFilled;

        private ObservableCollection<BuyerViewModel> _buyers;

        private BuyerViewModel _buyerSelected;

        private bool _canGoToFlowTab;

        private bool _canGoToFormTab;

        private ProposalFlowViewModel _flow = new ProposalFlowViewModel();

        private bool _isDocsTabVisible;

        private bool _isFlowTabVisible;

        private bool _isFormTabVisible;

        private bool _showBuyerOptions;

        private bool _showBuyers;

        #endregion Private Fields + Structs

        #region Public Constructors + Destructors

        public ProposalViewModel()
        {
            Title = "Proposta";
        }

        #endregion Public Constructors + Destructors



        #region Public Methods

        public override async Task InitializeAsync(object parameter)
        {
            try
            {
                if (parameter is null) return;

                if (parameter is ProposalDto item)
                {
                    if (!IsBusy) IsBusy = true;
                    //TODO Get buyers from servive
                    Buyers = new ObservableCollection<BuyerViewModel>();
                    //Buyers.Add(new BuyerViewModel
                    //{
                    //    Form = new ProposalFormViewModel
                    //    {
                    //        Cpf = "000.000.001-91",
                    //        FullName = item.Client
                    //    },
                    //});
                }
            }
            catch (Exception ex)
            {
                ExceptionService.TrackError(ex, nameof(ProposalViewModel), nameof(InitializeAsync));
                await MessageService.ShowAsync("Não foi possível carregar a proposta");
            }
            finally { IsBusy = false; }
        }

        #endregion Public Methods



        #region Private Methods

        private async Task EnableBuyerRegisterAsync()
        {
            BuyerSelected = new BuyerViewModel();
            EnableDocsTab();
        }

        private void EnableDocsTab()
        {
            IsDocsTabVisible = true;
            IsFlowTabVisible = false;
            IsFormTabVisible = false;
        }

        private void EnableFlowTab()
        {
            IsFlowTabVisible = true;
            IsDocsTabVisible = false;
            IsFormTabVisible = false;
        }

        private void EnableFormTab()
        {
            IsFormTabVisible = true;
            IsFlowTabVisible = false;
            IsDocsTabVisible = false;
            BuyerSelected.Form = BuyerSelected.Form ?? new ProposalFormViewModel();
            BuyerSelected.Form.LoadPickers();
        }

        private void GoToDocsTab()
        {
            EnableDocsTab();
        }

        private void GoToFlowTab()
        {
            EnableFlowTab();
        }

        private void GoToFormTab()
        {
            EnableFormTab();
        }

        #endregion Private Methods
    }
}