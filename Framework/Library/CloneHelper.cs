using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace Framework.Library
{
    public static class CloneHelper
    {
        /// <summary>
        /// Perform a Shallow Copy of the object, using MemberwiseClone.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T ShallowClone<T>(this T source)
        {
            MethodInfo inst = source.GetType().GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);
            return (T)inst.Invoke(source, null);
        }

        /// <summary>
        /// Perform a Deep Copy of the object, using Json as a serialization method.
        /// </summary>
        /// <typeparam name="T">The type of object being copied. Must be JSON Serializable</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T DeepClone<T>(this T source)
        {
            // Don't serialize a null object, simply return the default for that object
            return ReferenceEquals(source, null) ? Default<T>.Value : JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));
        }

        /// <summary>
        /// Clone a list including contents, using shallow clone
        /// </summary>
        /// <typeparam name="T">The type of object being copied</typeparam>
        /// <param name="listToClone">the list to be cloned</param>
        /// <returns></returns>
        public static IList<T> Clone<T>(this IList<T> listToClone)
        {
            return listToClone.Select(item => item.ShallowClone()).ToList();
        }
    }
}
