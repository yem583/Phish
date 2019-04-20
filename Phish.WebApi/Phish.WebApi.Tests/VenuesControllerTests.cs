using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Phish.Domain;

namespace Phish.WebApi.Tests
{
    [TestClass]
    public class VenuesControllerTests : WebApiTestBase
    {
        [TestMethod]
        public async Task GetVenuesTest()
        {
            var httpClient = GetHttpClient();
            var response = await httpClient.GetAsync("api/venues");
            var responseString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            var venues = JsonConvert.DeserializeObject<List<Venue>>(responseString);
            Assert.IsNotNull(venues);
        }

        [TestMethod]
        public async Task GetVenueTest()
        {
            var httpClient = GetHttpClient();
            var response = await httpClient.GetAsync("api/venues/1");
            var responseString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            var venue = JsonConvert.DeserializeObject<Venue>(responseString);
            Assert.IsNotNull(venue);
        }
    }
}