using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Phish.Domain;
using Phish.ViewModels;

namespace Phish.WebApi.Tests
{
    [TestClass]
    public class ShowsControllerTests : WebApiTestBase
    {
        [TestMethod]
        public async Task GetShowsTest()
        {
            var httpClient = GetHttpClient();
            var response = await httpClient.GetAsync("api/shows");
            var responseString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            var shows = JsonConvert.DeserializeObject<List<Show>>(responseString);
            Assert.IsNotNull(shows);
        }

        [TestMethod]
        public async Task GetUpcomingShowsTest()
        {
            var httpClient = GetHttpClient();
            var response = await httpClient.GetAsync("api/shows/upcoming");
            var responseString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            var upcomingShows = JsonConvert.DeserializeObject<List<ShowViewModel>>(responseString);
            Assert.IsNotNull(upcomingShows);
        }

        [TestMethod]
        public async Task GetShowLinksTest()
        {
            var httpClient = GetHttpClient();
            var response = await httpClient.GetAsync("api/shows/links/1252698446");
            var responseString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            var showLinks = JsonConvert.DeserializeObject<List<ShowLink>>(responseString);
            Assert.IsNotNull(showLinks);
        }
    }
}