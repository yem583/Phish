using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Phish.ApiClient.Tests
{
    [TestClass]
    public class SetListDataServiceTests : ApiClientTestBase
    {
        [TestMethod]
        public async Task GetSetListAsyncTest()
        {
            var setListDataService = ServiceProvider.GetService<ISetListDataService>();
            var randomSetList = await setListDataService.GetRandomSetListAsync();
            Assert.IsNotNull(randomSetList);
            var setList = await setListDataService.GetSetListAsync(randomSetList.ShowId.Value);
            Assert.IsNotNull(setList);
        }

        [TestMethod]
        public async Task GetLatestSetListAsyncTest()
        {
            var setListDataService = ServiceProvider.GetService<ISetListDataService>();
            var setList = await setListDataService.GetLatestSetListAsync();
            Assert.IsNotNull(setList);
        }

        [TestMethod]
        public async Task GetRecentSetListsAsyncTest()
        {
            var setListDataService = ServiceProvider.GetService<ISetListDataService>();
            var setList = await setListDataService.GetRecentSetListsAsync();
            Assert.IsNotNull(setList);
            Assert.IsTrue(setList.Count == 10);
        }

        [TestMethod]
        public async Task GetRandomSetListAsyncTest()
        {
            var setListDataService = ServiceProvider.GetService<ISetListDataService>();
            var setList = await setListDataService.GetRandomSetListAsync();
            Assert.IsNotNull(setList);
        }
    }
}