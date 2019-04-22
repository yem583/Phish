using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Caching.Memory;
using Phish.Domain;

namespace Phish.HttpClient
{
    public class SideShowDataService : ISideShowDataService
    {
        protected readonly System.Net.Http.HttpClient Client;
        protected readonly IMemoryCache MemoryCache;

        public SideShowDataService(System.Net.Http.HttpClient client, IMemoryCache memoryCache)
        {
            Client = client;
            MemoryCache = memoryCache;
            client.BaseAddress = new Uri("https://www.phish.net/");
        }

        public async Task<IEnumerable<Show>> GetSideShowsAsync()
        {
            if (!MemoryCache.TryGetValue("_HttpSideShows", out IEnumerable<Show> cacheEntry))
            {
                var list = new List<Show>();
                var artistList = new Dictionary<string, int>()
                {
                    {"trey", 2},
                    {"fish", 7},
                    {"mike", 6},
                    {"page", 9},
                    {"phish", 1}
                };
                for (var i = 1982; i <= DateTime.Now.Year; i++)
                {
                    foreach (var artist in artistList)
                    {
                        var response = await Client.GetAsync($"setlists/{artist.Key}?year={i}");
                        var result = await response.Content.ReadAsStringAsync();
                        response.EnsureSuccessStatusCode();
                        var doc = new HtmlDocument();
                        doc.LoadHtml(result);

                        var setListContainerNodes =
                            doc.DocumentNode.SelectNodes("//*[@class='setlist-container sideshow sideshow-2']");
                        if (setListContainerNodes != null)
                        {
                            foreach (var setListContainerNode in setListContainerNodes)
                            {
                                var headerNodeAnchorTags = setListContainerNode.Descendants("a");
                                var billedAsAnchorTag = headerNodeAnchorTags.FirstOrDefault();
                                var showDateAnchorTag = headerNodeAnchorTags.Skip(1).FirstOrDefault();
                                var venueAnchorTag = headerNodeAnchorTags.Skip(2).FirstOrDefault();
                                var locationAnchorTag = headerNodeAnchorTags.Skip(3).FirstOrDefault();
                                var locationSegments = locationAnchorTag.Attributes["href"].Value.Split('/');
                                var venueUrl = venueAnchorTag.Attributes["href"].Value.Split('/');
                                var venueId = int.Parse(venueUrl[2]);
                                var show = new Show
                                {
                                    BilledAs = billedAsAnchorTag.InnerText,
                                    ShowDate = showDateAnchorTag.InnerText,
                                    VenueId = venueId,
                                    Venue = venueAnchorTag.InnerText,
                                    ArtistId = artist.Value,
                                    ArtistLink = Client.BaseAddress.ToString() +
                                                 billedAsAnchorTag.Attributes["href"].Value.Substring(1),
                                    Link = Client.BaseAddress.ToString() +
                                           showDateAnchorTag.Attributes["href"].Value.Substring(1),
                                    Location = locationSegments.Length > 5
                                        ? locationAnchorTag.InnerText + ", " + locationSegments[4] + " " +
                                          locationSegments[5]
                                        : locationAnchorTag.InnerText
                                };
                                list.Add(show);
                            }
                        }

                    }

                }

                cacheEntry= list;
            }

            return cacheEntry;
        }
    }
}