using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Phish.ApiClient
{
    public class ApiDataServiceBase<T, TResponse>
    {
        protected readonly HttpClient Client;
        protected readonly IApiClientConfiguration ApiClientConfiguration;
        protected readonly IMemoryCache MemoryCache;

        public ApiDataServiceBase(HttpClient client, IApiClientConfiguration apiClientConfiguration,
            IMemoryCache memoryCache)
        {
            Client = client;
            ApiClientConfiguration = apiClientConfiguration;
            MemoryCache = memoryCache;
            client.BaseAddress = new Uri($"{apiClientConfiguration.PhishApiBaseUrl}");
        }

        protected async Task<IEnumerable<T>> GetCachedList<T>(string url, string cacheKey)
        {
            if (!MemoryCache.TryGetValue(cacheKey, out IEnumerable<T> cacheEntry))
            {
                cacheEntry = await GetListAsync(url) as IEnumerable<T>;
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(DateTimeOffset.Now.AddDays(1));
                MemoryCache.Set(cacheKey, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }

        protected async Task<IEnumerable<T>> GetListAsync(string url,
            Dictionary<string, string> additionalQueryParameters = null)
        {
            string fullUrl = null;
            if (additionalQueryParameters == null)
            {
                fullUrl = $"{url}?apikey={ApiClientConfiguration.PhishApiKey}";
            }
            else
            {
                var paramsString = string.Join("&", additionalQueryParameters.Select(kvp => $"{kvp.Key}={kvp.Value}"));

                fullUrl = $"{url}?apikey={ApiClientConfiguration.PhishApiKey}&{paramsString}";
            }

            var response = await Client.GetAsync(fullUrl);

            response.EnsureSuccessStatusCode();

            dynamic responseContainer = await response.Content.ReadAsJsonAsync<TResponse>();

            var list = new List<T>();
            if (responseContainer?.Response?.Data != null)
                foreach (var item in responseContainer.Response.Data)
                    list.Add(item.GetType().Name.StartsWith("KeyValuePair") ? (T) item.Value : (T) item);
            return list;
        }
    }
}