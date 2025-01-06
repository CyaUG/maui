using Newtonsoft.Json;

namespace Youth.Models
{
    public class Country
    {
        private static Country[] _all;

        public static Country[] All
        {
            get
            {
                if (_all == null)
                {
                    using StreamReader reader = new(typeof(Country).Assembly.GetManifestResourceStream("Youth.Resources.countries.json"));
                    string json = reader.ReadToEnd();
                    _all = JsonConvert.DeserializeObject<Country[]>(json);
                }

                return _all;
            }
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nameCode")]
        public string NameCode { get; set; }

        [JsonProperty("callingCode")]
        public string CallingCode { get; set; }

        [JsonProperty("flagUrl")]
        public string FlagUrl { get; set; }
    }
}
