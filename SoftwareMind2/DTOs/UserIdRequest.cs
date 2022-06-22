using System.ComponentModel.DataAnnotations;

namespace SoftwareMind2.DTOs
{
    public class UserIdRequest
    {
        [Required]
        public int idUser { get; set; }
    }
}
