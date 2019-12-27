namespace Imobi.ViewModels
{
    public class ProposalDocsViewModel : BaseViewModel
    {
        private BuyerViewModel _buyer;

        public BuyerViewModel Buyer
        {
            get => _buyer;
            set => SetProperty(ref _buyer, value);
        }

        public ProposalDocsViewModel()
        {
            Buyer = Buyer ?? new BuyerViewModel();
        }
    }
}