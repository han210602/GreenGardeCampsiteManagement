namespace GreenGardenCampsiteClient.Models
{
    public class GearVM
    {
        public int GearId { get; set; }
        public string GearName { get; set; } = null!;
        public int QuantityAvailable { get; set; }
        public decimal RentalPrice { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string GearCategoryName { get; set; }
        public string? ImgUrl { get; set; }
        public int Quantity { get; set; }
    }
}
