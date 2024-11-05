using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class ComboFootDetail
    {
        public int ComboId { get; set; }
        public int ItemId { get; set; }
        public int? Quantity { get; set; }
        public string? Description { get; set; }

        public virtual Combo Combo { get; set; } = null!;
        public virtual FoodAndDrink Item { get; set; } = null!;
    }
}
