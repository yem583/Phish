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
    public class SetListsController : ControllerBase
    {
        private readonly ISetListDataService _setListDataService;

        public SetListsController(ISetListDataService setListDataService)
        {
            _setListDataService = setListDataService;
        }

        [HttpGet("{showId}")]
        [ProducesResponseType(typeof(SetList), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        public async Task<ActionResult<SetList>> GetSetList(int showId)
        {
            if (showId == 0)
            {
                ModelState.AddModelError("Id", "Show Id is required");
                return BadRequest(ModelState);
            }
            var setList = await _setListDataService.GetSetListAsync(showId);
            if (setList == null)
            {
                return NotFound();
            }
            return setList;
        }

        [HttpGet("latest")]
        [ProducesResponseType(typeof(SetList), 200)]
        public async Task<ActionResult<SetList>> GetMostRecentSetList()
        {
            var mostRecentSetList = await _setListDataService.GetMostRecentSetListAsync();
            return mostRecentSetList;
        }

        [HttpGet("recent")]
        [ProducesResponseType(typeof(IEnumerable<SetList>), 200)]
        public async Task<ActionResult<IEnumerable<SetList>>> GetRecentSetLists()
        {
            var mostRecentSetList = await _setListDataService.GetRecentSetListsAsync();
            return mostRecentSetList;
        }

        [HttpGet("random")]
        [ProducesResponseType(typeof(SetList), 200)]
        public async Task<ActionResult<SetList>> GetRandomSetList()
        {
            var mostRecentSetList = await _setListDataService.GetRandomSetListAsync();
            return mostRecentSetList;
        }
    }
}