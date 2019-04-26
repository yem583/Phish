using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Caching.Memory;
using Phish.Domain;

namespace Phish.HttpClient
{
    public class TopRatedShowsDataService : HttpClientDataServiceBase, ITopRatedShowsDataService
    {
    
        public TopRatedShowsDataService(System.Net.Http.HttpClient client, IMemoryCache memoryCache)
            :base(client,memoryCache)
        {
        }

        public async Task<IEnumerable<TopRatedShow>> GetTopRatedShowsAsync()
        {
            if (!MemoryCache.TryGetValue("_HttpTopRatedShows", out IEnumerable<TopRatedShow> cacheEntry))
            {
                var response = await Client.GetAsync("music/ratings");
                var result = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                var doc = new HtmlDocument();
                doc.LoadHtml(result);

                var list = new List<TopRatedShow>();
                var topRatedShowRowNodes = doc.DocumentNode.SelectNodes("//tr");
                foreach (var topRatedShowNode in topRatedShowRowNodes.Skip(1).ToList())
                {
                    var topRatedShowCells = topRatedShowNode.ChildNodes.Where(c => c.Name == "td").ToList();
                    var ratingCell = topRatedShowCells[0];
                    var numberOfVotesCell = topRatedShowCells[1];
                    var showDateCell = topRatedShowCells[2];
                    var showDateUrl = showDateCell.ChildNodes.Any() &&
                                      showDateCell.ChildNodes[0].Name == "a"
                        ? showDateCell.ChildNodes[0].Attributes["href"].Value
                        : null;
                    var venueCell = topRatedShowCells[3];
                    var cityCell = topRatedShowCells[4];
                    var stateCell = topRatedShowCells[5];
                    var countryCell = topRatedShowCells[6];

                    var topRatedShow = new TopRatedShow()
                    {
                        City = cityCell.InnerText,
                        Venue = venueCell.InnerText,
                        ShowDateUrl = showDateUrl,
                        Country = countryCell.InnerText,
                        State = stateCell.InnerText
                    };
                    if (DateTime.TryParse(showDateCell.InnerText, out var showDate))
                    {
                        topRatedShow.ShowDate = showDate;
                    }
                    if (decimal.TryParse(ratingCell.InnerText, out var rating))
                    {
                        topRatedShow.Rating = rating;
                    }
                    if (int.TryParse(numberOfVotesCell.InnerText, out var numberOfVotes))
                    {
                        topRatedShow.NumberOfVotes = numberOfVotes;
                    }
                    list.Add(topRatedShow);
                }
                cacheEntry = list;
            }

            return cacheEntry;

        }
    }
}