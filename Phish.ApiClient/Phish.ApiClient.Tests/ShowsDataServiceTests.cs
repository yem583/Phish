using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Phish.ApiClient.Tests
{
    [TestClass]
    public class ShowsDataServiceTests : ApiClientTestBase
    {
        [TestMethod]
        public async Task GetShowsAsyncTest()
        {
            var showsDataService = ServiceProvider.GetService<IShowsDataService>();
            var shows = await showsDataService.GetShowsAsync();
            Assert.IsNotNull(shows);
            Assert.IsTrue(shows.Any());
            //exercises cache
            shows = await showsDataService.GetShowsAsync();
        }

        [TestMethod]
        public async Task GetShowLinksAsyncTest()
        {
            var showsDataService = ServiceProvider.GetService<IShowsDataService>();
            var setListDataService = ServiceProvider.GetService<ISetListDataService>();
            var setList = await setListDataService.GetSetListAsync(1252698446);
            Assert.IsNotNull(setList);
            var links = await showsDataService.GetShowLinksAsync(1252698446);
            Assert.IsNotNull(links);
        }

        [TestMethod]
        public async Task GetUpcomingShowsAsyncTest()
        {
            var showsDataService = ServiceProvider.GetService<IShowsDataService>();
            var upcomingShows = await showsDataService.GetUpcomingShowsAsync();
            Assert.IsNotNull(upcomingShows);
        }
    }
}