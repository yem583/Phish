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
    public class VenuesController : ControllerBase
    {
        private readonly HttpClient.IVenuesDataService _venuesDataService;

        public VenuesController(HttpClient.IVenuesDataService venuesDataService)
        {
            _venuesDataService = venuesDataService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Venue>), 200)]
        public async Task<ActionResult<IEnumerable<Venue>>> GetVenues()
        {
            var venues = await _venuesDataService.GetVenuesAsync();
            return venues.ToList();
        }

        [HttpGet("{venueId}")]
        [ProducesResponseType(typeof(Venue), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        public async Task<ActionResult<Venue>> GetVenue(int venueId)
        {
            if (venueId == 0)
            {
                ModelState.AddModelError("Id", "Venue Id is required");
                return BadRequest(ModelState);
            }
            var venues = await _venuesDataService.GetVenuesAsync();
            if (venues == null || !venues.Any())
            {
                return NotFound();
            }
            return venues.FirstOrDefault();
        }
    }
}