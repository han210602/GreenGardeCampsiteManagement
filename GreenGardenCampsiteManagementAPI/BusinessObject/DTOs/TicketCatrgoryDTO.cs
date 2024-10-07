using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class TicketCatrgoryDTO
    {
        public int TicketCategoryId { get; set; }
        public string TicketCategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
