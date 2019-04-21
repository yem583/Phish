using System.Collections.Generic;

namespace Phish.WebApi.Models
{
    public class SetListSetModel
    {
        public SetListSetModel()
        {
            SetListSongs = new List<SetListSongModel>();
        }

        public string SetLabel { get; set; }

        public List<SetListSongModel> SetListSongs { get; set; }
    }
}