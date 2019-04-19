using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace Phish.Domain
{
    public class Show
    {
        [JsonProperty("showid")]
        public int? ShowId { get; set; }

        [JsonProperty("showdate")]
        public string ShowDate { get; set; }

        [JsonProperty("artistid")]
        public int? ArtistId { get; set; }

        [JsonProperty("billed_as")]
        public string BilledAs { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("venue")]
        public string Venue { get; set; }

        [JsonProperty("setlistnotes")]
        public string SetListNotes { get; set; }

        [JsonProperty("venueid")]
        public int? VenueId { get; set; }

        [JsonProperty("tourid")]
        public int? TourId { get; set; }

        [JsonProperty("tourname")]
        public string TourName { get; set; }

        [JsonProperty("tour_when")]
        public string TourWhen { get; set; }

        [JsonProperty("artistlink")]
        public string ArtistLink { get; set; }

    }
}