using Imobi.ViewModels;
using System.Collections.ObjectModel;

namespace Imobi.Dtos
{
    public class BuyerDocumentGroupDto
    {
        public BuyerDocumentGroupDto(BuyerDocumentViewModel buyerDocument)
        {
            BuyerDocuments = new ObservableCollection<BuyerDocumentViewModel> { buyerDocument };
        }

        public ObservableCollection<BuyerDocumentViewModel> BuyerDocuments { get; private set; }
    }
}