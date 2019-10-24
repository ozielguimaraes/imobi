using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Imobi.ViewModels
{
    public class ProposalViewModel : BaseViewModel
    {
        public ProposalViewModel()
        {
            BuyerSelected = new BuyerViewModel();
        }

        public ICommand GoToRecordCommand => new Command(async () => await GoToRecordAsync());

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
            set => SetProperty(ref _buyerSelected, value);
        }

        private ObservableCollection<BuyerViewModel> _buyers;

        public ObservableCollection<BuyerViewModel> Buyers
        {
            get => _buyers;
            set => SetProperty(ref _buyers, value);
        }

        public bool ShowBuyerOptions => !(BuyerSelected is null);

        private async Task GoToRecordAsync()
        {
        }
    }
}