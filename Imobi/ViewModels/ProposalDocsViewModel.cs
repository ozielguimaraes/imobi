using Imobi.Dtos;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Imobi.ViewModels
{
    public class ProposalDocsViewModel : BaseViewModel
    {
        private const string CAMERA = "Câmera";
        private const string GALERIA = "Galeria";
        private readonly string[] AttachFileOptions = { CAMERA, GALERIA };
        //private readonly ISharedCam _sharedCam;

        public ProposalDocsViewModel(ISharedCam sharedCam)
        {
            //_sharedCam = sharedCam;
        }

        public ICommand IncludeAttachmentCommand => new Command(async () => await IncludeAttachmentAsync());

        private async Task IncludeAttachmentAsync()
        {
            try
            {
                var addFileFrom = await Application.Current.MainPage.DisplayActionSheet("Escolha onde deseja enviar os arquivos", "Cancel", null, AttachFileOptions);

                FilePickedDto media = null;

                if (addFileFrom.Equals(CAMERA))
                {
                    var file = await _sharedCam.TakePhotoAsync();
                    if (file != null) media = new FilePickedDto(file);
                }
                else if (addFileFrom.Equals(GALERIA))
                {
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("----- START ERROR -------");
                Console.WriteLine(ex.Message);
                Console.WriteLine("----- END ERROR -------");
            }
        }
    }
}