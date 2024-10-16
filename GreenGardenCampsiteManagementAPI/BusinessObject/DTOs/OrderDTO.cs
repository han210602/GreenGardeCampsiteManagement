using BusinessObject.Models;
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
        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? CustomerName { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? OrderUsageDate { get; set; }
        public decimal Deposit { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountPayable { get; set; }
        public bool? StatusOrder { get; set; }
        public int? ActivityId { get; set; }
        public string? ActivityName { get; set; }
        public string? PhoneCustomer { get; set; }

    }
    public class OrderAddDTO
    {


        public int? EmployeeId { get; set; }
        public string? CustomerName { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? OrderUsageDate { get; set; }
        public decimal Deposit { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountPayable { get; set; }
        public bool? StatusOrder { get; set; }
        public int? ActivityId { get; set; }
        public string? PhoneCustomer { get; set; }

    }
    public class OrderDetailDTO
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
        public virtual ICollection<OrderCampingGearDetailDTO> OrderCampingGearDetails { get; set; }
        public virtual ICollection<OrderComboDetailDTO> OrderComboDetails { get; set; }
        public virtual ICollection<OrderFoodComboDetailDTO> OrderFoodComboDetails { get; set; }
        public virtual ICollection<OrderFoodDetailDTO> OrderFoodDetails { get; set; }
        public virtual ICollection<OrderTicketDetailDTO> OrderTicketDetails { get; set; }
    }
}
