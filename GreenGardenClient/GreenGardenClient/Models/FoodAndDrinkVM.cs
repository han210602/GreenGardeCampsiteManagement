namespace GreenGardenClient.Models
{
    public class FoodAndDrinkVM
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
        public string? Description { get; set; }
        public string CategoryName { get; set; }
        public string? ImgUrl { get; set; }
        public int Quantity { get; set; }
    }
}
