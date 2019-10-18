using System;

namespace Imobi.Attributes
{
    //https://forums.xamarin.com/discussion/74074/enum-description-in-pcl
    public class EnumValueDataAttribute : Attribute
    {
        public EnumValueDataAttribute(object key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Value { get; set; }
        public object Key { get; set; }
    }
}