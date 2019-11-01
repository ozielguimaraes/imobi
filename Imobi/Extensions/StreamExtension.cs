﻿using System.IO;

namespace Imobi.Extensions
{
    public static class StreamExtension
    {
        public static byte[] ToByteArray(this Stream stream)
        {
            byte[] buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}