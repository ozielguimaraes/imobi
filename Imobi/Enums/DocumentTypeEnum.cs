using Imobi.Attributes;

namespace Imobi.Enums
{
    public enum DocumentTypeEnum
    {
        [EnumValueData(1, "RG")]
        Rg,

        [EnumValueData(2, "CPF")]
        Cpf,

        [EnumValueData(3, "CNH")]
        Cnh
    }
}