using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CatsWebApp2.Models
{
    /// <summary>
    /// Customized convertor to change PetType enum values to string
    /// </summary
    public class PetTypeConverter : JsonConverter
    {
        /// <summary>
        /// Convert from object to Json string
        /// </summary>
        /// <param name="writer">the handler</param>
        /// <param name="value">the value</param>
        /// <param name="serializer">the serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var ptype = (PetType) value;

            switch (ptype)
            {
                case PetType.Dog:
                    writer.WriteValue("Dog");
                    break;
                case PetType.Cat:
                    writer.WriteValue("Cat");
                    break;
                case PetType.Fish:
                    writer.WriteValue("Fish");
                    break;
                default:
                    throw new ApplicationException("Pet type " + ptype + " not implemented");
            }
        }

        /// <summary>
        /// Convert from string to object
        /// </summary>
        /// <param name="reader">the handler</param>
        /// <param name="objectType">the object type</param>
        /// <param name="existingValue">the value</param>
        /// <param name="serializer">the serializer</param>
        /// <returns>the enumed object</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var str = (string) reader.Value;
            return Enum.Parse(typeof (PetType), str, true);
        }

        /// <summary>
        /// filters out the other types from being read or written
        /// </summary>
        /// <param name="objectType">the object type</param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (PetType);
        }
    }
}