namespace Imobi.Extensions
{
    public static class BytesExtension
    {
        public static double ConvertBytesToMegabytes(this byte[] bytes)
        {
            return ConvertBytesToMegabytes(bytes.Length);
        }

        public static double ConvertBytesToMegabytes(this long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        public static double ConvertBytesToMegabytes(this double bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        public static double ConvertMegabytesToBytes(this double bytes)
        {
            return (bytes * 1024f) * 1024f;
        }
    }
}