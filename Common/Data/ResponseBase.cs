using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Common.Data
{
    [Serializable]
    public class ResponseBase
    {
        [JsonIgnore]
        public bool Failed { get; set; }

        [JsonIgnore]
        public int HashCode
        {
            get
            {
                return JsonConvert.SerializeObject(this).GetHashCode();
            }
        }

        [JsonIgnore]
        public string Msg { get; set; }

        [JsonIgnore]
        public object PrimitiveObject { get; set; }

        protected string[] defaultProperties { get; set; }

        public bool IsEqual(object obj)
        {
            return (JsonConvert.SerializeObject(this).GetHashCode() == JsonConvert.SerializeObject(obj).GetHashCode());
        }

        /// <summary>
        /// Checks to see if the passed in object is equal to the calling object based on comparing the values of
        /// specified properties passed in.  If no properties are passed in, this will compare the values of all
        /// properties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="compare"></param>
        /// <param name="errorList"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public bool IsEqual<T>(T compare, out List<string> errorList, params string[] properties)
        {
            errorList = new List<string>();
            if (compare == null)
            {
                errorList.Add("The object you are comparing is null");
                return false;
            }

            if (properties.Length == 0)
            {
                // If properties is empty, build an array of the names of all the properties
                properties = GetType().GetProperties().Select(p => p.Name).ToArray();
            }

            foreach (string property in properties)
            {
                PropertyInfo propInfo = GetType().GetProperty(property);
                object thisVal = propInfo.GetValue(this);
                object compareVal = propInfo.GetValue(compare);

                // Need to check to see if the values of the Property we are looking at are null
                // If both are null, that's fine because they are equal
                // If one is null and other isn't, add an error to the list
                // If both aren't null check to see if they are ResponseBase objects or other things
                // We do the null check here because:
                //   1. No need to go any further if they are both null - they are the same
                //   2. We really, really don't want to send null objects to the recursive isEqual function if one
                //      of the objects is null and the other is a ResponseBase (avoiding Null Reference Exceptions)
                if (thisVal == null && compareVal == null)
                {
                    continue;
                }

                // If both weren't null, just see if either one individually is null
                if (thisVal == null || compareVal == null)
                {
                    Failed = true;
                    errorList.Add(string.Format("{0}.{1}: {2} vs. {3}", propInfo.DeclaringType, property, thisVal ?? "null", compareVal ?? "null"));
                    continue;
                }
                
                if (propInfo.PropertyType.IsSubclassOf(typeof(ResponseBase)) || propInfo.PropertyType == typeof(ResponseBase))
                {
                    // This is a complex ResponseBase based object.  We need to recurse through this one as well
                    List<string> tmpErrors;
                    string declaringTypeHistory = string.Empty;
                    if (propInfo.DeclaringType != null)
                    {
                        declaringTypeHistory = propInfo.DeclaringType.ToString();
                    }

                    // Off to the recursive isEqual function that allows me to keep the DeclaringType history so you
                    // can see the full path of nested objects
                    if (!isEqual(thisVal, compareVal, declaringTypeHistory, out tmpErrors))
                    {
                        Failed = true;
                        errorList.AddRange(tmpErrors);
                    }
                }
                // Don't compare HashCode because it isn't something you can set
                else if (!property.Equals("HashCode") && (propInfo.PropertyType.IsPrimitive || propInfo.PropertyType == typeof(string) || propInfo.PropertyType == typeof(object)))
                {
                    if (!Equals(thisVal, compareVal))
                    {
                        Failed = true;
                        errorList.Add(string.Format("{0}.{1}: {2} vs. {3}", propInfo.DeclaringType, property, thisVal, compareVal));
                    }
                }
            }
            return !Failed;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Checks the properties of the object for null values.  Sets Failed = true and returns false if a property is null.
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public bool Validate(params string[] properties)
        {
            List<string> errorList = new List<string>();
            if (properties == null || properties.Length == 0)
            {
                properties = defaultProperties;
            }

            if (properties == null)
            {
                Failed = true;
                Msg = string.Format("{0}:  No properties were provided and there are no default properties to check.", GetType().Name);
                return !Failed;
            }

            foreach (string property in properties)
            {
                if (GetType().GetProperty(property).GetValue(this, null) == null)
                {
                    Failed = true;
                    errorList.Add(string.Format("{0}.{1} is null", GetType().Name, property));
                }
            }

            if (Failed)
            {
                if (Msg != null)
                {
                    errorList.Add(Msg);
                }
                Msg = string.Join(Environment.NewLine, errorList);
            }
            return !Failed;
        }

        /// <summary>
        /// Recursive if the source and compare objects are ResponseBase objects (or inherit ResponseBase)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="compare"></param>
        /// <param name="declaringTypeHistory"></param>
        /// <param name="errorList"></param>
        /// <returns></returns>
        private bool isEqual<T>(T source, T compare, string declaringTypeHistory, out List<string> errorList)
        {
            errorList = new List<string>();
            string[] properties = source.GetType().GetProperties().Select(p => p.Name).ToArray();
            foreach (string property in properties)
            {
                PropertyInfo propInfo = source.GetType().GetProperty(property);
                object thisVal = propInfo.GetValue(source);
                object compareVal = propInfo.GetValue(compare);
                // Need to check to see if the values of the Property we are looking at are null
                // If both are null, that's fine because they are equal
                // If one is null and other isn't, add an error to the list
                // If both aren't null check to see if they are ResponseBase objects or other things
                // We do the null check here because:
                //   1. No need to go any further if they are both null - they are the same
                //   2. We really, really don't want to send null objects to the recursive isEqual function if one
                //      of the objects is null and the other is a ResponseBase (avoiding Null Reference Exceptions)
                if (thisVal == null && compareVal == null)
                {
                    continue;
                }

                // If both weren't null, just see if either one individually is null
                if (thisVal == null || compareVal == null)
                {
                    Failed = true;
                    errorList.Add(string.Format("{0}.{1}: {2} vs. {3}", propInfo.DeclaringType, property, thisVal ?? "null", compareVal ?? "null"));
                    continue;
                }

                if (propInfo.PropertyType.IsSubclassOf(typeof(ResponseBase)) || propInfo.PropertyType == typeof(ResponseBase))
                {
                    // This is a complex ResponseBase based object.  We need to recurse through this one as well
                    List<string> tmpErrors;
                    if (!isEqual(thisVal, compareVal, string.Format("{0}.{1}", declaringTypeHistory, propInfo.DeclaringType), out tmpErrors))
                    {
                        Failed = true;
                        errorList.AddRange(tmpErrors);
                    }
                }
                // Don't compare HashCode because it isn't something you can set
                else if (!property.Equals("HashCode") && (propInfo.PropertyType.IsPrimitive || propInfo.PropertyType == typeof(string) || propInfo.PropertyType == typeof(object)))
                {
                    if (!Equals(thisVal, compareVal))
                    {
                        Failed = true;
                        errorList.Add(string.Format("{0}.{1}.{2}: {3} vs. {4}", declaringTypeHistory, propInfo.DeclaringType, property, thisVal, compareVal));
                    }
                }
            }
            return !Failed;
        }
    }
}
