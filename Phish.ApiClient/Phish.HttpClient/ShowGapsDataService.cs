using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Caching.Memory;
using Phish.Domain;

namespace Phish.HttpClient
{
    public class ShowGapsDataService : HttpClientDataServiceBase, IShowGapsDataService
    {

        public ShowGapsDataService(System.Net.Http.HttpClient client, IMemoryCache memoryCache)
            : base(client, memoryCache)
        {
        }

        public async Task<IEnumerable<ShowGap>> GetShowGapAsync()
        {
            if (!MemoryCache.TryGetValue("_HttpShowGaps", out IEnumerable<ShowGap> cacheEntry))
            {
                var response = await Client.GetAsync("music/gaps");
                var result = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                var doc = new HtmlDocument();
                doc.LoadHtml(result);

                var list = new List<ShowGap>();
                var topRatedShowRowNodes = doc.DocumentNode.SelectNodes("//tr");
                foreach (var topRatedShowNode in topRatedShowRowNodes.Skip(1).ToList())
                {
                    var topRatedShowCells = topRatedShowNode.ChildNodes.Where(c => c.Name == "td").ToList();
                    var showDateCell = topRatedShowCells[0];
                    var showDateUrl = showDateCell.ChildNodes.Any() &&
                                      showDateCell.ChildNodes[0].Name == "a"
                        ? showDateCell.ChildNodes[0].Attributes["href"].Value
                        : null;
                    var venueCell = topRatedShowCells[1];
                    var cityCell = topRatedShowCells[2];
                    var stateCell = topRatedShowCells[3];
                    var countryCell = topRatedShowCells[4];

                    var averageGapCell = topRatedShowCells[5];
                    var averageGapUrl = averageGapCell.ChildNodes.Any() &&
                                      averageGapCell.ChildNodes[0].Name == "a"
                        ? averageGapCell.ChildNodes[0].Attributes["href"].Value
                        : null;
                    var showGap = new ShowGap()
                    {
                        City = cityCell.InnerText,
                        Venue = venueCell.InnerText,
                        ShowDateUrl = showDateUrl,
                        Country = countryCell.InnerText,
                        State = stateCell.InnerText,
                        AverageGapUrl = averageGapUrl
                    };
                    if (DateTime.TryParse(showDateCell.InnerText, out var showDate))
                    {
                        showGap.ShowDate = showDate;
                    }
                    if (decimal.TryParse(averageGapCell.InnerText, out var averageGap))
                    {
                        showGap.AverageGap = averageGap;
                    }
                    list.Add(showGap);
                }
                cacheEntry = list;
            }

            return cacheEntry;

        }
    }
}