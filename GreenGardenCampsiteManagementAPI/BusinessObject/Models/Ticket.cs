using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            ComboTicketDetails = new HashSet<ComboTicketDetail>();
            OrderTicketDetails = new HashSet<OrderTicketDetail>();
        }

        public int TicketId { get; set; }
        public string TicketName { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? TicketCategoryId { get; set; }
        public string? ImgUrl { get; set; }

        public virtual TicketCategory? TicketCategory { get; set; }
        public virtual ICollection<ComboTicketDetail> ComboTicketDetails { get; set; }
        public virtual ICollection<OrderTicketDetail> OrderTicketDetails { get; set; }
    }
}
