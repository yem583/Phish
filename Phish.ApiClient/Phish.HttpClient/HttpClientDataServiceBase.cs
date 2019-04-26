using System;
using Microsoft.Extensions.Caching.Memory;

namespace Phish.HttpClient
{
    public class HttpClientDataServiceBase
    {
        protected readonly System.Net.Http.HttpClient Client;
        protected readonly IMemoryCache MemoryCache;

        public HttpClientDataServiceBase(System.Net.Http.HttpClient client, IMemoryCache memoryCache)
        {
            Client = client;
            MemoryCache = memoryCache;
            client.BaseAddress = new Uri("https://www.phish.net/");
        }
    }
}