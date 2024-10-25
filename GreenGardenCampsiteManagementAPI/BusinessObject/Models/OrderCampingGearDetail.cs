using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class OrderCampingGearDetail
    {
        public int GearId { get; set; }
        public int OrderId { get; set; }
        public int? Quantity { get; set; }
        public string? Description { get; set; }

        public virtual CampingGear Gear { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
    public  class OrderCampingGearByUsageDateDTO
    {
        public int GearId { get; set; }
        public int? Quantity { get; set; }
    }
}
