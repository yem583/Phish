using System.Collections.Generic;
using System.Threading.Tasks;
using Phish.Domain;
using Phish.ViewModels;

namespace Phish.Desktop.Wpf.Services
{
    public interface IWebApiClientService
    {
        Task<SetListViewModel> GetRandomSetlistAsync();

        Task<List<UpcomingShow>> GetUpcomingShowsAsync();

        Task<IEnumerable<Song>> GetSongsAsync();
    }
}