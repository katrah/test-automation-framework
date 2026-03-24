using System;
using System.ComponentModel;

namespace Framework.Library
{
    /// <summary>
    /// Used with Enumerations to allow you to define multiple synonyms for the same enum value.
    /// For example: Lime, Emerald, Malachite, and Avocado are all "Green" colors.  Therefore,
    /// you could have a Colors enum with a value of Green that has the SynonymsAttribute that
    /// lists all the other value that would also equate to "Green".  Like this:
    /// [Synonyms("Lime", "Emerald", "Malachite", "Avocado")]
    /// Green
    /// 
    /// The SynonymsAttribute can be accessed (perhaps in an extention method) like this:
    /// public static string GetFirstSynonym(this Enum val)
    /// {
    ///     FieldInfo field = val.GetType().GetField(val.ToString());
    ///     SynonymsAttribute synAttribute = (SynonymsAttribute)Attribute.GetCustomAttribute(field, typeof(SynonymsAttribute));
    ///     if (synAttribute != null && synAttribute.Values.Length > 0)
    ///     {
    ///         return synAttribute.Values[0];
    ///     }
    ///     return val.ToString();
    /// }
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class SynonymsAttribute : DescriptionAttribute
    {
        public string[] Values { get; private set; }

        public SynonymsAttribute(params string[] values)
        {
            Values = values;
        }
    }
}
