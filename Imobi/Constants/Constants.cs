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
                public const string Key = "e0b50fe4-f223-434d-bc48-827415136b70";
            }

            public class iOS
            {
                public const string Key = "ce7adff1-e1ef-45eb-8d29-7742ecd942a7";
            }
        }

        public class Expressions
        {
            public const string NumbersOnly = @"[^\d]";
            public const string PersonName = @"^[A-Za-z]+[\s][A-Za-z]+[.][A-Za-z]+$";
        }
    }
}