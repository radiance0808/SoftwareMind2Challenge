using Microsoft.EntityFrameworkCore;
using SoftwareMind.Models;

using SoftwareMind2.DTOs.DeskDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareMind2.Services.DeskService
{
    public class DeskService : IDeskService
    {
        private readonly BookingDbContext _context;

        public DeskService(BookingDbContext context)
        {
            _context = context;
        }

        public async Task<CreateDeskResponse> CreateDesk(CreateDeskRequest request) // create desk at certain location
        {
            var desk = new SoftwareMind.Models.Desk()
            {
                idLocation = request.idLocation,
                name = request.name,
                availability = request.availability
                
            };

            await _context.Desks.AddAsync(desk);
            await _context.SaveChangesAsync();

            return new CreateDeskResponse()
            {
                idDesk = desk.idDesk,
                name = desk.name,
                idLocation = desk.idLocation
            };
        }

        public async Task DeleteDesk(int idDesk) // remove desks if it doesnt have any reservations
        {
            var desk = await _context.Desks
            .FirstOrDefaultAsync(e => e.idDesk == idDesk);
            var futureReservationExists = await _context.Bookings.AnyAsync(e => e.idDesk == idDesk);
            if (futureReservationExists) throw new System.Exception("There is a reservation!");
            _context.Desks.Remove(desk);
            await _context.SaveChangesAsync();
        }

        public async Task MakeUnavailable (int idDesk)
        {
            var desk = await _context.Desks.FirstOrDefaultAsync(e => e.idDesk == idDesk);
            if (desk == null) throw new System.Exception("no such desk");
            desk.availability = false;
            
            await _context.SaveChangesAsync();
        } 

        public async Task MakeAvailable (int idDesk) // make the desk available (didnt rly get the point of this tbh)
        {
            var desk = await _context.Desks
            .FirstOrDefaultAsync(e => e.idDesk == idDesk);
            desk.availability = true;
            await _context.SaveChangesAsync();
        }

        public async Task<List<SearchDesksByLocationResponse>> SearchDesksOnLocation(int idLocation)
        {
            var desks = await _context.Desks.
                  Where(e => e.idLocation == idLocation).
                  Select(e => new SearchDesksByLocationResponse
                  {
                      idDesk = e.idDesk,
                      name = e.name,
                      availability = e.availability
                  }).OrderBy(e => e.name).ToListAsync();
            if (desks.Count == 0) throw new Exception("Not found");
            return desks;
        }
    }
    }

