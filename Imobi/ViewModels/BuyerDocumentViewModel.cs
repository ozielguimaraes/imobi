using Imobi.Dtos;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Imobi.ViewModels
{
    public class BuyerDocumentViewModel : BaseViewModel
    {
        public BuyerDocumentViewModel(string buyerDocumentType, FilePickedDto file)
        {
            BuyerDocumentType = buyerDocumentType;
            File = file;
        }

        public string BuyerDocumentType { get; private set; }
        public FilePickedDto File { get; private set; }
        public string Image => string.IsNullOrWhiteSpace(BuyerDocumentType) ? "ic_x_red" : "ic_check_green";
    }
}