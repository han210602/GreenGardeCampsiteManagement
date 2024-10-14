namespace GreenGardenCampsiteClient.Models
{
    public class TicketVM
    {
        public int TicketId { get; set; }
        public string TicketName { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ImgUrl { get; set; }

        public string TicketCategoryName { get; set; }
    }
}
