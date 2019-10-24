using Imobi.Dtos;
using Imobi.Extensions;
using Imobi.IoC;
using Imobi.Managers.File.Interfaces;
using Imobi.Services.Interfaces;
using Imobi.Validations.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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