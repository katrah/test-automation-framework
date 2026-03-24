using System;
using System.Collections.Generic;
using System.Reflection;

namespace Framework.Library
{
    /// <summary>
    /// Generic IEqualityComparer for any object you want to compare based on a Property of the object.
    /// To use, you need to specify the PropertyName (as a string) that you want to be the entity on
    /// which you base equality.  new ObjectComparer&lt;SomeObject&gt; { PropertyName = "SomePropertyOfSomeObject" }
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectComparer<T> : IEqualityComparer<T>
    {
        public string PropertyName { get; set; }

        public bool Equals(T a, T b)
        {
            if (typeof(T).GetProperty(PropertyName) == null)
            {
                throw new Exception(string.Format("{0} does not contain a {1} Property", typeof(T).Name, PropertyName));
            }

            var aVal = a.GetType().GetProperty(PropertyName).GetValue(a, null);
            var bVal = b.GetType().GetProperty(PropertyName).GetValue(b, null);
            if (aVal == null)
            {
                return (bVal == null);
            }
            return aVal.Equals(bVal);
        }

        public int GetHashCode(T a)
        {
            PropertyInfo info = a.GetType().GetProperty(PropertyName);
            if (info == null)
            {
                return 0;
            }
            return info.GetValue(a, null).GetHashCode();
        }
    }
}
