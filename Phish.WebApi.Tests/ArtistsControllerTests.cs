using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Phish.Domain;

namespace Phish.WebApi.Tests
{
    [TestClass]
    public class ArtistsControllerTests: WebApiTestBase
    {
        [TestMethod]
        public async Task GetArtistsTest()
        {
            var httpClient = GetHttpClient();
            var response = await httpClient.GetAsync("api/artists");
            var responseString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            var artists = JsonConvert.DeserializeObject<List<Artist>>(responseString);
            Assert.IsNotNull(artists);
        }

        [TestMethod]
        public async Task GetArtistTest()
        {
            var httpClient = GetHttpClient();
            var response = await httpClient.GetAsync("api/artists/1");
            var responseString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            var artists = JsonConvert.DeserializeObject<Artist>(responseString);
            Assert.IsNotNull(artists);
        }
    }
}
