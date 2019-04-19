using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Phish.ApiClient
{
    public class ApiClientConfiguration : IApiClientConfiguration
    {
        public ApiClientConfiguration(IConfiguration configuration)
        {
            PhishApiBaseUrl = configuration["PhishApiBaseUrl"];
            PhishApiKey = configuration["PhishApiKey"];
        }

        public string PhishApiBaseUrl { get; set; }

        public string PhishApiKey { get; set; }
    }
}
