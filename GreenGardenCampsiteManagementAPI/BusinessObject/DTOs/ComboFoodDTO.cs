using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class ComboFoodDTO
    {
        public int ComboId { get; set; }
        public string ComboName { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ImgUrl { get; set; }


    }
    public class ComboFoodDetailDTO
    {
        public int ComboId { get; set; }
        public string ComboName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImgUrl { get; set; }

        public  List<FootComboItemDTO> FootComboItems { get; set; }
    }
  
}
