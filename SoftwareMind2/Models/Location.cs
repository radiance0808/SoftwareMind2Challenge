using System.Collections.Generic;

namespace SoftwareMind.Models
{
    public class Location
    {
        public int idLocation { get; set; }

        public string city { get; set; }
        public string street { get; set; }

        public virtual ICollection<Desk> Desks { get; set; }
    }
}
