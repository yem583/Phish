using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Phish.HttpClient.Tests
{
    [TestClass]
    public class ShowGapDataServiceTests : HttpClientTestBase
    {
        [TestMethod]
        public async Task GetVenuesAsyncTest()
        {
            var showGapsDataService = ServiceProvider.GetService<IShowGapsDataService>();
            var showGaps = await showGapsDataService.GetShowGapAsync();
            Assert.IsNotNull(showGaps);
            Assert.IsTrue(showGaps.Any());

        }
    }
}