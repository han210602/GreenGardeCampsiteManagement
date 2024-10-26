namespace GreenGardenClient.Models
{
    public class TicketVM
    {
        public int TicketId { get; set; }
        public string TicketName { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ImgUrl { get; set; }
        public int Quantity { get; set; }
       public string TicketCategoryName { get; set; }
    }
    public class UpdateTicketDTO
    {
        public int TicketId { get; set; }
        public int OrderId { get; set; }
        public int? Quantity { get; set; }
    }
}
