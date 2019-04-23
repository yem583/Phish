using System;

namespace Phish.Domain
{
    public class Song
    {
        public string SongName{get; set;}

        public string OriginalArtist { get; set; }

        public string Times { get; set; }

        public DateTime? Debut { get; set; }

        public DateTime? Last { get; set; }

        public int? Gap { get; set; }

        public bool IsOriginal { get; set; }

        public bool IsCover { get; set; }

        public bool IsAlias { get; set; }
    }
}