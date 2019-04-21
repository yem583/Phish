using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Phish.ApiClient;
using Phish.Domain;
using Phish.ViewModels;
using Phish.WebApi.Services;

namespace Phish.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly IShowsDataService _showsDataService;
        private readonly IModelTransformationService _modelTransformationService;

        public ShowsController(IShowsDataService showsDataService,IModelTransformationService modelTransformationService)
        {
            _showsDataService = showsDataService;
            _modelTransformationService = modelTransformationService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Show>), 200)]
        public async Task<ActionResult<IEnumerable<Show>>> GetShows()
        {
            var shows = await _showsDataService.GetShowsAsync();
            return shows.ToList();
        }

        [HttpGet("upcoming")]
        [ProducesResponseType(typeof(IEnumerable<ShowViewModel>), 200)]
        public async Task<ActionResult<IEnumerable<ShowViewModel>>> GetUpcomingShows()
        {
            var shows = await _showsDataService.GetUpcomingShowsAsync();
            var list = new List<ShowViewModel>();
            foreach (var show in shows)
            {
                var vm = await _modelTransformationService.GetShowViewModelAsync(show);
                list.Add(vm);
            }

            return list;
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