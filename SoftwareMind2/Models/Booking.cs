using System;

namespace SoftwareMind.Models
{
    public class Booking
    {
        public int idBooking { get; set; }

        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public int idDesk { get; set; }

        public int idUser { get; set; }

        public User user { get; set; }
        public Desk desk { get; set; }

    }
}
