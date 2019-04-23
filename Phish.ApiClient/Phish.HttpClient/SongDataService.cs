using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Caching.Memory;
using Phish.Domain;

namespace Phish.HttpClient
{
    public class SongDataService : ISongDataService
    {
        protected readonly System.Net.Http.HttpClient Client;
        protected readonly IMemoryCache MemoryCache;

        public SongDataService(System.Net.Http.HttpClient client, IMemoryCache memoryCache)
        {
            Client = client;
            MemoryCache = memoryCache;
            client.BaseAddress = new Uri("https://www.phish.net/");
        }

        public async Task<IEnumerable<Song>> GetSongsAsync()
        {
            if (!MemoryCache.TryGetValue("_HttpSongs", out IEnumerable<Song> cacheEntry))
            {
                var response = await Client.GetAsync("song");
                var result = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                var doc = new HtmlDocument();
                doc.LoadHtml(result);

                var list = new List<Song>();
                var venueRowNodes = doc.DocumentNode.SelectNodes("//tr");
                foreach (var songRowNode in venueRowNodes.Skip(1).ToList())
                {
                    var classes = songRowNode.Attributes["class"].Value.Split(' ');
                    var songCells = songRowNode.ChildNodes.Where(c => c.Name == "td").ToList();
                    var nameCell = songCells[0];
                    var songUrl = nameCell.ChildNodes[0].Attributes["href"].Value;
                    var originalArtistCell = songCells[1];
                    var originalArtistUrl = originalArtistCell.ChildNodes.Any() &&
                                            originalArtistCell.ChildNodes[0].Name == "a"
                        ? originalArtistCell.ChildNodes[0].Attributes["href"].Value
                        : null;
                    var timesCell = songCells[2];
                    var timesUrl = timesCell.ChildNodes.Any() &&
                                   timesCell.ChildNodes[0].Name == "a"
                        ? timesCell.ChildNodes[0].Attributes["href"].Value
                        : null;
                    string debutLink = null;
                    HtmlNode gapCell = null;
                    string lastLink = null;
                    HtmlNode lastCell = null;
                    HtmlNode debutCell = null;
                    if (songCells.Count > 3)
                    {
                        debutCell = songCells[3];
                        debutLink = debutCell.ChildNodes.Any() &&
                                    debutCell.ChildNodes[0].Name == "a"
                            ? debutCell.ChildNodes[0].Attributes["href"].Value
                            : null;
                        lastCell = songCells[4];
                        lastLink = lastCell.ChildNodes.Any() &&
                                   lastCell.ChildNodes[0].Name == "a"
                            ? lastCell.ChildNodes[0].Attributes["href"].Value
                            : null;
                        gapCell = songCells[5];
                    }

                    var song = new Song()
                    {
                        SongName = nameCell?.InnerText,
                        DebutLink = debutLink,
                        Gap = gapCell != null && !string.IsNullOrEmpty(gapCell.InnerText)
                            ? int.Parse(gapCell.InnerText)
                            : (int?) null,
                        LastLink = lastLink,
                        OriginalArtist = originalArtistCell?.InnerText,
                        OriginalArtistLink = originalArtistUrl,
                        Times = timesCell?.InnerText,
                        TimesLink = timesUrl,
                        IsAlias = classes.Contains("aliases"),
                        IsCover = classes.Contains("covers"),
                        IsOriginal = classes.Contains("originals"),
                        SongLink = songUrl
                    };
                    if (lastCell != null && DateTime.TryParse(lastCell.InnerText, out var songLast))
                    {
                        song.Last = songLast;
                    }

                    if (debutCell != null && DateTime.TryParse(debutCell.InnerText, out var songDebut))
                    {
                        song.Debut = songDebut;
                    }

                    if (gapCell != null && int.TryParse(gapCell.InnerText, out var songGap))
                    {
                        song.Gap = songGap;
                    }

                    list.Add(song);
                }
                cacheEntry = list;
            }

            return cacheEntry;

        }
    }
}