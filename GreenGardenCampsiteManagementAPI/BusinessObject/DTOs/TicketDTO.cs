﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class TicketDTO
    {
        public int TicketId { get; set; }
        public string TicketName { get; set; } = null!;
        public decimal Price { get; set; }
        public int TicketCategoryId { get; set; }
        public string TicketCategoryName { get; set; }
        public string? ImgUrl { get; set; }
        public bool? Status { get; set; }
    }
    public class AddTicket
    {
        public int TicketId { get; set; }
        public string TicketName { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? TicketCategoryId { get; set; }
        public string? ImgUrl { get; set; }
        public bool? Status { get; set; }

    }
    public class UpdateTicket
    {
        public int TicketId { get; set; }
        public string TicketName { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ImgUrl { get; set; }
        public int TicketCategoryId { get; set; }
    }
    public class ChangeTicketStatus
    {
        public bool? Status { get; set; }
    }
}
