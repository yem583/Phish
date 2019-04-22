using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;

namespace Phish.HttpClient.Tests
{
    public class HttpClientTestBase
    {
        protected readonly IServiceCollection ServiceCollection;

        protected IConfiguration Configuration { get; }

        public HttpClientTestBase()
        {
            ServiceCollection = new ServiceCollection();
            ServiceCollection.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });
            ServiceCollection.AddMemoryCache();
            ServiceCollection.AddHttpClient<IVenuesDataService,VenuesDataService>().
                AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));

        }

        private IServiceProvider _serviceProvider;

        protected IServiceProvider ServiceProvider => _serviceProvider ?? (_serviceProvider = ServiceCollection.BuildServiceProvider());

    }
}