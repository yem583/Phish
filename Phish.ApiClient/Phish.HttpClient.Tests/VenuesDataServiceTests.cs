using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Phish.HttpClient.Tests
{
    [TestClass]
    public class VenuesDataServiceTests : HttpClientTestBase
    {
        [TestMethod]
        public async Task GetVenuesAsyncTest()
        {
            var venuesDataService = ServiceProvider.GetService<IVenuesDataService>();
            var venues = await venuesDataService.GetVenuesAsync();
            Assert.IsNotNull(venues);
            Assert.IsTrue(venues.Any());
            
        }
    }
}
