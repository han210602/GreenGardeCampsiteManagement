﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class OrderCampingGearDetailDTO
    {
        public int GearId { get; set; }
        public int? Quantity { get; set; }
        public string? Description { get; set; }
    }
}