using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Phish.Domain;


namespace Phish.WebApi.Tests
{
    //[TestClass]
    //public class SetListsControllerTests : WebApiTestBase
    //{
    //    [TestMethod]
    //    public async Task GetSetListTest()
    //    {
    //        var httpClient = GetHttpClient();
    //        var response = await httpClient.GetAsync("api/setlists/random/");
    //        var responseString = await response.Content.ReadAsStringAsync();
    //        response.EnsureSuccessStatusCode();
    //        var randomSetlist = JsonConvert.DeserializeObject<SetListViewModel>(responseString);
    //        Assert.IsNotNull(randomSetlist);
    //        response = await httpClient.GetAsync($"api/setlists/{randomSetlist.ShowId.Value}");
    //        responseString = await response.Content.ReadAsStringAsync();
    //        response.EnsureSuccessStatusCode();
    //        var setList = JsonConvert.DeserializeObject<SetListViewModel>(responseString);
    //        Assert.IsNotNull(setList);
    //    }

    //    [TestMethod]
    //    public async Task GetLatestSetListsTest()
    //    {
    //        var httpClient = GetHttpClient();
    //        var response = await httpClient.GetAsync("api/setlists/latest");
    //        var responseString = await response.Content.ReadAsStringAsync();
    //        response.EnsureSuccessStatusCode();
    //        var latestSetList = JsonConvert.DeserializeObject<SetListViewModel>(responseString);
    //        Assert.IsNotNull(latestSetList);
    //    }

    //    [TestMethod]
    //    public async Task GetRecentSetListsTest()
    //    {
    //        var httpClient = GetHttpClient();
    //        var response = await httpClient.GetAsync("api/setlists/recent");
    //        var responseString = await response.Content.ReadAsStringAsync();
    //        response.EnsureSuccessStatusCode();
    //        var recentSetLists = JsonConvert.DeserializeObject<List<SetListViewModel>>(responseString);
    //        Assert.IsNotNull(recentSetLists);
    //    }

    //    [TestMethod]
    //    public async Task GetRandomSetListTest()
    //    {
    //        var httpClient = GetHttpClient();
    //        var response = await httpClient.GetAsync("api/setlists/random/");
    //        var responseString = await response.Content.ReadAsStringAsync();
    //        response.EnsureSuccessStatusCode();
    //        var randomSetlist = JsonConvert.DeserializeObject<SetListViewModel>(responseString);
    //        Assert.IsNotNull(randomSetlist);
    //    }

        
    //}
}