using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Phish.Domain;

namespace Phish.ApiClient
{
    public class ArtistsDataService : ApiDataServiceBase, IArtistsDataService
    {
        public ArtistsDataService(HttpClient client, IApiClientConfiguration apiClientConfiguration, IMemoryCache memoryCache)
            :base(client,apiClientConfiguration, memoryCache){}

        public async Task<IEnumerable<Artist>> GetArtistsAsync()
        {
            return await GetCachedList<Artist, ResponseContainer<Artist>>("artists/all", CacheKeys.Artists);
        }
    }
}
