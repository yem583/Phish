using System;
using System.Net.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Caching;
using Polly.Caching.Memory;
using Polly.Registry;

namespace Phish.ApiClient.Tests
{
    public class ApiClientTestBase
    {
        protected readonly IServiceCollection ServiceCollection;
 
        protected IConfiguration Configuration { get; }
      
        public ApiClientTestBase()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .AddEnvironmentVariables()
                .Build();
            ServiceCollection = new ServiceCollection();
            ServiceCollection.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });
            ServiceCollection.AddMemoryCache();
            ServiceCollection.AddHttpClient<IArtistsDataService, ArtistsDataService>().
               AddTransientHttpErrorPolicy(p =>p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));
            ServiceCollection.AddHttpClient<IVenuesDataService, VenuesDataService>().
                AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));
            ServiceCollection.AddHttpClient<IShowsDataService, ShowsDataService>().
                AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));

            ServiceCollection.AddHttpClient<ISetListDataService,SetListDataService>().
                AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));


            ServiceCollection.AddTransient<IApiClientConfiguration, ApiClientConfiguration>();

            ServiceCollection.AddSingleton<IConfiguration>(Configuration);
        }


        private IServiceProvider _serviceProvider;
    
        protected IServiceProvider ServiceProvider => _serviceProvider ?? (_serviceProvider = ServiceCollection.BuildServiceProvider());

    }
}
