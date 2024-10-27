using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class ComboCampingGearDetailDTO
    {
        
            public int ComboId { get; set; }
            public int GearId { get; set; }
            public string Name { get; set; }

            public int? Quantity { get; set; }
         
    }
}
