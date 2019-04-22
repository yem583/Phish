using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Phish.HttpClient.Tests
{
    [TestClass]
    public class SideShowDataServiceTests : HttpClientTestBase
    {
        [TestMethod]
        public async Task GetSideShowsAsyncTest()
        {
            var sideShowDataService = ServiceProvider.GetService<ISideShowDataService>();
            var sideShows = await sideShowDataService.GetSideShowsAsync();
            Assert.IsNotNull(sideShows);
            Assert.IsTrue(sideShows.Any());

        }
    }
}