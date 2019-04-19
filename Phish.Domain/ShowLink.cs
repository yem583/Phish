using Newtonsoft.Json;

namespace Phish.Domain
{
    public class ShowLink
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}