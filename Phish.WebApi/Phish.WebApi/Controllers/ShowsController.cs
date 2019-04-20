using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Phish.ApiClient;
using Phish.Domain;

namespace Phish.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly IShowsDataService _showsDataService;

        public ShowsController(IShowsDataService showsDataService)
        {
            _showsDataService = showsDataService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Show>), 200)]
        public async Task<ActionResult<IEnumerable<Show>>> GetShows()
        {
            var shows = await _showsDataService.GetShowsAsync();
            return shows.ToList();
        }

        [HttpGet("upcoming")]
        [ProducesResponseType(typeof(IEnumerable<UpcomingShow>), 200)]
        public async Task<ActionResult<IEnumerable<UpcomingShow>>> GetUpcomingShows()
        {
            var shows = await _showsDataService.GetUpcomingShowsAsync();
            return shows.ToList();
        }

        [HttpGet("links/{showId}")]
        [ProducesResponseType(typeof(IEnumerable<ShowLink>), 200)]
        public async Task<ActionResult<IEnumerable<ShowLink>>> GetShowLinks(int showId)
        {
            var showLinks = await _showsDataService.GetShowLinksAsync(showId);
            return showLinks.ToList();
        }
    }
}