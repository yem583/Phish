using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Phish.Domain;

namespace Phish.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly HttpClient.ISongDataService _songDataService;

        public SongsController(HttpClient.ISongDataService songDataService)
        {
            _songDataService = songDataService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Song>), 200)]
        public async Task<ActionResult<IEnumerable<Song>>> GetVenues()
        {
            var songs = await _songDataService.GetSongsAsync();
            return songs.ToList();
        }
    }
}