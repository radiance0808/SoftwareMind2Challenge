namespace SoftwareMind2.DTOs.DeskDTO
{
    public class SearchDesksByLocationResponse
    {
        public int idDesk { get; set; }

        public int idLocation { get; set; }

        public string name { get; set; }

        public bool availability { get; set; }
    }
}
