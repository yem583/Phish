using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Documents;
using Newtonsoft.Json;
using Phish.Domain;
using Phish.ViewModels;

namespace Phish.Desktop.Wpf.Services
{
    public class WebApiClientService : IWebApiClientService
    {
        private readonly HttpClient _httpClient;

        public WebApiClientService()
        {
            _httpClient = new HttpClient();
        }

        //TODO:Need to move api url to Config
        public async Task<SetListViewModel> GetRandomSetlistAsync()
        {
            var result = await _httpClient.GetAsync("http://api.phishfact.com/api/setlists/random");
            var response = await result.Content.ReadAsStringAsync();
            result.EnsureSuccessStatusCode();
            var setList = JsonConvert.DeserializeObject<SetListViewModel>(response);
            return setList;
        }

        public async Task<IEnumerable<Song>> GetSongsAsync()
        {
            var result = await _httpClient.GetAsync("http://api.phishfact.com/api/songs");
            var response = await result.Content.ReadAsStringAsync();
            result.EnsureSuccessStatusCode();
            var songs = JsonConvert.DeserializeObject<List<Song>>(response);
            return songs;
        }

        public async Task<List<UpcomingShow>> GetUpcomingShowsAsync()
        {
            var result = await _httpClient.GetAsync("http://api.phishfact.com/api/shows/upcoming");
            var response = await result.Content.ReadAsStringAsync();
            result.EnsureSuccessStatusCode();
            var setList = JsonConvert.DeserializeObject<List<UpcomingShow>>(response);
            return setList;
        }
    }
}