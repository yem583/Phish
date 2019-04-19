using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;

namespace Phish.ApiClient.Tests
{
    [TestClass]
    public class ArtistsDataServiceTests : ApiClientTestBase
    {
        [TestMethod]
        public async Task GetArtistsAsyncTest()
        {
           var artistsDataService = ServiceProvider.GetService<IArtistsDataService>();
            var artists = await artistsDataService.GetArtistsAsync();
            Assert.IsNotNull(artists);
            Assert.IsTrue(artists.Any());
            //exercises cache
            artists = await artistsDataService.GetArtistsAsync();
        }
    }
}