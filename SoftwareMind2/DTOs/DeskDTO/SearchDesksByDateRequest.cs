using System;
using System.ComponentModel.DataAnnotations;

namespace SoftwareMind2.DTOs.DeskDTO
{
    public class SearchDesksByDateRequest
    {
        [Required]
        public DateTime startDate { get; set; }

        [Required]
        public DateTime endDate { get; set; }

        [Required]
        public int idLocation { get; set; }

    }
}
