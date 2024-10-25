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
  
}
