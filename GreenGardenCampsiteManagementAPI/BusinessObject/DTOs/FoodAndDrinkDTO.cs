using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class FoodAndDrinkDTO
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string CategoryName { get; set; }
        public string? ImgUrl { get; set; }
    }
    public class AddFoodOrDrinkDTO
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public string? ImgUrl { get; set; }
    }
    public class UpdateFoodOrDrinkDTO
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public string? ImgUrl { get; set; }
    }

}
