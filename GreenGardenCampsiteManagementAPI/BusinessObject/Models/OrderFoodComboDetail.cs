using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class OrderFoodComboDetail
    {
        public int ComboId { get; set; }
        public int OrderId { get; set; }
        public int? Quantity { get; set; }
        public string? Description { get; set; }

        public virtual FoodCombo Combo { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
