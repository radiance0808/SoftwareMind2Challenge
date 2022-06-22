using System.ComponentModel.DataAnnotations;

namespace SoftwareMind2.DTOs.LocationDTO
{
    public class CreateLocationRequest
    {
        [Required]
        public string city { get; set; }
        [Required]
        public string street { get; set; }
    }
}
