using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imobi.IoC;
using Imobi.Managers.File.Interfaces;
using Imobi.Services.Interfaces;
using Imobi.Validations;
using Imobi.Validations.Interfaces;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace Imobi.Managers.File
{
    public class FileManager : IFileManager
    {
        public async Task<FileData> PickerFilesAsync()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await Application.Current.MainPage.DisplayAlert("Photos não suportado", ":( Você se esqueceu de dar permissão para photos.", "OK");
                    return null;
                }
                var file = await CrossFilePicker.Current.PickFile();

                if (file is null) return null;

                return file;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return null;
            }
        }

        public async Task<MediaFile> PickerPhotoAsync()
        {
            try
            {
                var permissionValidation = Bootstraper.Resolve<IPermissionValidation>();
                var storageAccessAllowed = await permissionValidation.ValidateAsync(Permission.Storage);

                if (storageAccessAllowed)
                {
                    await CrossMedia.Current.Initialize();

                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        await Application.Current.MainPage.DisplayAlert("Fotos não suportadas", ":( Permissão não concedida.", "OK");
                        return null;
                    }

                    var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                    {
                        PhotoSize = PhotoSize.Medium,
                        CompressionQuality = 50
                    });

                    if (file is null) return null;

                    return file;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Acesso negado", "Não foi possível continuar, tente novamente.", "OK");
                    return null;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return null;
            }
        }

        public byte[] ResizeImage(byte[] bytes, int quality = 30)
        {
            return DependencyService.Get<IImageResizer>().ResizeImage(bytes, quality);
        }

        public byte[] ResizeImage(byte[] bytes, float width, float height, int quality = 30)
        {
            return DependencyService.Get<IImageResizer>().ResizeImage(bytes, width, height, quality);
        }

        public async Task<MediaFile> TakePhotoAsync()
        {
            try
            {
                var permissionValidation = Bootstraper.Resolve<IPermissionValidation>();
                var cameraAccessAllowed = await permissionValidation.ValidateCameraAccessAsync();

                if (cameraAccessAllowed)
                {
                    await CrossMedia.Current.Initialize();

                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        await Application.Current.MainPage.DisplayAlert("Desculpe", ":( Nenhuma câmera disponível.", "OK");
                        return null;
                    }

                    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                    });

                    if (file is null) return null;

                    return file;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Acesso negado", "Não foi possível continuar, tente novamente.", "OK");
                    return null;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                Bootstraper.Resolve<IExceptionService>()?.TrackError(ex, nameof(FileManager), nameof(TakePhotoAsync));

                return null;
            }
        }
    }
}