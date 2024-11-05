using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class FoodCombo
    {
        public FoodCombo()
        {
            FootComboItems = new HashSet<FootComboItem>();
            OrderFoodComboDetails = new HashSet<OrderFoodComboDetail>();
        }

        public int ComboId { get; set; }
        public string ComboName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? ImgUrl { get; set; }

        public virtual ICollection<FootComboItem> FootComboItems { get; set; }
        public virtual ICollection<OrderFoodComboDetail> OrderFoodComboDetails { get; set; }
    }
}
