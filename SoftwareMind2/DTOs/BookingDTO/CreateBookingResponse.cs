using System;

namespace SoftwareMind2.DTOs.BookingDTO
{
    public class CreateBookingResponse
    {
        public int idBooking { get; set; }

        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public int idDesk { get; set; }

        public int idUser { get; set; }
    }
}
