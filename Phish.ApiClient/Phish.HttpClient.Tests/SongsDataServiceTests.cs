using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Phish.HttpClient.Tests
{
    [TestClass]
    public class SongsDataServiceTests : HttpClientTestBase
    {
        [TestMethod]
        public async Task GetVenuesAsyncTest()
        {
            var songDataService = ServiceProvider.GetService<ISongDataService>();
            var songs = await songDataService.GetSongsAsync();
            Assert.IsNotNull(songs);
            Assert.IsTrue(songs.Any());

        }
    }
}