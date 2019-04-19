using Newtonsoft.Json;

namespace Phish.Domain
{
    public class Venue
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("venue")]
        public string VenueName { get; set; }

        [JsonProperty("venueid")]
        public int VenueId { get; set; }

    }
}