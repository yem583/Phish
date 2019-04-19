using Newtonsoft.Json;

namespace Phish.Domain
{
    public class UpcomingShow
    {
        [JsonProperty("showid")]
        public int? ShowId { get; set; }

        [JsonProperty("showdate")]
        public string ShowDate { get; set; }

        [JsonProperty("artistid")]
        public int? ArtistId { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("venue")]
        public string Venue { get; set; }

        [JsonProperty("venueid")]
        public int? VenueId { get; set; }

  
    }
}