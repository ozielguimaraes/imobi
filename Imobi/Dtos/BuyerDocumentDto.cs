namespace Imobi.Dtos
{
    public class BuyerDocumentDto
    {
        public BuyerDocumentDto(string buyerDocumentType, FilePickedDto file)
        {
            BuyerDocumentType = buyerDocumentType;
            File = file;
        }

        public string BuyerDocumentType { get; private set; }
        public FilePickedDto File { get; private set; }
        public string Image => string.IsNullOrWhiteSpace(BuyerDocumentType) ? "ic_x_red" : "ic_check_green";
    }
}