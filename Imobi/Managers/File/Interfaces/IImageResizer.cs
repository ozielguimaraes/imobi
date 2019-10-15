namespace Imobi.Managers.File.Interfaces
{
    public interface IImageResizer
    {
        byte[] ResizeImage(byte[] imageData, int quality = 30);

        byte[] ResizeImage(byte[] imageData, float width, float height, int quality = 30);
    }
}