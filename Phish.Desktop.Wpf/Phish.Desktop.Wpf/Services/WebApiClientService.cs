using System.Net.Http;
using System.Threading.Tasks;
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

        public async Task<SetListModel> GetRandomSetlistAsync()
        {
            var result = await _httpClient.GetAsync("http://api.phishfact.com/api/setlists/random");
            var response = await result.Content.ReadAsStringAsync();
            result.EnsureSuccessStatusCode();
            var setList = JsonConvert.DeserializeObject<SetListModel>(response);
            return setList;
        }
    }
}