using System.Threading.Tasks;
using Phish.Domain;
using Phish.ViewModels;

namespace Phish.WebApi.Services
{
    public interface IModelTransformationService
    {
        Task<SetListViewModel> GetSetListViewModelAsync(SetList setList);

        Task<ShowViewModel> GetShowViewModelAsync(UpcomingShow show);
    }
}