using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Caching.Memory;
using Phish.Domain;

namespace Phish.HttpClient
{
    public class VenuesDataService : IVenuesDataService
    {
        protected readonly System.Net.Http.HttpClient Client;
        protected readonly IMemoryCache MemoryCache;
        
        public VenuesDataService(System.Net.Http.HttpClient client,IMemoryCache memoryCache)
        {
            Client = client;
            MemoryCache = memoryCache;
            client.BaseAddress = new Uri("https://www.phish.net/");
        }

        public async Task<IEnumerable<Venue>> GetVenuesAsync()
        {
            if (!MemoryCache.TryGetValue("_HttpVenues", out IEnumerable<Venue> cacheEntry))
            {
                var response = await Client.GetAsync("venues");
                var result = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                var doc = new HtmlDocument();
                doc.LoadHtml(result);

                var list = new List<Venue>();
                var venueRowNodes = doc.DocumentNode.SelectNodes("//tr");
                foreach (var venueRowNode in venueRowNodes.Skip(1).ToList())
                {
                    var venueCells = venueRowNode.ChildNodes.Where(c => c.Name == "td").ToList();
                    var nameCell = venueCells[0];
                    var venueName = nameCell.InnerText;
                    var venueUrl = nameCell.ChildNodes[0].Attributes["href"].Value.Split('/');
                    var venueId = int.Parse(venueUrl[2]);
                    list.Add(new Venue()
                    {
                        VenueName = venueName,
                        VenueId = venueId,
                        City = venueCells[1].InnerText,
                        State = venueCells[2].InnerText,
                        Country = venueCells[3].InnerText,
                        TimesPlayed = int.Parse(venueCells[4].InnerText),
                        FirstTime = DateTime.Parse(venueCells[5].InnerText),
                        LastTime = DateTime.Parse(venueCells[6].InnerText),
                        Link = Client.BaseAddress.ToString() +
                               nameCell.ChildNodes[0].Attributes["href"].Value.Substring(1)
                    });
                }

                cacheEntry = list;
            }

            return cacheEntry;
        }
    }
}
