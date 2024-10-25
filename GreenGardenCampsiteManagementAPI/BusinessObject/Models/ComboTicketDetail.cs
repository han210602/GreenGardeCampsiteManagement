using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class ComboTicketDetail
    {
        public int ComboId { get; set; }
        public int TicketId { get; set; }
        public int? Quantity { get; set; }
        public string? Description { get; set; }

        public virtual Combo Combo { get; set; } = null!;
        public virtual Ticket Ticket { get; set; } = null!;
    }
}
