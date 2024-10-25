using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class TicketCategory
    {
        public TicketCategory()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int TicketCategoryId { get; set; }
        public string TicketCategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
