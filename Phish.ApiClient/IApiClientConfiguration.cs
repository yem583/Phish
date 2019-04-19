namespace Phish.ApiClient
{
    public interface IApiClientConfiguration
    {
        string PhishApiBaseUrl { get; set; }

        string PhishApiKey { get; set; }
    }

}