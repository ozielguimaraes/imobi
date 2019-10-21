using Imobi.Dtos;
using Imobi.Extensions;
using Imobi.IoC;
using Imobi.Managers.File.Interfaces;
using Imobi.Services.Interfaces;
using Imobi.Validations.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Imobi.ViewModels
{
    public class BuyerViewModel : BaseViewModel
    {
        private const string CAMERA = "Câmera";
        private const string GALERIA = "Galeria";
        private const string FILES = "Arquivos";
        private readonly string[] AttachFileOptions = { CAMERA, GALERIA, FILES };
        private readonly IFileManager _fileManager;

        public BuyerViewModel()
        {
            _fileManager = Bootstraper.Resolve<IFileManager>();
            Documents = new List<BuyerDocumentViewModel>();
        }

        public ICommand IncludeAttachmentCommand => new Command(async () => await IncludeAttachmentAsync());
        public ICommand OpenBuyerDocumentOptionsCommand => new Command<BuyerDocumentViewModel>(async (item) => await OpenBuyerDocumentOptionsAsync(item));

        private async Task OpenBuyerDocumentOptionsAsync(BuyerDocumentViewModel itemSelected)
        {
            var optionSelected = await MessageService.ShowOptionsAsync("Escolha uma opção", "Visualizar", "Excluir");
            if (optionSelected is null) return;

            if (optionSelected.Equals("Excluir"))
            {
                RemoveDocument(itemSelected);
            }
            else if (optionSelected.Equals("Visualizar"))
            {
            }
        }

        private string _documentType;

        public string DocumentType
        {
            get { return _documentType; }
            set { SetProperty(ref _documentType, value); }
        }

        private List<BuyerDocumentViewModel> _documents;

        public List<BuyerDocumentViewModel> Documents
        {
            get { return _documents; }
            set { SetProperty(ref _documents, value); }
        }

        private ProposalFormViewModel _form;

        public ProposalFormViewModel Form
        {
            get => _form;
            set => SetProperty(ref _form, value);
        }

        private async Task IncludeAttachmentAsync()
        {
            try
            {
                if (IsBusy) return;
                IsBusy = true;
                var proposalFileTypeService = Bootstraper.Resolve<IProposalFileTypeService>();
                var fileTypes = await proposalFileTypeService.GetFileTypes();

                if (!fileTypes?.Any() ?? true)
                {
                    await MessageService.ShowAsync("Não foi possível obter a lista de arquivos necessários, tente novamente mais tarde.");
                    return;
                }

                DocumentType = await MessageService.ShowOptionsAsync("Qual tipo de documento deseja selecionar?", options: fileTypes);
                if (DocumentType is null) return;

                var addFileFrom = await MessageService.ShowOptionsAsync("Agora escolha de onde deseja enviar os arquivos", AttachFileOptions);

                FilePickedDto media = null;

                if (addFileFrom.Equals(CAMERA))
                {
                    var file = await _fileManager.TakePhotoAsync();
                    if (file != null)
                    {
                        media = new FilePickedDto(file);
                        await PickedFileHandler(media);
                    }
                }
                else if (addFileFrom.Equals(FILES))
                {
                    var file = await _fileManager.PickerFilesAsync();
                    if (file != null)
                    {
                        var tempPath = Path.GetTempPath();
                        var folder = Path.Combine(tempPath, "tmp");
                        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                        var path = Path.Combine(folder, file.FileName);
                        File.WriteAllBytes(path, file.DataArray);
                        file.FilePath = path;
                        media = new FilePickedDto(file);
                        await PickedFileHandler(media);
                    }
                }
                else if (addFileFrom.Equals(GALERIA))
                {
                    try
                    {
                        var file = await _fileManager.PickerPhotoAsync();
                        if (file != null)
                        {
                            media = new FilePickedDto(file);
                            await PickedFileHandler(media);
                        }
                    }
                    catch (Exception ex)
                    {
                        await MessageService.ShowAsync(ex?.InnerException.ToString());
                    }
                }
                else return;
            }
            catch (Exception ex)
            {
                ExceptionService.TrackError(ex, "IncludeAttachmentAsync");
            }
            finally { IsBusy = false; }
        }

        private async Task PickedFileHandler(FilePickedDto media)
        {
            if (media == null) return;

            var isValidFileType = Bootstraper.Resolve<IFileValidation>().IsValidFileType(media);
            if (!isValidFileType)
            {
                await MessageService.ShowAsync($"Tipo de arquivo {Path.GetExtension(media.Name)?.Replace(".", "").ToUpperInvariant()} não aceito.");
                return;
            }

            var maxImageSizeToCompressInBytes = Constants.Constants.MaxImageSizeInMegaBytesToCompress.ConvertMegabytesToBytes();
            var fileExtension = Path.GetExtension(media.Name)?.Replace(".", "").ToUpperInvariant();
            if (Constants.Constants.FilesImageTypeAccepted.Any(a => a == fileExtension) && media.SizeInBytes > maxImageSizeToCompressInBytes)
            {
                media.Compress();
                File.WriteAllBytes(media.Path, media.Bytes);
            }

            if (media.SizeInBytes > maxImageSizeToCompressInBytes)
            {
                await MessageService.ShowAsync(message: $"O arquivo excedeu o limite máximo de {Constants.Constants.MaxImageSizeInMegaBytesToCompress}mb.");
                return;
            }

            NewDocumentAdded(DocumentType, media);
        }

        private async Task NewDocumentAdded(string documentType, FilePickedDto file)
        {
            try
            {
                var buyerDocument = new BuyerDocumentViewModel(documentType, file);
                //TODO Add validation
                Documents.Add(buyerDocument);
            }
            catch (Exception ex)
            {
                ExceptionService.TrackError(ex, $"{nameof(BuyerViewModel)}.NewDocumentAdded");
                await MessageService.ShowAsync("Houve um problema ao anexar o arquivo, tente novamente mais tarde");
            }
        }

        private void RemoveDocument(BuyerDocumentViewModel itemSelected)
        {
            Documents.Remove(itemSelected);
        }
    }
}