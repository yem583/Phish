using System.Collections.Generic;
using System.Linq;
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

        public async Task<Venue> GetVenueAsync(int venueId)
        {
            var allVenues = await GetCachedList<Venue, ResponseContainer<Venue>>("venues/all", CacheKeys.Venues);
            return allVenues.FirstOrDefault(v => v.VenueId == venueId);
        }
    }
}