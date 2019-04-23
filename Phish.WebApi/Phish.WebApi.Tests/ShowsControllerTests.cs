using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Phish.Domain;


namespace Phish.WebApi.Tests
{
    [TestClass]
    public class ShowsControllerTests : WebApiTestBase
    {
        [TestMethod]
        public async Task GetUpcomingShowsTest()
        {
            var httpClient = GetHttpClient();
            var response = await httpClient.GetAsync("api/shows/upcoming");
            var responseString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            var upcomingShows = JsonConvert.DeserializeObject<List<UpcomingShow>>(responseString);
            Assert.IsNotNull(upcomingShows);
        }

      
    }
}