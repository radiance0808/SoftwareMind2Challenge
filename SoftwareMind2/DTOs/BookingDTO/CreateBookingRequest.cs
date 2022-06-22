using System;
using System.ComponentModel.DataAnnotations;

namespace SoftwareMind2.DTOs.BookingDTO
{
    public class CreateBookingRequest
    {
          

        
        [Required]
        public DateTime startDate { get; set; }
        [Required]
        public DateTime endDate { get; set; }
        [Required]
        public int idDesk { get; set; }
        [Required]
        public int idUser { get; set; }

    }
}
