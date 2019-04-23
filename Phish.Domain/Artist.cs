using Newtonsoft.Json;

namespace Phish.Domain
{
    public class Artist
    {
        [JsonProperty("artistid")]
        public int ArtistId { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        
    }
}
