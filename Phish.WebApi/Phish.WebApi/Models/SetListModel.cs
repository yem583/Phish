using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Phish.Domain;

namespace Phish.WebApi.Models
{
    public class SetListModel
    {
        public SetListModel()
        {
            Sets = new List<SetListSetModel>();
            FooterItems = new List<SetListFooterItemModel>();
        }

        public Artist Artist { get; set; }

        public Venue Venue { get; set; }
       
        public string Location { get;set; }

        public string ShowDate { get; set; }

        public string ShortDate { get; set; }

        public string LongDate { get; set; }

        public string SetListNotes { get; set; }

        public string GapChart { get; set; }

        public decimal? Rating { get; set; }

        public string RelativeDate { get; set; }

        public int? ShowId { get; set; }

        public string Url { get; set; }

        public List<SetListSetModel> Sets { get; set; }

        public List<SetListFooterItemModel> FooterItems { get; set; }
        
    }
}
