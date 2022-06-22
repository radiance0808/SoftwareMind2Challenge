using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareMind.Models;
using SoftwareMind2.DTOs;
using SoftwareMind2.DTOs.DeskDTO;
using SoftwareMind2.Services.DeskService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftwareMind2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeskController : ControllerBase
    {
        private readonly BookingDbContext _context;
        private readonly IDeskService _deskService;
        

        public DeskController(BookingDbContext context, IDeskService deskService)
        {
            _context = context;
            _deskService = deskService;
        }

        



        // POST: api/Location
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Desk>> CreateDesk(CreateDeskRequest request)
        {
            var response = await _deskService.CreateDesk(request);
            return CreatedAtAction(nameof(CreateDesk), response);
        }

        // DELETE: api/Location/5
        [HttpDelete("{idDesk}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteDesk(int idDesk)
        {
            await _deskService.DeleteDesk(idDesk);
            return Ok();
        }

        // PUT: api/Location/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{idDesk}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> MakeUnavailable(int idDesk)
        {
            await _deskService.MakeUnavailable(idDesk);
            return Ok();
        }

        [HttpGet]
        [Route("searchbylocation")]
        [Authorize(Roles = "Employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SearchDesksByLocation([FromQuery] int idLocation)
        {
            try
            {
                var response = await _deskService.SearchDesksOnLocation(idLocation);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }


        }
        }
}
