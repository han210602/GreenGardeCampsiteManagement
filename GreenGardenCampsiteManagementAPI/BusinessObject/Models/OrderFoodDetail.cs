using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class OrderFoodDetail
    {
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public int? Quantity { get; set; }
        public string? Description { get; set; }

        public virtual FoodAndDrink Item { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
