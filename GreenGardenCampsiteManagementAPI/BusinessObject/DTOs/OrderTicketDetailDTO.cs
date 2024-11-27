namespace BusinessObject.DTOs
{
    public class OrderTicketDetailDTO
    {
        public int TicketId { get; set; }

        public string Name { get; set; }

        public int? Quantity { get; set; }

        public string? ImgUrl { get; set; }
        public decimal Price { get; set; }

        public string? Description { get; set; }
    }
    public class OrderTicketAddlDTO
    {
        public int TicketId { get; set; }
        public int OrderId { get; set; }
        public int? Quantity { get; set; }

    }
}
