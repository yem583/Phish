using Newtonsoft.Json;

namespace Phish.Domain
{
    public class SetList
    {
        [JsonProperty("artist")]
        public string Artist { get; set; }

        [JsonProperty("artistid")]
        public int? ArtistId { get; set; }

        [JsonProperty("gapchart")]
        public string GapChart { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("long_date")]
        public string LongDate { get; set; }

        [JsonProperty("rating")]
        public string Rating { get; set; }

        [JsonProperty("relative_date")]
        public string RelativeDate { get; set; }

        [JsonProperty("setlistdata")]
        public string SetListData { get; set; }

        [JsonProperty("setlistnotes")]
        public string SetListNotes { get; set; }

        [JsonProperty("short_date")]
        public string ShortDate { get; set; }

        [JsonProperty("showdate")]
        public string ShowDate { get; set; }

        [JsonProperty("showid")]
        public int? ShowId { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("venue")]
        public string Venue { get; set; }

        [JsonProperty("venueid")]
        public int? VenuId { get; set; } 
      
    }
}