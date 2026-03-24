using System;
using System.ComponentModel;
using System.Reflection;

namespace Framework.Library.Extensions
{
    public static class Enumerations
    {
        public static bool ContainsFlag<T>(this T flags, T flag) where T : struct
        {
            int flagsValue = (int)(object)flags;
            int flagValue = (int)(object)flag;
            return (flagsValue & flagValue) != 0;
        }

        public static string GetDescription(this Enum val)
        {
            FieldInfo field = val.GetType().GetField(val.ToString());
            DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            if (attribute != null && !string.IsNullOrEmpty(attribute.Description))
            {
                return attribute.Description;
            }

            SynonymsAttribute synAttribute = (SynonymsAttribute)Attribute.GetCustomAttribute(field, typeof(SynonymsAttribute));
            if (synAttribute != null && synAttribute.Values.Length > 0)
            {
                return synAttribute.Values[0];
            }
            return val.ToString();
        }

        public static T SetFlag<T>(this T flags, T flag) where T : struct
        {
            int flagsValue = (int)(object)flags;
            int flagValue = (int)(object)flag;
            return (T)(object)(flagsValue | flagValue);
        }

        public static T UnsetFlag<T>(this T flags, T flag) where T : struct
        {
            int flagsValue = (int)(object)flags;
            int flagValue = (int)(object)flag;
            return (T)(object)(flagsValue & (~flagValue));
        }
    }
}
