using System;
using Newtonsoft.Json;

namespace Phish.Domain
{
    public class UpcomingShow
    {
       public string Artist { get; set; }

        public DateTime? Date { get; set; }

        public string DateUrl { get; set; }

        public string Venue { get; set; }

        public string VenueUrl { get; set; }

        public string Location { get; set; }
    }
}