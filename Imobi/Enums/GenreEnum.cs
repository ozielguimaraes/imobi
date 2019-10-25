using Imobi.Attributes;

namespace Imobi.Enums
{
    public enum GenreEnum
    {
        [EnumValueData(1, "Masculino")]
        Male,

        [EnumValueData(2, "Feminino")]
        Female,

        [EnumValueData(3, "Outro")]
        Other
    }
}