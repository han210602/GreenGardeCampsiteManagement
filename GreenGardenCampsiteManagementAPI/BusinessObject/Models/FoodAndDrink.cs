using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class FoodAndDrink
    {
        public FoodAndDrink()
        {
            ComboFootDetails = new HashSet<ComboFootDetail>();
            FootComboItems = new HashSet<FootComboItem>();
            OrderFoodDetails = new HashSet<OrderFoodDetail>();
        }

        public int ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CategoryId { get; set; }
        public string? ImgUrl { get; set; }

        public virtual FoodAndDrinkCategory? Category { get; set; }
        public virtual ICollection<ComboFootDetail> ComboFootDetails { get; set; }
        public virtual ICollection<FootComboItem> FootComboItems { get; set; }
        public virtual ICollection<OrderFoodDetail> OrderFoodDetails { get; set; }
    }
}
