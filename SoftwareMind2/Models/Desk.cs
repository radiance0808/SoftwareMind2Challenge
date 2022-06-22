using System.Collections.Generic;

namespace SoftwareMind.Models
{
    public class Desk
    {
        public int idDesk { get; set; }

        public int idLocation { get; set; }

        public string name { get; set; }

        public bool availability { get; set; }


        public Location Location { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
