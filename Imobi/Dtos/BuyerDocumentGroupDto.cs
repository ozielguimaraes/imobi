using System.Collections.ObjectModel;

namespace Imobi.Dtos
{
    public class BuyerDocumentGroupDto
    {
        public BuyerDocumentGroupDto(BuyerDocumentDto buyerDocument)
        {
            BuyerDocuments = new ObservableCollection<BuyerDocumentDto> { buyerDocument };
        }

        public ObservableCollection<BuyerDocumentDto> BuyerDocuments { get; private set; }
    }
}