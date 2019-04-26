using System;

namespace Phish.Domain
{
    public class TopRatedShow
    {
        public decimal? Rating { get; set; }

        public int? NumberOfVotes { get; set; }

        public DateTime? ShowDate { get; set; }

        public string ShowDateUrl { get; set; }

        public string Venue { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }
    }
}