using System;
using System.Collections.Generic;
using System.Text;

namespace Imobi.Dtos
{
    public class BuyerDto
    {
        public BuyerDto()
        {
            Files = new List<FilePickedDto>();
        }

        public string Name { get; set; }
        public List<FilePickedDto> Files { get; set; }
    }
}