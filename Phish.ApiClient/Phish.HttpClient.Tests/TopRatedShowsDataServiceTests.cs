using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Phish.HttpClient.Tests
{
    [TestClass]
    public class TopRatedShowsDataServiceTests : HttpClientTestBase
    {
        [TestMethod]
        public async Task GetVenuesAsyncTest()
        {
            var topRatedShowDataService = ServiceProvider.GetService<ITopRatedShowsDataService>();
            var topRatedShows = await topRatedShowDataService.GetTopRatedShowsAsync();
            Assert.IsNotNull(topRatedShows);
            Assert.IsTrue(topRatedShows.Any());

        }
    }
}