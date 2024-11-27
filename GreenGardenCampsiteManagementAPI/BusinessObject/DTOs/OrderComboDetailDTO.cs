namespace BusinessObject.DTOs
{
    public class OrderComboDetailDTO
    {
        public int ComboId { get; set; }
        public string Name { get; set; }

        public string? ImgUrl { get; set; }
        public int? Quantity { get; set; }
        public decimal Price { get; set; }

        public string? Description { get; set; }
    }
    public class OrderComboAddDTO
    {
        public int ComboId { get; set; }
        public int OrderId { get; set; }
        public int? Quantity { get; set; }
        public string? Description { get; set; }
    }
}
