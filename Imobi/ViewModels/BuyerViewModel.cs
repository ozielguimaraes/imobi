﻿using Imobi.Dtos;
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
            Documents = new ObservableCollection<BuyerDocumentViewModel>();
            CanAddNewFile = true;
        }

        public ICommand BuyerDocumentSelectedCommand => new Command<BuyerDocumentViewModel>(async (item) => await BuyerDocumentSelectedAsync(item));

        public ICommand IncludeAttachmentCommand => new Command(async () => await IncludeAttachmentAsync());
        public ICommand OpenBuyerDocumentOptionsCommand => new Command<BuyerDocumentViewModel>(async (item) => await OpenBuyerDocumentOptionsAsync(item));

        private string _documentType;

        public string DocumentType
        {
            get { return _documentType; }
            set { SetProperty(ref _documentType, value); }
        }

        private ObservableCollection<BuyerDocumentViewModel> _documents;

        public ObservableCollection<BuyerDocumentViewModel> Documents
        {
            get { return _documents; }
            set => SetProperty(ref _documents, value);
        }

        private ProposalFormViewModel _form;

        public ProposalFormViewModel Form
        {
            get => _form;
            set => SetProperty(ref _form, value);
        }

        private string[] _fileTypes;

        public string[] FileTypes
        {
            get => _fileTypes;
            set
            {
                _fileTypes = value;
                OnPropertyChanged(nameof(FileTypes));
                FileTypesRemain = value;
            }
        }

        private string[] _fileTypesRemain;

        public string[] FileTypesRemain
        {
            get => _fileTypesRemain;
            set
            {
                _fileTypesRemain = value;
                OnPropertyChanged(nameof(FileTypesRemain));
                CanAddNewFile = value?.Any() ?? false;
            }
        }

        private bool _canAddNewFile;

        public bool CanAddNewFile
        {
            get => _canAddNewFile;
            set => SetProperty(ref _canAddNewFile, value);
        }

        private async Task IncludeAttachmentAsync()
        {
            try
            {
                if (IsBusy) return;
                IsBusy = true;
                if (!FileTypes?.Any() ?? true)
                {
                    var proposalFileTypeService = Bootstraper.Resolve<IProposalFileTypeService>();
                    FileTypes = await proposalFileTypeService.GetFileTypes();
                }
                if (!FileTypes?.Any() ?? true)
                {
                    await MessageService.ShowAsync("Não foi possível obter a lista de arquivos necessários, tente novamente mais tarde.");
                    return;
                }

                DocumentType = await MessageService.ShowOptionsAsync("Qual tipo de documento deseja selecionar?", options: FileTypesRemain);
                if (DocumentType is null || DocumentType.Equals("Cancelar")) return;

                var addFileFrom = await MessageService.ShowOptionsAsync("Agora escolha de onde deseja enviar os arquivos", AttachFileOptions);
                if (addFileFrom is null || addFileFrom.Equals("Cancelar")) return;

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

            await NewDocumentAdded(DocumentType, media);
        }

        private async Task NewDocumentAdded(string documentType, FilePickedDto file)
        {
            try
            {
                var buyerDocument = new BuyerDocumentViewModel(documentType, file);
                Documents.Add(buyerDocument);
                FileTypesRemain = FileTypes?.Where(a => !Documents.Any(d => d.BuyerDocumentType == a)).ToArray();
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
            FileTypesRemain = FileTypes?.Where(a => !Documents.Any(d => d.BuyerDocumentType == a)).ToArray();
        }

        private async Task BuyerDocumentSelectedAsync(BuyerDocumentViewModel buyerDocument)
        {
            //await MessageService.ShowAsync("TESTE OK " + buyerDocument.BuyerDocumentType);
        }

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
    }
}