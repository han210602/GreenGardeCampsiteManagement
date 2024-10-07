using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class FoodAndDrinkCategory
    {
        public FoodAndDrinkCategory()
        {
            FoodAndDrinks = new HashSet<FoodAndDrink>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<FoodAndDrink> FoodAndDrinks { get; set; }
    }
}
