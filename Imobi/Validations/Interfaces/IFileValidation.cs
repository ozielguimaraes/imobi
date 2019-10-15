using Imobi.Dtos;
using System.Collections.Generic;

namespace Imobi.Validations.Interfaces
{
    public interface IFileValidation
    {
        long MaxSizeInBytes { get; }

        bool IsValidFileType(FilePickedDto pickedFileToAdd, string[] filesTypeAccepted = null);

        bool IsValidSize(IEnumerable<FilePickedDto> pickedFiles, out double sizeAttached, out double remainSpace, double? maxSize = null);

        bool IsValidSize(IEnumerable<FilePickedDto> pickedFiles, FilePickedDto pickedFileToAdd, out double sizeAttached, out double remainSpace, double? maxSize = null);

        double CalculateRemainSpace(IEnumerable<FilePickedDto> pickedFiles, double? maxSize = null);
    }
}