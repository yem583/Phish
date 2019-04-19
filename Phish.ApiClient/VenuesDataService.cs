using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Phish.Domain;

namespace Phish.ApiClient
{
    public class VenuesDataService : ApiDataServiceBase, IVenuesDataService
    {
        public VenuesDataService(HttpClient client, IApiClientConfiguration apiClientConfiguration, IMemoryCache memoryCache)
            : base(client, apiClientConfiguration, memoryCache) { }

        public async Task<IEnumerable<Venue>> GetVenuesAsync()
        {
            return await GetCachedList<Venue, ResponseContainer<Venue>>("venues/all", CacheKeys.Venues);
        }
    }
}