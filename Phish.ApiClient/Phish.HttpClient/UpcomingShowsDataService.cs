using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Caching.Memory;
using Phish.Domain;

namespace Phish.HttpClient
{
    public class UpcomingShowsDataService : IUpcomingShowsDataService
    {
        protected readonly System.Net.Http.HttpClient Client;
        protected readonly IMemoryCache MemoryCache;

        public UpcomingShowsDataService(System.Net.Http.HttpClient client, IMemoryCache memoryCache)
        {
            Client = client;
            MemoryCache = memoryCache;
            client.BaseAddress = new Uri("https://www.phish.net/");
        }

        public async Task<IEnumerable<UpcomingShow>> GetUpcomingShowsAsync()
        {
            if (!MemoryCache.TryGetValue("_HttpUpcomingShows", out IEnumerable<UpcomingShow> cacheEntry))
            {
                var response = await Client.GetAsync("upcoming");
                var result = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                var doc = new HtmlDocument();
                doc.LoadHtml(result);

                var list = new List<UpcomingShow>();
                var showRowNodes = doc.DocumentNode.SelectNodes("//tr");
                foreach (var songRowNode in showRowNodes.Skip(1).ToList())
                {
                    var showCells = songRowNode.ChildNodes.Where(c => c.Name == "td").ToList();
                    var artistCell = showCells[0];
                    var dateCell = showCells[1];
                    var dateCellUrl = dateCell.ChildNodes.Any() &&
                                      dateCell.ChildNodes[0].Name == "a"
                        ? dateCell.ChildNodes[0].Attributes["href"].Value
                        : null;
                    var venueCell = showCells[2];
                    var venueCellUrl = venueCell.ChildNodes.Any() &&
                                   venueCell.ChildNodes[0].Name == "a"
                        ? venueCell.ChildNodes[0].Attributes["href"].Value
                        : null;
                    var locationCell = showCells[3];

                    var upcomingShow = new UpcomingShow()
                    {
                        Artist = artistCell?.InnerText,
                        DateUrl = dateCellUrl,
                        Location = locationCell.InnerText,
                        Venue = venueCell.InnerText,
                        VenueUrl = venueCellUrl
                    };
                    if (DateTime.TryParse(dateCell.InnerText, out var upcomingShowDate))
                    {
                        upcomingShow.Date = upcomingShowDate;
                    }
                    list.Add(upcomingShow);
                }
                cacheEntry = list;
            }

            return cacheEntry;

        }
    }
}