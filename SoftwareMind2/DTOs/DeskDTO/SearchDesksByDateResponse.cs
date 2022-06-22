using System;

namespace SoftwareMind2.DTOs.DeskDTO
{
    public class SearchDesksByDateResponse
    {
        public int idDesk { get; set; }

       

        public string name { get; set; }

        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
