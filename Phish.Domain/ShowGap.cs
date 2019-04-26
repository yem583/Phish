using System;

namespace Phish.Domain
{
    public class ShowGap
    {
        public DateTime? ShowDate { get; set; }

        public string ShowDateUrl { get; set; }

        public string Venue { get; set; }

        public string VenueUrl { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public decimal? AverageGap { get; set; }

        public string AverageGapUrl { get; set; }
    }
}