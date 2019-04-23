using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Phish.HttpClient.Tests
{
    [TestClass]
    public class UpcomingShowDataServiceTests : HttpClientTestBase
    {
        [TestMethod]
        public async Task GetVenuesAsyncTest()
        {
            var upcomingShowsDataService = ServiceProvider.GetService<IUpcomingShowsDataService>();
            var upcomingShows = await upcomingShowsDataService.GetUpcomingShowsAsync();
            Assert.IsNotNull(upcomingShows);
            Assert.IsTrue(upcomingShows.Any());

        }
    }
}