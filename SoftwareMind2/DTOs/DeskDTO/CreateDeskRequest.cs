using System.ComponentModel.DataAnnotations;

namespace SoftwareMind2.DTOs.DeskDTO
{
    public class CreateDeskRequest
    {
        [Required]
        public int idLocation { get; set; }
        [Required]
        public string name { get; set;}
        [Required]
        public bool availability { get; set;}


    }
}
