using System;
using System.Drawing;
using CoreGraphics;
using Imobi.iOS.Managers.File;
using Imobi.Managers.File.Interfaces;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ImageResizer))]

namespace Imobi.iOS.Managers.File
{
    public class ImageResizer : IImageResizer
    {
        public byte[] ResizeImage(byte[] imageData, int quality = 75)
        {
            UIImage originalImage = ImageFromByteArray(imageData);

            return ResizeImage(imageData, (float)originalImage.Size.Width, (float)originalImage.Size.Height, quality);
        }

        public byte[] ResizeImage(byte[] imageData, float width, float height, int quality = 75)
        {
            UIImage originalImage = ImageFromByteArray(imageData);

            float oldWidth = (float)originalImage.Size.Width;
            float oldHeight = (float)originalImage.Size.Height;
            float scaleFactor = oldWidth > oldHeight ? width / oldWidth : height / oldHeight;

            float newHeight = oldHeight * scaleFactor;
            float newWidth = oldWidth * scaleFactor;

            //create a 24bit RGB image
            using (CGBitmapContext context = new CGBitmapContext(IntPtr.Zero,
                (int)newWidth, (int)newHeight, 8,
                (int)(4 * newWidth), CGColorSpace.CreateDeviceRGB(),
                CGImageAlphaInfo.PremultipliedFirst))
            {
                RectangleF imageRect = new RectangleF(0, 0, newWidth, newHeight);

                // draw the image
                context.DrawImage(imageRect, originalImage.CGImage);

                UIKit.UIImage resizedImage = UIKit.UIImage.FromImage(context.ToImage());

                // save the image as a jpeg
                return resizedImage.AsJPEG((float)quality).ToArray();
            }
        }

        public static UIKit.UIImage ImageFromByteArray(byte[] data)
        {
            if (data == null) return null;

            UIKit.UIImage image;
            try
            {
                image = new UIKit.UIImage(Foundation.NSData.FromArray(data));
            }
            catch (Exception e)
            {
                Console.WriteLine("Image load failed: " + e.Message);
                return null;
            }
            return image;
        }
    }
}