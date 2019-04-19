using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Phish.Domain;

namespace Phish.ApiClient
{
    public class ShowsDataService : ApiDataServiceBase<Show,ResponseContainerWithArray<Show>>, IShowsDataService
    {
        public ShowsDataService(HttpClient client, IApiClientConfiguration apiClientConfiguration, IMemoryCache memoryCache)
            : base(client, apiClientConfiguration, memoryCache) { }

        public async Task<IEnumerable<Show>> GetShowsAsync()
        {
            if (!MemoryCache.TryGetValue(CacheKeys.Shows, out IEnumerable<Show> cacheEntry))
            {
                var shows = new List<Show>();
                for (var i = 1982; i <= DateTime.Now.Year; i++)
                {
                    Thread.Sleep(1000);//avoid rate limit quota
                    var showsForYear = await GetListAsync("shows/query", new Dictionary<string, string> { { "year", i.ToString() } });
                    shows.AddRange(showsForYear);
                }
                cacheEntry = shows;
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(DateTimeOffset.Now.AddDays(1));
                MemoryCache.Set(CacheKeys.Shows, cacheEntry, cacheEntryOptions);
            }
            return cacheEntry;
        }
    }
}