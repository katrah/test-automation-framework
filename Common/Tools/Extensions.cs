using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using Common.ConfigObjects;
using Common.Data;

namespace Common.Tools
{
    public static class Extensions
    {
        /// <summary>
        /// This method takes convert and changes any of the properties specified in propConverts to the dynamic type so that
        /// a property that has the potential to be more than one data type can be serialized with the desired type but the properties
        /// not contained in propConverts would remain unchanged.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="convert"></param>
        /// <param name="propConverts"></param>
        /// <returns></returns>
        public static dynamic ConvertToDynamic<T>(this T convert, params PropertyConversion[] propConverts) where T : ResponseBase, new()
        {
            dynamic dynamo = new ExpandoObject();
            IDictionary<string, object> what = dynamo;
            PropertyInfo[] properties = convert.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                foreach (PropertyConversion propertyCon in propConverts)
                {
                    if (property.Name.Equals(propertyCon.Name))
                    {
                        what.Add(property.Name, propertyCon.Value);
                    }
                    else
                    {
                        what.Add(property.Name, property.GetValue(convert));
                    }
                }
            }
            return dynamo;
        }
    }
}
