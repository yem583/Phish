using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Phish.Domain;
using Phish.HttpClient;

namespace Phish.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly IUpcomingShowsDataService _upcomingShowsDataService;
   
        public ShowsController(IUpcomingShowsDataService upcomingShowsDataService)
        {
            _upcomingShowsDataService = upcomingShowsDataService;
        }

        [HttpGet("upcoming")]
        [ProducesResponseType(typeof(IEnumerable<UpcomingShow>), 200)]
        public async Task<ActionResult<IEnumerable<UpcomingShow>>> GetUpcomingShows()
        {
            var shows = await _upcomingShowsDataService.GetUpcomingShowsAsync();
            return shows.ToList();
        }

    }
}