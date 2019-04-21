using HtmlAgilityPack;
using Phish.ApiClient;
using Phish.Domain;
using Phish.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace Phish.WebApi.Services
{
    public class ModelTransformationService : IModelTransformationService
    {
        private readonly IArtistsDataService _artistsDataService;
        private readonly IVenuesDataService _venuesDataService;
        private readonly IShowsDataService _showsDataService;

        public ModelTransformationService(IArtistsDataService artistsDataService,
            IVenuesDataService venuesDataService,IShowsDataService showsDataService)
        {
            _artistsDataService = artistsDataService;
            _venuesDataService = venuesDataService;
            _showsDataService = showsDataService;
        }

        public async Task<ShowViewModel> GetShowViewModelAsync(UpcomingShow upcomingShow)
        {
            var artist = upcomingShow.ArtistId.HasValue ? await _artistsDataService.GetArtistAsync(upcomingShow.ArtistId.Value) : null;
            var venue = upcomingShow.VenueId.HasValue ? await _venuesDataService.GetVenueAsync(upcomingShow.VenueId.Value) : null;
            var shows = await _showsDataService.GetShowsAsync();
            var show = shows.FirstOrDefault(s => s.ShowId == upcomingShow.ShowId);
            if (show != null)
            {
                var showViewModel = new ShowViewModel()
                {
                    Venue = venue,
                    ShowId = show.ShowId,
                    SetListNotes = show.SetListNotes,
                    Location = show.Location,
                    ShowDate = show.ShowDate,
                    Artist = artist,
                    BilledAs = show.BilledAs,
                    Link = show.Link,
                    TourId = show.TourId,
                    TourName = show.TourName,
                    TourWhen = show.TourWhen
                };
                return showViewModel;
            }

            return null;
        }

        public async Task<SetListViewModel> GetSetListViewModelAsync(SetList setList)
        {
            var artist = setList.ArtistId.HasValue? await _artistsDataService.GetArtistAsync(setList.ArtistId.Value):null;
            var venue = setList.VenuId.HasValue ?await _venuesDataService.GetVenueAsync(setList.VenuId.Value) : null;

            var setListModel = new SetListViewModel()
            {
                Artist = artist,
                Location = setList.Location?.Replace(", USA", ""),
                LongDate = setList.LongDate,
                ShortDate = setList.ShortDate,
                ShowDate = setList.ShowDate,
                Url = setList.Url,
                Venue = venue,
                ShowId = setList.ShowId,
                RelativeDate = setList.RelativeDate,
                GapChart = setList.GapChart,
                Rating = !string.IsNullOrWhiteSpace(setList.Rating)?decimal.Parse(setList.Rating):(decimal?) null,
                SetListNotes = GetInnerText(setList.SetListNotes)?.Replace("via phish.net", "")

        };
            var doc = new HtmlDocument();
            doc.LoadHtml(setList.SetListData);
            setListModel.Sets = GetSetsForSetList(doc);
            setListModel.FooterItems = GetFooterItems(doc);
            return setListModel;
        }

        private ObservableCollection<SetListFooterItemViewModel> GetFooterItems(HtmlDocument doc)
        {
            var list = new ObservableCollection<SetListFooterItemViewModel>();
            var footerSup = doc.DocumentNode.SelectSingleNode("//*[@class='setlist-footer']");
            if (footerSup != null)
            {
                var items = footerSup.InnerHtml.Split("<br>");
                foreach (var item in items)
                {
                    var setListFooterItem = new SetListFooterItemViewModel();
                    setListFooterItem.Value = item;
                    list.Add(setListFooterItem);
                }
            }

            return list;
        }

        private ObservableCollection<SetListSetViewModel> GetSetsForSetList(HtmlDocument doc)
        {
            var list = new ObservableCollection<SetListSetViewModel>();
            var sets = doc.DocumentNode.SelectNodes("//*[@class='set-label']");
            foreach (var setNode in sets)
            {
                var set = new SetListSetViewModel();
                set.SetLabel = setNode.InnerText;
                var songs = setNode.ParentNode.SelectNodes("*[@class='setlist-song']");
                if (songs != null)
                {
                    foreach (var songNode in songs)
                    {
                        var setListSong = new SetListSongViewModel();
                        if (songNode?.NextSibling?.InnerText == " > ")
                        {
                            setListSong.RightIntoNextSong = true;
                        }
                        setListSong.Song = songNode.InnerText;
                        setListSong.Link = songNode.Attributes["href"].Value;
                        var hasSup = songNode.NextSibling?.Name == "sup";
                        if (hasSup)
                        {
                            setListSong.SupValue = songNode.NextSibling.InnerText;
                            setListSong.SupTitle = songNode.NextSibling.Attributes["title"].Value;
                        }

                        if (songNode == songs.Last())
                        {
                            setListSong.IsSetCloser = true;
                        }
                        set.SetListSongs.Add(setListSong);
                    }
                }

                list.Add(set);
            }
            return list;
        }

        private string GetInnerText(string html)
        {
            if (!string.IsNullOrEmpty(html))
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                return HttpUtility.HtmlDecode(doc.DocumentNode.InnerText);
            }
            return null;
        }

        private string GetAnchorTagInnerText(string html)
        {
            if (!string.IsNullOrEmpty(html))
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                var anchorTag = doc.DocumentNode
                    .Descendants("a")
                    .FirstOrDefault();
                if (anchorTag != null)
                {
                    return HttpUtility.HtmlDecode(anchorTag.InnerText);
                }
            }
            return null;
        }

    }
}
