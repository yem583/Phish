using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Phish.Domain;

namespace Phish.WebApi.Tests
{
    [TestClass]
    public class SongsControllerTests : WebApiTestBase
    {
        [TestMethod]
        public async Task GetSongsTest()
        {
            var httpClient = GetHttpClient();
            var response = await httpClient.GetAsync("api/songs");
            var responseString = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            var songs = JsonConvert.DeserializeObject<List<Song>>(responseString);
            Assert.IsNotNull(songs);
        }
    }
}