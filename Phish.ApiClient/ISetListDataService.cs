using System.Collections.Generic;
using System.Threading.Tasks;
using Phish.Domain;

namespace Phish.ApiClient
{
    public interface ISetListDataService
    {
        Task<SetList> GetSetListAsync(int showId);

        Task<SetList> GetMostRecentSetListAsync();

        Task<List<SetList>> GetRecentSetListsAsync(int limit=10);

        Task<SetList> GetRandomSetListAsync();
    }
}