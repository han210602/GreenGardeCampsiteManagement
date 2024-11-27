namespace BusinessObject.DTOs
{
    public class OrderFoodDetailDTO
    {
        public int ItemId { get; set; }
        public int OrderId { get; set; }

        public string Name { get; set; }

        public string? ImgUrl { get; set; }
        public int? Quantity { get; set; }
        public decimal Price { get; set; }

        public string? Description { get; set; }

    }
    public class OrderFoodAddDTO
    {
        public int ItemId { get; set; }
        public int OrderId { get; set; }


        public int? Quantity { get; set; }
        public string? Description { get; set; }


    }
}
