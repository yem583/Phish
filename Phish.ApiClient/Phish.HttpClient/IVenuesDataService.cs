using System.Collections.Generic;
using System.Threading.Tasks;
using Phish.Domain;

namespace Phish.HttpClient
{
    public interface IVenuesDataService
    {
        Task<IEnumerable<Venue>> GetVenuesAsync();
    }
}