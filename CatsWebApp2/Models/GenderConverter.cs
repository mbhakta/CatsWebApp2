using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CatsWebApp2.Models
{

    /// <summary>
    /// Customized convertor to change OwnerGender enum values to string
    /// </summary>
    public class GenderConverter : JsonConverter
    {
        /// <summary>
        /// Convert from object to Json string
        /// </summary>
        /// <param name="writer">the handler</param>
        /// <param name="value">the value</param>
        /// <param name="serializer">the serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var gender = (OwnerGender) value;

            switch (gender)
            {
                case OwnerGender.Male:
                    writer.WriteValue("Male");
                    break;
                case OwnerGender.Female:
                    writer.WriteValue("Female");
                    break;
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
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var str = (string) reader.Value;
            return Enum.Parse(typeof (OwnerGender), str, true);
        }

        /// <summary>
        /// filters out the other types from being read or written
        /// </summary>
        /// <param name="objectType">the object type</param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (OwnerGender);
        }
    }
}