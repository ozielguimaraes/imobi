namespace Imobi.Constants
{
    public class Constants
    {
        public const string AppName = "Imobi";
        public const double MaxUploadSizeInMegaBytes = 20;
        public const double MaxImageSizeInMegaBytesToCompress = 1;
        public static readonly string[] FilesImageTypeAccepted = new string[] { FileType.Jpg, FileType.Jpeg, FileType.Png };
        public static readonly string[] FilesTypeAccepted = new string[] { FileType.Jpg, FileType.Jpeg, FileType.Png, FileType.Pdf };

        public class FileType
        {
            public const string Jpg = "JPG";
            public const string Jpeg = "JPEG";
            public const string Png = "PNG";
            public const string Pdf = "PDF";
        }

        public class AppCenter
        {
            public class Android
            {
                public const string Key = "fill";
            }

            public class iOS
            {
                public const string Key = "fill";
            }
        }
    }
}