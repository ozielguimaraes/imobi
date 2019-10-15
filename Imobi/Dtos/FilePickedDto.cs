using System;
using Imobi.Extensions;
using Imobi.Managers.File.Interfaces;
using Plugin.FilePicker.Abstractions;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace Imobi.Dtos
{
    public class FilePickedDto
    {
        public FilePickedDto(MediaFile file)
        {
            SetInfo(file);
        }

        public FilePickedDto(FileData file)
        {
            SetInfo(file);
        }

        public string Path { get; private set; }
        public string Name { get; private set; }
        public byte[] Bytes { get; private set; }
        public long SizeInBytes { get; private set; }
        public double SizeInMb { get; private set; }

        public void SetInfo(MediaFile file)
        {
            Bytes = file.GetStream().ToByteArray();
            Name = System.IO.Path.GetFileName(file.Path);
            Path = file.Path;
            UpdateSize();
        }

        public void SetInfo(FileData file)
        {
            Bytes = file.GetStream().ToByteArray();
            Name = file.FileName;
            Path = file.FilePath;
            UpdateSize();
        }

        public void UpdateSize()
        {
            SizeInBytes = Bytes.Length;
            SizeInMb = Bytes.ConvertBytesToMegabytes();
        }

        internal void Compress()
        {
            Bytes = DependencyService.Get<IImageResizer>().ResizeImage(Bytes);
            UpdateSize();
        }
    }
}