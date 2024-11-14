namespace GreenGardenClient.Models
{
    public class TicketVM
    {
        public int TicketId { get; set; }
        public string TicketName { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ImgUrl { get; set; }
        public int Quantity { get; set; }
        public int TicketCategoryId { get; set; }
        public string TicketCategoryName { get; set; }
        public bool Status { get; set; }
    }
    public class TicketDetailVM
    {
        public int TicketId { get; set; }
        public string TicketName { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ImgUrl { get; set; }
        public int TicketCategoryId { get; set; }
        public string TicketCategoryName { get; set; }
        public bool Status { get; set; }
    }
    public class UpdateTicketVM
    {
        public int TicketId { get; set; }
        public string TicketName { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ImgUrl { get; set; }
        public int TicketCategoryId { get; set; }
    }
    public class AddTicketVM
    {
        public int TicketId { get; set; }
        public string TicketName { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TicketCategoryId { get; set; }
        public string ImgUrl { get; set; }
        public bool Status { get; set; }

    }
    public class UpdateTicketDTO
    {
        public int TicketId { get; set; }
        public int OrderId { get; set; }
        public int? Quantity { get; set; }
    }
}
