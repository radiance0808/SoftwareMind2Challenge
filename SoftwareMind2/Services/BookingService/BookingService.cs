using Microsoft.EntityFrameworkCore;
using SoftwareMind.Models;
using SoftwareMind2.DTOs.BookingDTO;
using SoftwareMind2.DTOs.DeskDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SoftwareMind2.Services.BookingService
{
    public class BookingService : IBookingService
    {

        private readonly BookingDbContext _context;

        public BookingService(BookingDbContext context)
        {
            _context = context;
        }

        public async Task ChangeDesk(int idBooking, int newIdDesk)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(e => e.idBooking == idBooking);
            if (booking == null) throw new System.Exception("no such booking");
            if ((booking.startDate - DateTime.Now).TotalHours < 24) throw new Exception("too late");
            var desks = await _context.Bookings.
                Where(e => e.startDate >= booking.startDate && e.endDate <= booking.endDate).
                Select(e => new SearchDesksByDateResponse
                {
                    idDesk = e.idDesk,
                    name = e.desk.name,
                    startDate = e.startDate,
                    endDate = e.endDate
                }).OrderBy(e => e.name).ToListAsync();
            var desk = desks.FirstOrDefault(e => e.idDesk == newIdDesk);
            if (desk != null) throw new Exception("Occupied desk");

            booking.idDesk = newIdDesk;

            await _context.SaveChangesAsync();
        }

        public async Task<DeskAvailabilityResponse> CheckDeskAvailability(int idDesk, DateTime startDate, DateTime endDate, string Role)
        {
            string message;
            var desk = await _context.Desks.FirstOrDefaultAsync(e => e.idDesk == idDesk);
            if (desk == null) throw new System.Exception("no such desk");
            var booking = await _context.Bookings.FirstOrDefaultAsync(e => e.idDesk == idDesk && e.startDate == startDate && e.endDate == endDate);
            
            if (booking == null)
            {
                 message = "desk is free";
                return new DeskAvailabilityResponse()
                {
                    idDesk = idDesk,
                    name = desk.name,
                    status = message
                    
                };
            } else {  message = "desk is occupied"; if (Role == "Admin")
                return new DeskAvailabilityResponse()
                {
                    idDesk = booking.idDesk,
                    name = booking.desk.name,
                    status = message,
                    idUser = booking.idUser.ToString()
                }; else return new DeskAvailabilityResponse()
                {
                    idDesk = booking.idDesk,
                    name = booking.desk.name,
                    status = message,
                    idUser = "occupied by your colleague"
                };
            }

            
        }

        public async Task<CreateBookingResponse> CreateBooking(CreateBookingRequest request, int idUser)
        {
            if ((request.endDate - request.startDate).TotalDays > 7) throw new Exception("More than a week");

            var desks = await _context.Bookings.
                Where(e => e.startDate >= request.startDate && e.endDate <= request.endDate).
                Select(e => new SearchDesksByDateResponse
                {
                    idDesk = e.idDesk,
                    name = e.desk.name,
                    startDate = e.startDate,
                    endDate = e.endDate
                }).OrderBy(e => e.name).ToListAsync();

            var desk = desks.FirstOrDefault(e => e.idDesk == request.idDesk);
            if (desk != null) throw new Exception("Occupied desk");
            
        var booking = new Booking()
            {
                startDate = request.startDate,
                endDate = request.endDate,
                idDesk = request.idDesk,
                idUser = idUser

            };
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();

            return new CreateBookingResponse()
            {
                idBooking = booking.idBooking,
                startDate = booking.startDate,
                endDate= booking.endDate,
                idDesk = booking.idDesk,
                idUser= booking.idUser
            };
        }

        

        public async Task<List<SearchDesksByDateResponse>> SearchFreeDesks(DateTime startDate, DateTime endDate, int idLocation) //search for free desks between given dates at given location
        {
            var desks = await _context.Bookings.
                Where(e => e.endDate < startDate || e.startDate > endDate && e.desk.Location.idLocation == idLocation).
                Select(e => new SearchDesksByDateResponse
                {
                    idDesk = e.idDesk,
                    name = e.desk.name,
                    startDate = e.startDate,
                    endDate = e.endDate
                }).OrderBy(e => e.name).ToListAsync();
            if (desks.Count == 0) throw new Exception("Not found");
            return desks;
        }

        public async Task<List<SearchDesksByDateResponse>> SearchOccupiedDesks(DateTime startDate, DateTime endDate, int idLocation) //search for occupied desks between given dates at given location
        {
            var desks = await _context.Bookings.
                Where(e => e.startDate >= startDate && e.endDate <= endDate && e.desk.Location.idLocation == idLocation).
                Select(e => new SearchDesksByDateResponse
            {
                idDesk = e.idDesk,
                name = e.desk.name,
                startDate = e.startDate,
                endDate = e.endDate
            }).OrderBy(e => e.name).ToListAsync() ;
            if (desks.Count == 0) throw new Exception("Not found");
            return desks ;
        }
    }
}
