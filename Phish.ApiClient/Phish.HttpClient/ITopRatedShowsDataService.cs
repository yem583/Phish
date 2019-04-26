using System.Collections.Generic;
using System.Threading.Tasks;
using Phish.Domain;

namespace Phish.HttpClient
{
    public interface ITopRatedShowsDataService
    {
        Task<IEnumerable<TopRatedShow>> GetTopRatedShowsAsync();
    }
}