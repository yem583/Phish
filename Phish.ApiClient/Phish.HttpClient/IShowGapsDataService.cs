using System.Collections.Generic;
using System.Threading.Tasks;
using Phish.Domain;

namespace Phish.HttpClient
{
    public interface IShowGapsDataService
    {
        Task<IEnumerable<ShowGap>> GetShowGapAsync();
    }
}