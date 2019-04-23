using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Phish.WebApi.Tests
{
    public class WebApiTestBase
    {
        protected readonly IServiceCollection ServiceCollection;

        protected IConfiguration Configuration { get; }

        protected String WebApiBaseUrl { get; }

        public WebApiTestBase()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .AddEnvironmentVariables()
                .Build();
            WebApiBaseUrl = Configuration["PhishWebApiBaseUrl"];
            ServiceCollection = new ServiceCollection();
            ServiceCollection.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });
            ServiceCollection.AddSingleton<IConfiguration>(Configuration);
        }

        private IServiceProvider _serviceProvider;
        protected IServiceProvider ServiceProvider => _serviceProvider ?? (_serviceProvider = ServiceCollection.BuildServiceProvider());


        protected System.Net.Http.HttpClient GetHttpClient()
        {
            var httpClient = new System.Net.Http.HttpClient();
            httpClient.BaseAddress = new Uri(WebApiBaseUrl);
            return httpClient;
        }
    }
}