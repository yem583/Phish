using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Phish.Domain;

namespace Phish.ApiClient
{
    public class SetListDataService : ApiDataServiceBase, ISetListDataService
    {
        public SetListDataService(HttpClient client, IApiClientConfiguration apiClientConfiguration,
            IMemoryCache memoryCache)
            : base(client, apiClientConfiguration, memoryCache)
        {
        }

        public async Task<SetList> GetSetListAsync(int showId)
        {
            var setLists = await GetListAsync<SetList, ResponseContainerWithArray<SetList>>("setlists/get", new Dictionary<string, string> { { "showid", showId.ToString() } });
            return setLists.FirstOrDefault();
        }

        public async Task<SetList> GetMostRecentSetListAsync()
        {
            var setLists = await GetListAsync<SetList, ResponseContainerWithArray<SetList>>("setlists/latest");
            return setLists.FirstOrDefault();
        }

        public async Task<List<SetList>> GetRecentSetListsAsync(int limit=10)
        {
            var setLists = await GetListAsync<SetList, ResponseContainerWithArray<SetList>>("setlists/recent", new Dictionary<string, string> { { "limit", limit.ToString() } });
            return setLists.ToList();
        }

        public async Task<SetList> GetRandomSetListAsync()
        {
            var setLists = await GetListAsync<SetList, ResponseContainerWithArray<SetList>>("setlists/random");
            return setLists.FirstOrDefault();
        }
    }
}