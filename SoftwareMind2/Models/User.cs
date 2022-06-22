using System;
using System.Collections.Generic;

namespace SoftwareMind.Models
{
    public class User
    {
        public int idUser { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string Role { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

    }
}
