using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderCampingGearDetails = new HashSet<OrderCampingGearDetail>();
            OrderComboDetails = new HashSet<OrderComboDetail>();
            OrderFoodComboDetails = new HashSet<OrderFoodComboDetail>();
            OrderFoodDetails = new HashSet<OrderFoodDetail>();
            OrderTicketDetails = new HashSet<OrderTicketDetail>();
        }

        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public string? CustomerName { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? OrderUsageDate { get; set; }
        public decimal Deposit { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountPayable { get; set; }
        public bool? StatusOrder { get; set; }
        public int? ActivityId { get; set; }

        public virtual Activity? Activity { get; set; }
        public virtual User? Customer { get; set; }
        public virtual User? Employee { get; set; }
        public virtual ICollection<OrderCampingGearDetail> OrderCampingGearDetails { get; set; }
        public virtual ICollection<OrderComboDetail> OrderComboDetails { get; set; }
        public virtual ICollection<OrderFoodComboDetail> OrderFoodComboDetails { get; set; }
        public virtual ICollection<OrderFoodDetail> OrderFoodDetails { get; set; }
        public virtual ICollection<OrderTicketDetail> OrderTicketDetails { get; set; }
    }
}
