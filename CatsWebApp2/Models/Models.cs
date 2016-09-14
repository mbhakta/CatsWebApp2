using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CatsWebApp2.Models
{
    /// <summary>
    /// The owner gender 
    /// </summary>
     
    [Flags]
    public enum OwnerGender
    {
        Male = 0,   
        Female
    }

    /// <summary>
    /// Pet type 
    /// </summary>
    [Flags]
    public enum PetType
    {
        Dog = 0,
        Cat,
        Fish
    }

    /// <summary>
    /// The owner structure
    /// </summary>
    public class Owner
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Make sure the gender is converted by Json
        /// </summary>
        [JsonProperty("gender")]
        [JsonConverter(typeof(GenderConverter))]
        public OwnerGender Gender { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("pets")]
        public List<Pet> Pets { get; set; } 
    }

    /// <summary>
    /// The pet structure
    /// </summary>
    public class Pet
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Make sure the pet type is converted by Json
        /// </summary>
        [JsonProperty("type")]
        [JsonConverter(typeof(PetTypeConverter))]
        public PetType Type { get; set; }
    }


    /// <summary>
    /// The output object rendered onto the screen (if output requirements change this is the structure to modify)
    /// </summary>
    public class Output
    {
        /// <summary>
        /// Prevent misuse
        /// </summary>
        private Output()
        {
            
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="gender">the gender from caller</param>
        public Output(OwnerGender gender)
        {
            OwnerGender = gender.ToString();
            PetNames = new List<string>();  // init
        }


        /// <summary>
        /// The gender label
        /// </summary>
        public string OwnerGender { get; }

        /// <summary>
        /// The pet names in a list
        /// </summary>
        public List<string> PetNames { get; set; } 
    }
}