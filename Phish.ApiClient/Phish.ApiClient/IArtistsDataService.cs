using System.Collections.Generic;
using System.Threading.Tasks;
using Phish.Domain;

namespace Phish.ApiClient
{
    public interface IArtistsDataService
    {
        Task<IEnumerable<Artist>> GetArtistsAsync();

        Task<Artist> GetArtistAsync(int artistId);
    }
}