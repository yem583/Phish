using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
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
    public class SetListsController : ControllerBase
    {
        private readonly ISetListDataService _setListDataService;
        private readonly IModelTransformationService _modelTransformationService;

        public SetListsController(ISetListDataService setListDataService, 
            IModelTransformationService modelTransformationService)
        {
            _setListDataService = setListDataService;
            _modelTransformationService = modelTransformationService;
        }

        [HttpGet("{showId}")]
        [ProducesResponseType(typeof(SetListModel), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        public async Task<ActionResult<SetListModel>> GetSetList(int showId)
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
            var transformed = await _modelTransformationService.GetSetListModelAsync(setList);
            return transformed;
        }

        [HttpGet("latest")]
        [ProducesResponseType(typeof(SetListModel), 200)]
        public async Task<ActionResult<SetListModel>> GetLatestSetList()
        {
            var mostRecentSetList = await _setListDataService.GetLatestSetListAsync();
            var transformed = await _modelTransformationService.GetSetListModelAsync(mostRecentSetList);
            return transformed;
        }

        [HttpGet("recent")]
        [ProducesResponseType(typeof(IEnumerable<SetListModel>), 200)]
        public async Task<ActionResult<IEnumerable<SetListModel>>> GetRecentSetLists()
        {
            var list = new List<SetListModel>();
            var mostRecentSetList = await _setListDataService.GetRecentSetListsAsync();
            foreach (var recentSetList in mostRecentSetList)
            {
                var transformed = await _modelTransformationService.GetSetListModelAsync(recentSetList);
                list.Add(transformed);
            }
            return list;
        }

        [HttpGet("random")]
        [ProducesResponseType(typeof(SetListModel), 200)]
        public async Task<ActionResult<SetListModel>> GetRandomSetList()
        {
            var randomSetList = await _setListDataService.GetRandomSetListAsync();
            var transformed = await _modelTransformationService.GetSetListModelAsync(randomSetList);
            return transformed;
        }
    }

}