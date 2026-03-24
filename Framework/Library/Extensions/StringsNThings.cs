using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using HtmlAgilityPack;

namespace Framework.Library.Extensions
{
    public static class StringsNThings
    {
        /// <summary>
        /// If the val string is not null, appends the val to the StringBuilder
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static StringBuilder AppendIfNotNull(this StringBuilder sb, string val)
        {
            if (val != null)
            {
                sb.Append(val);
            }
            return sb;
        }

        /// <summary>
        /// If the val string is not null, appends a string.Format(template, val) to the StringBuilder
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="template"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static StringBuilder AppendIfNotNull(this StringBuilder sb, string template, string val)
        {
            if (val != null)
            {
                sb.Append(string.Format(template, val));
            }
            return sb;
        }

        /// <summary>
        /// If the val string is not null, appends the val to the StringBuilder
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static StringBuilder AppendLineIfNotNull(this StringBuilder sb, string val)
        {
            if (val != null)
            {
                sb.AppendLine(val);
            }
            return sb;
        }

        /// <summary>
        /// If the val string is not null, appends a string.Format(template, val) to the StringBuilder
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="template"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static StringBuilder AppendLineIfNotNull(this StringBuilder sb, string template, string val)
        {
            if (val != null)
            {
                sb.AppendLine(string.Format(template, val));
            }
            return sb;
        }

        public static bool Between(this DateTime dateTime, DateTime lower, DateTime upper)
        {
            return lower <= dateTime && dateTime <= upper;
        }

        public static bool Between(this TimeSpan timeSpan, TimeSpan lower, TimeSpan upper)
        {
            return lower <= timeSpan && timeSpan <= upper;
        }

        public static bool Contains(this string source, string searchString, StringComparison strComp)
        {
            return (source.IndexOf(searchString, strComp) >= 0);
        }

        /// <summary>
        /// Converts an object to a Boolean, defaults to false unless default value is sent.  If the throwError
        /// parameter is true, this will not return a default value, instead, it throw an error in the event the
        /// object cannot be converted.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="throwError"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public static bool ConvertToBool(this object obj, bool throwError = false, bool defaultVal = false)
        {
            bool result = defaultVal;
            try
            {
                if (obj != null && !DBNull.Value.Equals(obj))
                {
                    result = Convert.ToBoolean(obj);
                }
            }
            catch (Exception)
            {
                if (throwError)
                {
                    throw;
                }
            }
            return result;
        }

        /// <summary>
        /// Override specifically used to convert boolean like strings 
        /// EX: check/uncheck
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="throwError"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public static bool ConvertToBool(this string obj, bool throwError = false, bool defaultVal = false)
        {
            bool result = defaultVal;
            if (obj == null)
            {
                return result;
            }

            try
            {
                switch (obj.ToLower())
                {
                    case "true":
                    case "check":
                        return true;
                    default:
                        return false;
                }
            }
            catch (Exception)
            {
                if (throwError)
                {
                    throw;
                }
            }
            return result;
        }

        /// <summary>
        /// Converts an object to a decimal, defaults to 0 unless default value is sent.  If the throwError
        /// parameter is true, this will not return a default value, instead, it throw an error in the event the
        /// object cannot be converted.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="throwError"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public static decimal ConvertToDecimal(this object obj, bool throwError = false, decimal defaultVal = 0)
        {
            decimal result = defaultVal;
            try
            {
                if (obj != null && !DBNull.Value.Equals(obj))
                {
                    result = Convert.ToDecimal(obj);
                }
            }
            catch (Exception)
            {
                if (throwError)
                {
                    throw;
                }
            }
            return result;
        }

        /// <summary>
        /// Converts an object to a double, defaults to 0.0 unless default value is sent.  If the throwError
        /// parameter is true, this will not return a default value, instead, it throw an error in the event the
        /// object cannot be converted.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="throwError"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public static double ConvertToDouble(this object obj, bool throwError = false, double defaultVal = 0.0)
        {
            double result = defaultVal;
            try
            {
                if (obj != null && !DBNull.Value.Equals(obj))
                {
                    result = Convert.ToDouble(obj);
                }
            }
            catch (Exception)
            {
                if (throwError)
                {
                    throw;
                }
            }
            return result;
        }

        /// <summary>
        /// Converts an object to an Single (float), defaults to 0 unless default value is sent.  If the throwError
        /// parameter is true, this will not return a default value, instead, it throw an error in the event the
        /// object cannot be converted.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="throwError"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public static float ConvertToFloat(this object obj, bool throwError = false, float defaultVal = 0F)
        {
            float result = defaultVal;
            try
            {
                if (obj != null && !DBNull.Value.Equals(obj))
                {
                    result = Convert.ToSingle(obj);
                }
            }
            catch (Exception)
            {
                if (throwError)
                {
                    throw;
                }
            }
            return result;
        }

        /// <summary>
        /// Converts an object to an Int32, defaults to 0 unless default value is sent.  If the throwError
        /// parameter is true, this will not return a default value, instead, it throw an error in the event the
        /// object cannot be converted.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="throwError"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public static int ConvertToInt(this object obj, bool throwError = false, int defaultVal = 0)
        {
            int result = defaultVal;
            try
            {
                if (obj != null && !DBNull.Value.Equals(obj))
                {
                    result = Convert.ToInt32(obj);
                }
            }
            catch (Exception)
            {
                if (throwError)
                {
                    throw;
                }
            }
            return result;
        }

        /// <summary>
        /// Converts an object to an Int64, defaults to 0 unless default value is sent.  If the throwError
        /// parameter is true, this will not return a default value, instead, it throw an error in the event the
        /// object cannot be converted.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="throwError"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public static long ConvertToLong(this object obj, bool throwError = false, long defaultVal = 0)
        {
            long result = defaultVal;
            try
            {
                if (obj != null && !DBNull.Value.Equals(obj))
                {
                    result = Convert.ToInt64(obj);
                }
            }
            catch (Exception)
            {
                if (throwError)
                {
                    throw;
                }
            }
            return result;
        }

        /// <summary>
        /// Converts an object to a Boolean, defaults to null.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool? ConvertToNullBool(this object obj)
        {
            bool? result = null;
            try
            {
                result = Convert.ToBoolean(obj);
            }
            catch (Exception)
            {
            }
            return result;
        }

        /// <summary>
        /// If classVal is null, this will look for a node with no class attribute
        /// </summary>
        /// <param name="node"></param>
        /// <param name="tag"></param>
        /// <param name="classVal"></param>
        /// <returns></returns>
        public static HtmlNode FirstDescendantByClass(this HtmlNode node, string tag, string classVal)
        {
            try
            {
                if (classVal == null)
                {
                    return node.Descendants(tag).First(n => !n.Attributes.Contains("class"));
                }
                return node.Descendants(tag).First(n => n.Attributes.Contains("class") && n.Attributes["class"].Value.Contains(classVal));
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        /// <summary>
        /// Removes leading and trailing \r, \n, and space characters
        /// </summary>
        /// <returns></returns>
        public static string HtmlTrim(this string orig)
        {
            return orig.TrimStart(new char[] { '\r', '\n', ' ' }).TrimEnd(new char[] { '\r', '\n', ' ' });
        }

        public static bool IsNullable<T>(this T obj)
        {
            if (obj == null)
            {
                return true;
            }

            Type type = typeof(T);
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

        public static bool IsNullableType(this Type type)
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

        public static string ParagraphTrim(this string existingString)
        {
            string[] parts = existingString.Split(new string[] { Environment.NewLine, "\r", "\n" }, StringSplitOptions.None);
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = parts[i].Trim();
            }
            return string.Join(Environment.NewLine, parts);
        }

        public static string NullSafe(this string val)
        {
            if (val == null)
            {
                return string.Empty;
            }
            return val;
        }

        public static string Replace(this string existingString, string oldVal, string newVal, int occurances)
        {
            for (int i = 0; i < occurances; i++)
            {
                int index = existingString.IndexOf(oldVal);
                if (index < 0)
                {
                    break;
                }
                existingString = existingString.Remove(index, oldVal.Length).Insert(index, newVal);
            }
            return existingString;
        }

        public static string SafeFileName(this string val, string replaceVal = "-")
        {
            return val.Replace(@"\", replaceVal).Replace("/", replaceVal).Replace(":", replaceVal).Replace("*", replaceVal).Replace("?", replaceVal).Replace("<", replaceVal).Replace(">", replaceVal).Replace("|", replaceVal).Replace("\"", replaceVal);
        }

        /// <summary>
        /// Replaces each occurance of one or more contiguous space characters with a number of
        /// space characters equal to the 'spaces' parameter specified.
        /// <para>In this example, spaces are shown as underscores:</para>
        /// <para>Input: "__some_string__with___variable______spaces___"</para>
        /// <para>Call with spaces = 1</para>
        /// <para>Output: "_some_string_with_variable_spaces_"</para>
        /// </summary>
        /// <param name="existingString"></param>
        /// <param name="spaces"></param>
        /// <returns></returns>
        public static string SetSpaces(this string existingString, int spaces)
        {
            spaces = Math.Max(0, spaces);
            string newSpacing = "".PadRight(spaces);
            bool startwithSpace = existingString.StartsWith(" ");
            bool endswithSpace = existingString.EndsWith(" ");
            string[] parts = existingString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string retVal = string.Join(newSpacing, parts);
            if (startwithSpace)
            {
                retVal = string.Format("{0}{1}", newSpacing, retVal);
            }

            if (endswithSpace)
            {
                retVal = string.Format("{0}{1}", retVal, newSpacing);
            }
            return retVal;
        }

        public static int ToAsciiSum(this string val)
        {
            int sum = 0;
            try
            {
                foreach (char c in val)
                {
                    sum += Convert.ToInt32(c);
                }
            }
            catch (Exception)
            {
                return -1;
            }
            return sum;
        }

        public static T ToEnum<T>(this string value, bool throwError = false) where T : struct
        {
            Type type = typeof(T);
            if (type.IsEnum)
            {
                if (string.IsNullOrEmpty(value))
                {
                    if (throwError)
                    {
                        throw new Exception(string.Format("'{0}' is not a valid enum value for {1}", value ?? "null", type));
                    }
                    return default(T);
                }

                T retVal;
                if (Enum.TryParse(value, true, out retVal))
                {
                    return retVal;
                }

                foreach (FieldInfo field in type.GetFields())
                {
                    SynonymsAttribute synAttribute = (SynonymsAttribute)Attribute.GetCustomAttribute(field, typeof(SynonymsAttribute));
                    if (synAttribute != null)
                    {
                        foreach (string synonym in synAttribute.Values)
                        {
                            if (synonym.Equals(value, StringComparison.OrdinalIgnoreCase))
                            {
                                return (T)field.GetValue(null);
                            }
                        }
                    }

                    DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                    if (attribute != null && attribute.Description.Equals(value, StringComparison.OrdinalIgnoreCase))
                    {
                        return (T)field.GetValue(null);
                    }
                }

                if (throwError)
                {
                    throw new Exception(string.Format("'{0}' is not a valid enum value for {1}", value, type));
                }
                return default(T);
            }
            throw new ArgumentException("T must be an enumerated type");
        }

        public static string ToNullableString(this object obj)
        {
            if (obj == null || DBNull.Value.Equals(obj))
            {
                return null;
            }
            return obj.ToString();
        }
    }
}
