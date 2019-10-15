using Plugin.FilePicker.Abstractions;
using Plugin.Media.Abstractions;
using System.Threading.Tasks;

namespace Imobi.Managers.File.Interfaces
{
    public interface IFileManager
    {
        Task<FileData> PickerFilesAsync();

        Task<MediaFile> TakePhotoAsync();

        Task<MediaFile> PickerPhotoAsync();

        byte[] ResizeImage(byte[] imageData, int quality = 30);

        byte[] ResizeImage(byte[] imageData, float width, float height, int quality = 30);
    }
}