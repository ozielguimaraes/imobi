using Imobi.Dtos;
using Imobi.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Imobi.Validations.Interfaces
{
    public class FileValidation : IFileValidation
    {
        public long MaxSizeInBytes => Convert.ToInt64(Math.Round(Constants.Constants.MaxUploadSizeInMegaBytes.ConvertMegabytesToBytes()));

        public bool IsValidSize(IEnumerable<FilePickedDto> pickedFiles, out double sizeAttached, out double remainSpace, double? maxSize)
        {
            maxSize = maxSize ?? MaxSizeInBytes;
            sizeAttached = pickedFiles.Sum(s => s.SizeInBytes);
            remainSpace = maxSize.Value - sizeAttached;

            return !(sizeAttached > maxSize.Value);
        }

        public bool IsValidFileType(FilePickedDto pickedFileToAdd, string[] filesTypeAccepted = null)
        {
            filesTypeAccepted = filesTypeAccepted ?? Constants.Constants.FilesTypeAccepted;

            var extension = Path.GetExtension(pickedFileToAdd.Name)?.Replace(".", "").ToUpperInvariant();
            return filesTypeAccepted.Any(a => a == extension);
        }

        public bool IsValidSize(IEnumerable<FilePickedDto> pickedFiles, FilePickedDto pickedFileToAdd, out double sizeAttached, out double remainSpace, double? maxSize)
        {
            maxSize = maxSize ?? MaxSizeInBytes;
            sizeAttached = pickedFiles.Sum(s => s.SizeInBytes) + pickedFileToAdd.SizeInBytes;
            remainSpace = maxSize.Value - sizeAttached;

            return !(sizeAttached > maxSize.Value);
        }

        public double CalculateRemainSpace(IEnumerable<FilePickedDto> pickedFiles, double? maxSize)
        {
            maxSize = maxSize ?? MaxSizeInBytes;
            var sizeAttached = pickedFiles.Sum(s => s.SizeInBytes);
            return maxSize.Value - sizeAttached;
        }
    }
}