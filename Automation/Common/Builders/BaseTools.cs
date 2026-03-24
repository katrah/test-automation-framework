using System.Reflection;

namespace Automation.Common.Builders
{
    public static class BaseTools
    {
        /// <summary>
        /// Generic combiner method to take a base object and combine it with an override object.
        /// Basically, for each Property in the override object that isn't null, keep that value for that Property.
        /// If the value for the Property of override object is null, use the value from the base object for the same Property
        /// (as long as the base object value isn't null).
        /// If the optional parameter "overrideBool" is true, this treats any bool Property with a value of false
        /// as effectively being null for the purposes of should it be replaced by the base object value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseObject"></param>
        /// <param name="overrideObject"></param>
        /// <param name="overrideBool"></param>
        /// <returns></returns>
        public static T Combine<T>(T baseObject, T overrideObject, bool overrideBool = false)
        {
            foreach (PropertyInfo property in baseObject.GetType().GetProperties())
            {
                if (overrideBool && property.PropertyType == typeof(bool) && !(bool)property.GetValue(overrideObject, null) && (bool)property.GetValue(baseObject, null))
                {
                    property.SetValue(overrideObject, property.GetValue(baseObject, null));
                }

                if (property.GetValue(baseObject, null) != null && property.GetValue(overrideObject, null) != null)
                {
                    property.SetValue(baseObject, property.GetValue(overrideObject, null));
                }
            }
            return overrideObject;
        }
    }
}
