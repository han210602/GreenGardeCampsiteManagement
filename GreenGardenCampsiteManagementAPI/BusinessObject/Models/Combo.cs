using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Combo
    {
        public Combo()
        {
            ComboCampingGearDetails = new HashSet<ComboCampingGearDetail>();
            ComboFootDetails = new HashSet<ComboFootDetail>();
            ComboTicketDetails = new HashSet<ComboTicketDetail>();
            OrderComboDetails = new HashSet<OrderComboDetail>();
        }

        public int ComboId { get; set; }
        public string ComboName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? ImgUrl { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<ComboCampingGearDetail> ComboCampingGearDetails { get; set; }
        public virtual ICollection<ComboFootDetail> ComboFootDetails { get; set; }
        public virtual ICollection<ComboTicketDetail> ComboTicketDetails { get; set; }
        public virtual ICollection<OrderComboDetail> OrderComboDetails { get; set; }
    }
}
