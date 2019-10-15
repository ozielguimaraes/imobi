using Xamarin.Forms;
using Imobi.Droid.Managers.File;
using Android.Graphics;
using System.IO;
using Imobi.Managers.File.Interfaces;

[assembly: Dependency(typeof(ImageResizer))]

namespace Imobi.Droid.Managers.File
{
    public class ImageResizer : IImageResizer
    {
        public byte[] ResizeImage(byte[] imageData, int quality = 75)
        {
            Bitmap originalImage = GetBitmap(imageData);
            return ResizeImage(imageData, originalImage.Width, originalImage.Height, quality);
        }

        public byte[] ResizeImage(byte[] imageData, float width, float height, int quality = 75)
        {
            Bitmap originalImage = GetBitmap(imageData);

            float oldWidth = originalImage.Width;
            float oldHeight = originalImage.Height;
            float scaleFactor = oldWidth > oldHeight ? width / oldWidth : height / oldHeight;

            float newHeight = oldHeight * scaleFactor;
            float newWidth = oldWidth * scaleFactor;

            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)newWidth, (int)newHeight, false);

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, quality, ms);
                return ms.ToArray();
            }
        }

        private Bitmap GetBitmap(byte[] imageData)
        {
            return BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
        }
    }
}