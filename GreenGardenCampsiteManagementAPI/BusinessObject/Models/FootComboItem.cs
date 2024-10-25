using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class FootComboItem
    {
        public int ItemId { get; set; }
        public int ComboId { get; set; }
        public int? Quantity { get; set; }

        public virtual FoodCombo Combo { get; set; } = null!;
        public virtual FoodAndDrink Item { get; set; } = null!;
    }
}
