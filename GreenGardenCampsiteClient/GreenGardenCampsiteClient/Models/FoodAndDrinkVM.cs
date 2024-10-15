namespace GreenGardenCampsiteClient.Models
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
    }
    public class FoodAndDrinkCategoryVM
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
