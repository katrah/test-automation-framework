using System;
using System.Collections;

namespace Framework.Library
{
    public class Default<T>
    {
        private static readonly T val;

        static Default()
        {
            if (typeof(T).IsArray)
            {
                if (typeof(T).GetArrayRank() > 1)
                {
                    val = (T)(object)Array.CreateInstance(typeof(T).GetElementType(), new int[typeof(T).GetArrayRank()]);
                }
                else
                {
                    val = (T)(object)Array.CreateInstance(typeof(T).GetElementType(), 0);
                }
                return;
            }

            if (typeof(T) == typeof(string))
            {
                // string is IEnumerable<char>, but don't want to treat it like a collection
                val = default(T);
                return;
            }

            if (typeof(IEnumerable).IsAssignableFrom(typeof(T)))
            {
                // Check if an empty array is an instance of T
                if (typeof(T).IsAssignableFrom(typeof(object[])))
                {
                    val = (T)(object)new object[0];
                    return;
                }

                if (typeof(T).IsGenericType && typeof(T).GetGenericArguments().Length == 1)
                {
                    Type elementType = typeof(T).GetGenericArguments()[0];
                    if (typeof(T).IsAssignableFrom(elementType.MakeArrayType()))
                    {
                        val = (T)(object)Array.CreateInstance(elementType, 0);
                        return;
                    }
                    val = (T)Activator.CreateInstance(typeof(T));
                    return;
                }
                //throw new NotImplementedException(string.Format("No default value is implemented for type {0}", typeof(T).FullName));
            }
            val = default(T);
        }

        public static T Value
        {
            get
            {
                return val;
            }
        }
    }
}
