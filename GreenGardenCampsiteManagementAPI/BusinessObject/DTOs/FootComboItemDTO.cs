using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class FootComboItemDTO
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public int? Quantity { get; set; }
    }
}
