using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common.Tools
{
    /// <summary>
    /// This converter is intended to work on nullable classes or objects
    /// -can handle non nullable classes and objects if correct data is received
    /// -can't handle non nullable classes and objects with incorrect or absent data is received
    /// -e.g. non nullable ints and non nullable bools
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NullableConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(T));
        }

        /// <summary>
        /// -Deserializes and finds objects 
        /// -ensures they are in the correct type before returning the appropriate value
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //Finds the objects
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Object)
            {
                return token.ToObject<T>();
            }

            // Ensures the primitive type is nullable before deserializing
            if (CanConvert(reader.ValueType))
            {
                return (T)serializer.Deserialize(reader, typeof(T));
            }

            //JSON reader likes 64-bit ints so make sure to return a 32-bit int if that is the desired int type
            if ((typeof(T) == typeof(int) || typeof(T) == typeof(int?)) && reader.ValueType == typeof(long))
            {
                return Convert.ToInt32(reader.Value);
            }
            return null;
        }

        /// <summary>
        /// serializes the JSON
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
