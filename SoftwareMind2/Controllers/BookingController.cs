using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareMind.Models;
using SoftwareMind2.DTOs.BookingDTO;
using SoftwareMind2.DTOs.DeskDTO;
using SoftwareMind2.Services.BookingService;

namespace SoftwareMind2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingDbContext _context;
        private readonly IBookingService _bookingService;

        public BookingController(BookingDbContext context, IBookingService bookingService)
        {
            _context = context;
            _bookingService = bookingService;
        }

        

        

        // POST: api/Location
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Booking>> CreateBooking(CreateBookingRequest request)
        {
            var user = HttpContext.User;
            var nameIdentifier = int.Parse(user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var response = await _bookingService.CreateBooking(request, nameIdentifier);
            return CreatedAtAction(nameof(CreateBooking), response);
        }

        

        

        [HttpGet]
        [Route("searchoccupied")]
        [Authorize(Roles = "Employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SearchOccupiedDesks([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int idLocation)
        {
            try
            {
                var response = await _bookingService.SearchOccupiedDesks(startDate, endDate, idLocation);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpGet]
        [Route("searchfree")]
        [Authorize(Roles = "Employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SearchFreeDesks([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int idLocation)
        {
            try
            {
                var response = await _bookingService.SearchFreeDesks(startDate, endDate, idLocation);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/Location/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("changedesk")]
        [Authorize(Roles = "Employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ChangeDesk([FromQuery] int idBooking, [FromQuery] int newIdDesk)
        {
            var user = HttpContext.User;
            var nameIdentifier = int.Parse(user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var booking = await _context.Bookings.FirstOrDefaultAsync(e => e.idBooking == idBooking);
            if (nameIdentifier != booking.idUser) throw new Exception("not your booking");
            await _bookingService.ChangeDesk(idBooking, newIdDesk);
            return Ok();
        }

        [HttpGet]
        [Route("checkdesk")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CheckDeskAvailability([FromQuery] int idDesk, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var user = HttpContext.User;
            var roleIdentifier = (user.Claims.First(x => x.Type == ClaimTypes.Role).Value);
            
            try
            {
                var response = await _bookingService.CheckDeskAvailability(idDesk, startDate, endDate, roleIdentifier);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.idBooking == id);
        }
    }
}
