using System.Collections.Generic;
using System.Threading.Tasks;
using Phish.Domain;

namespace Phish.ApiClient
{
    public interface IShowsDataService
    {
        Task<IEnumerable<Show>> GetShowsAsync();

        Task<IEnumerable<ShowLink>> GetShowLinksAsync(int showId);

        Task<IEnumerable<UpcomingShow>> GetUpcomingShowsAsync();
    }
}