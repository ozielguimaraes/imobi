using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Imobi.ViewModels
{
    public class ProposalViewModel : BaseViewModel
    {
        public ProposalViewModel()
        {
        }

        public ICommand EnableBuyerRegisterCommand => new Command(async () => await EnableBuyerRegisterAsync());
        public ICommand GoToDocsTabCommand => new Command(async () => await GoToDocsTabAsync());
        public ICommand GoToFormTabCommand => new Command(async () => await GoToFormTabAsync());
        public ICommand GoToFowTabCommand => new Command(async () => await GoToFlowTabAsync());

        private ProposalFlowViewModel _flow;

        public ProposalFlowViewModel Flow
        {
            get => _flow;
            set => SetProperty(ref _flow, value);
        }

        private BuyerViewModel _buyerSelected;

        public BuyerViewModel BuyerSelected
        {
            get => _buyerSelected;
            set
            {
                _buyerSelected = value;
                OnPropertyChanged();

                ShowBuyerOptions = !(BuyerSelected is null);
            }
        }

        private bool _isDocsTabVisible;

        public bool IsDocsTabVisible
        {
            get => _isDocsTabVisible;
            set => SetProperty(ref _isDocsTabVisible, value);
        }

        private bool _isFormTabVisible;

        public bool IsFormTabVisible
        {
            get => _isFormTabVisible;
            set => SetProperty(ref _isFormTabVisible, value);
        }

        private bool _isFlowTabVisible;

        public bool IsFlowTabVisible
        {
            get => _isFlowTabVisible;
            set => SetProperty(ref _isFlowTabVisible, value);
        }

        private bool _showBuyers;

        public bool ShowBuyers
        {
            get => _showBuyers;
            set => SetProperty(ref _showBuyers, value);
        }

        private bool _showBuyerOptions;

        public bool ShowBuyerOptions
        {
            get => _showBuyerOptions;
            set => SetProperty(ref _showBuyerOptions, value);
        }

        private ObservableCollection<BuyerViewModel> _buyers;

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

        private async Task EnableBuyerRegisterAsync()
        {
            BuyerSelected = new BuyerViewModel();
            EnableDocsTab();
        }

        private async Task GoToDocsTabAsync()
        {
            EnableDocsTab();
        }

        private async Task GoToFormTabAsync()
        {
            EnableFormTab();
        }

        private async Task GoToFlowTabAsync()
        {
            EnableFlowTab();
        }

        private void EnableDocsTab()
        {
            IsDocsTabVisible = true;
            IsFlowTabVisible = false;
            IsFormTabVisible = false;
        }

        private void EnableFormTab()
        {
            IsFormTabVisible = true;
            IsFlowTabVisible = false;
            IsDocsTabVisible = false;
        }

        private void EnableFlowTab()
        {
            IsFlowTabVisible = true;
            IsDocsTabVisible = false;
            IsFormTabVisible = false;
        }
    }
}