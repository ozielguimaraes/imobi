using Imobi.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Imobi.Extensions
{
    //https://forums.xamarin.com/discussion/74074/enum-description-in-pcl
    public static class EnumExtension
    {
        public static List<EnumValueDataAttribute> ConvertToList<TEnum>()
            where TEnum : struct // can't constrain to enums so closest thing
        {
            return Enum.GetValues(typeof(TEnum)).Cast<Enum>()
                .Select(val => GetAttribute(val))
                .OrderBy(o => o.Value).ToList();
        }

        public static EnumValueDataAttribute GetAttribute(Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetRuntimeField(name)
                .GetCustomAttributes(false)
                .OfType<EnumValueDataAttribute>()
                .FirstOrDefault();
        }

        public static EnumValueDataAttribute GetByType<TEnum>(string value) where TEnum : struct // can't constrain to enums so closest thing
        {
            return Enum.GetValues(typeof(TEnum)).Cast<Enum>()
                .Select(val => GetAttribute(val))
                .ToList().FirstOrDefault(v => v.Key.Equals(value));
        }
    }
}