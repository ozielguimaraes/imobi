using Imobi.Attributes;

namespace Imobi.Enums
{
    public enum ScholarityEnum
    {
        [EnumValueData(1, "Fundamental incompleto")]
        ElementaryIncomplete,

        [EnumValueData(2, "Fundamental completo")]
        ElementaryComplete,

        [EnumValueData(3, "Superior incompleto")]
        GraduationStudent,

        [EnumValueData(4, "Superior completo")]
        Graduated,

        [EnumValueData(5, "Pós graduação")]
        Postgraduate
    }
}