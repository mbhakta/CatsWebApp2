using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using CatsWebApp2.Models;
using Newtonsoft.Json;

namespace CatsWebApp2.Utilities
{
    /// <summary>
    /// This class is used for extracting data from the web server and 
    /// deserializing the Json buffer into a C# readable format (see Models folder)
    /// </summary>
    internal sealed class DataExtractor
    {
        /// <summary>
        /// Extract the Json data from the Web service 
        /// </summary>
        /// <param name="wsa">the web service address</param>
        /// <returns></returns>
        public static async Task<string> ExtractData(string wsa)
        {
            string result;
            using (var client = new HttpClient())
            {
                // Extract the data into a string
                using (var resp = await client.GetAsync(wsa).ConfigureAwait(false))
                {
                    using (var content = resp.Content)
                    {
                        result = await content.ReadAsStringAsync();
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Convert the Json string to the required format for processing
        /// </summary>
        /// <param name="json">the json string</param>
        /// <returns>the data</returns>
        public static IEnumerable<Owner> DeserializeObject(string json)
        {
            if (json == string.Empty)
            {
                throw new ApplicationException("Invalid or empty string");
            }

            var serializer = new JsonSerializer();
            // NOTE - Do not need to exclusively invoke the converters
            // here as they are handled by the
            // deserializer. As long as the attributes in model are set
            // all should be good
            // serializer.Converters.Add(new GenderConverter());
            //serializer.Converters.Add(new PetTypeConverter());
            using (var rdr = new StringReader(json))
            {
                return serializer.Deserialize(rdr, typeof(List<Owner>)) as IEnumerable<Owner>;
            }
        }

        /// <summary>
        /// Method is used to collect information for UI rendering
        /// Modify this method if requirements change
        /// </summary>
        /// <param name="source">the source data</param>
        /// <param name="gender">the owners gender</param>
        /// <param name="petType">the pet type to filter for</param>
        /// <returns></returns>
        public static Output GetPetsByOwnerGender(IEnumerable<Owner> source,
                    OwnerGender gender, PetType petType = PetType.Cat)
        {
            if (source == null)
            {
                throw new ApplicationException("Invalid or null source");
            }

            // init the output buffer
            var output = new Output(gender);

            // Filter by pet type and collect data (filter out owners without pets)
            foreach (var pet in source.Where(item => item.Gender == gender && item.Pets != null)
                                   .SelectMany(item => item.Pets).Where(a => a.Type == petType))
            {
                output.PetNames.Add(pet.Name);
            }

            // sort by pet name before sending out data
            output.PetNames.Sort((a, b) => string.Compare(a, b, StringComparison.Ordinal));

            return output;
        }
    }
}