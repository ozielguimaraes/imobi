using Imobi.Attributes;

namespace Imobi.Enums
{
    public enum MaritalStatusEnum
    {
        [EnumValueData(1, "Solteiro(a)")]
        Single,

        [EnumValueData(2, "Casado(a)")]
        Married,

        [EnumValueData(3, "Separado(a)")]
        Separated,

        [EnumValueData(4, "Divorciado(a)")]
        Divorced,

        [EnumValueData(5, "Viúvo(a)")]
        Widow
    }
}