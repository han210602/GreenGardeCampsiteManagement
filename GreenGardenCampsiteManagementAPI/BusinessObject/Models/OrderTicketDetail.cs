using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class OrderTicketDetail
    {
        public int TicketId { get; set; }
        public int OrderId { get; set; }
        public int? Quantity { get; set; }
        public string? Description { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Ticket Ticket { get; set; } = null!;
    }
}
