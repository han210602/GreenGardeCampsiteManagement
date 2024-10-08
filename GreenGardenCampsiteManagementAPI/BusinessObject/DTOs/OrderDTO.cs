using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public string? EmployeeName { get; set; }
        public string? CustomerName { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? OrderUsageDate { get; set; }
        public decimal Deposit { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountPayable { get; set; }
        public bool? StatusOrder { get; set; }
        public string? ActivityId { get; set; }
    }
}
