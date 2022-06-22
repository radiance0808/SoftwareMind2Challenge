using Microsoft.EntityFrameworkCore;
using SoftwareMind.Models;
using SoftwareMind2.DTOs.LocationDTO;
using System.Threading.Tasks;

namespace SoftwareMind2.Services.LocationService
{
    public class LocationService : ILocationService
    {
        private readonly BookingDbContext _context;

        public LocationService(BookingDbContext context)
        {
            _context = context;
        }

        public async Task<CreateLocationResponse> CreateLocation(CreateLocationRequest request) // create location
        {
            var location = new SoftwareMind.Models.Location()
            {
                city = request.city,
                street = request.street

            };

            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();

            return new CreateLocationResponse()
            {
                idLocation = location.idLocation,
                city = location.city,
                street = location.street
            };
        }

        public async Task DeleteLocation(int idLocation) // remove location if there are no desks
        {
            var location = await _context.Locations
            .FirstOrDefaultAsync(e => e.idLocation == idLocation);
            var exists = await _context.Desks.AnyAsync(e => e.idLocation == idLocation);
            if (exists) throw new System.Exception("Desks at that location exist!");
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
        }

    }
}
