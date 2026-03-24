using System;

namespace Framework.Library
{
    public static class MetaData
    {
        public static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        public static bool IsNullableType(Type type)
        {
            if (!type.IsValueType)
            {
                return true;
            }

            if (Nullable.GetUnderlyingType(type) != null)
            {
                return true;
            }
            return false;
        }
    }
}
