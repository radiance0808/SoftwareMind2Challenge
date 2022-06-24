using SoftwareMind2.DTOs.BookingDTO;
using SoftwareMind2.DTOs.DeskDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftwareMind2.Services.BookingService
{
    public interface IBookingService
    {
        Task<List<SearchDesksByDateResponse>> SearchOccupiedDesks(DateTime startDate, DateTime endDate, int idLocation);
        Task<List<SearchDesksByDateResponse>> SearchFreeDesks(DateTime startDate, DateTime endDate, int idLocation);

        Task<CreateBookingResponse> CreateBooking(CreateBookingRequest request, int idUser);

        Task ChangeDesk(int idBooking, int newDesk);

        Task<DeskAvailabilityResponse> CheckDeskAvailability(int idDesk, DateTime startDate, DateTime endDate, string Role);
    }
}
