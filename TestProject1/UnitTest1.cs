using Microsoft.EntityFrameworkCore;
using Moq;
using SoftwareMind.Models;
using SoftwareMind2.Controllers;
using SoftwareMind2.DTOs.DeskDTO;
using SoftwareMind2.Services.DeskService;
using SoftwareMind2.Services.LocationService;
using System;
using System.Linq;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        private readonly BookingDbContext dbContext;
        private readonly ILocationService IlocationService;

        public UnitTest1()
        {
            var dbOption = new DbContextOptionsBuilder<BookingDbContext>()
    .UseSqlServer("Server=tcp:softwaremind.database.windows.net,1433;Initial Catalog=SoftwareMindDb;Persist Security Info=False;User ID=radyslavburylko;Password=softwaremind_2022;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
    .Options;

            
            dbContext = new BookingDbContext (dbOption);
            
        }

        [Fact]
        public async void DeleteLocation()
        {
            dbContext.Locations.Add(new Location
            {
                city = "testCity",
                street = "testStreet"
            });
            await dbContext.SaveChangesAsync();

            LocationController locationController = new LocationController(dbContext, IlocationService);

            var service = new LocationService(dbContext);

            var item = dbContext.Locations.Where(i => i.city == "testCity").Select(i => i.idLocation);
            Console.WriteLine(item);

            int deletedItem = item.First();

            await service.DeleteLocation(deletedItem);

            var exists = dbContext.Locations.FirstOrDefaultAsync(e => e.idLocation == deletedItem);
            Assert.True(exists.Result == null);
        }
    }
}
