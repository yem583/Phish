using System.Collections.Generic;
using System.Threading.Tasks;
using Phish.Domain;

namespace Phish.ApiClient
{
    public interface IVenuesDataService
    {
        Task<IEnumerable<Venue>> GetVenuesAsync();
    }
}