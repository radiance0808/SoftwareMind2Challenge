using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareMind.Models;
using SoftwareMind2.DTOs.LocationDTO;
using SoftwareMind2.Services.LocationService;

namespace SoftwareMind2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly BookingDbContext _context;
        private readonly ILocationService _locationService;
        

        public LocationController(BookingDbContext context, ILocationService locationService)
        {
            _context = context;
            _locationService = locationService;
        }

        




        // POST: api/Location
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Location>> CreateLocation(CreateLocationRequest request)
        {
            var response = await _locationService.CreateLocation(request);
            return CreatedAtAction(nameof(CreateLocation), response);
        }

        // DELETE: api/Location/5
        [HttpDelete("{idLocation}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteLocation(int idLocation)
        {
            await _locationService.DeleteLocation(idLocation);
            return Ok();
        }

        private bool LocationExists(int id)
        {
            return _context.Locations.Any(e => e.idLocation == id);
        }
    }
}
