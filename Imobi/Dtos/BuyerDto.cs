using System;
using System.Collections.ObjectModel;

namespace Imobi.Dtos
{
    public class BuyerDto
    {
        public BuyerDto()
        {
            Documents = new ObservableCollection<BuyerDocumentGroupDto>();
        }

        public string Name { get; set; }

        public ObservableCollection<BuyerDocumentGroupDto> Documents { get; private set; }

        internal void NewDocumentAdded(string documentType, FilePickedDto file)
        {
            var buyerDocument = new BuyerDocumentDto(documentType, file);
            var fileAddedTolist = false;
            foreach (var item in Documents)
            {
                if (item.BuyerDocuments.Count < 4)
                {
                    item.BuyerDocuments.Add(buyerDocument);
                    fileAddedTolist = true;
                }
            }
            if (!fileAddedTolist)
            {
                var newDocument = new BuyerDocumentGroupDto(buyerDocument);
                Documents.Add(newDocument);
            }
        }
    }
}