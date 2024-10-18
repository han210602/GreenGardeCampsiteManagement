namespace GreenGardenClient.Models
{
    public class TicketVM
    {
        public int TicketId { get; set; }
        public string TicketName { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ImgUrl { get; set; }
        public int Quantity { get; set; }

        public int CategoryId { get; set; }
        public string TicketCategoryName { get; set; }
    }
}
