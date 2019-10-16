using Imobi.Dtos;
using Imobi.Enums;
using System.Collections.Generic;

namespace Imobi.Models
{
    public class FilesAttachedGroup
    {
        public FilesAttachedGroup(DocumentGroupTypeEnum? type = DocumentGroupTypeEnum.FourColumns)
        {
            FilesAttached = new List<FilePickedDto>();
            Type = type ?? DocumentGroupTypeEnum.FourColumns;
        }

        public DocumentGroupTypeEnum Type { get; private set; }
        public IList<FilePickedDto> FilesAttached { get; set; }
    }
}