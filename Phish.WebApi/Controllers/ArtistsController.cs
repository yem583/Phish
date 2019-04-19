using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Phish.ApiClient;
using Phish.Domain;


namespace Phish.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ArtistsController : Controller
    {
        private readonly IArtistsDataService _artistsDataService;

        public ArtistsController(IArtistsDataService artistsDataService)
        {
            _artistsDataService = artistsDataService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Artist>), 200)]
        public async Task<ActionResult<IEnumerable<Artist>>> GetClients()
        {
            var artists = await _artistsDataService.GetArtistsAsync();
            return artists.ToList();
        }

        [HttpGet("{artistId}")]
        [ProducesResponseType(typeof(Artist), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        public async Task<ActionResult<Artist>> GetArtist(int artistId)
        {
            if (artistId == 0)
            {
                ModelState.AddModelError("Id", "Artist Id is required");
                return BadRequest(ModelState);
            }
            var artists = await _artistsDataService.GetArtistsAsync();
            if (artists == null || !artists.Any())
            {
                return NotFound();
            }
            return artists.FirstOrDefault();
        }
    }
}
