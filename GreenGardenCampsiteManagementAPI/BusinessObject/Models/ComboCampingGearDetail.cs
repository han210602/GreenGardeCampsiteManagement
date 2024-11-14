using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class ComboCampingGearDetail
    {
        public int ComboId { get; set; }
        public int GearId { get; set; }
        public int? Quantity { get; set; }
        public string? Description { get; set; }

        public virtual Combo Combo { get; set; } = null!;
        public virtual CampingGear Gear { get; set; } = null!;
    }
}
