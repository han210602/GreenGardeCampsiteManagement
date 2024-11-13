using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class CampingGear
    {
        public CampingGear()
        {
            ComboCampingGearDetails = new HashSet<ComboCampingGearDetail>();
            OrderCampingGearDetails = new HashSet<OrderCampingGearDetail>();
        }

        public int GearId { get; set; }
        public string GearName { get; set; } = null!;
        public int QuantityAvailable { get; set; }
        public decimal RentalPrice { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? GearCategoryId { get; set; }
        public string? ImgUrl { get; set; }
        public bool? Status { get; set; }

        public virtual CampingCategory? GearCategory { get; set; }
        public virtual ICollection<ComboCampingGearDetail> ComboCampingGearDetails { get; set; }
        public virtual ICollection<OrderCampingGearDetail> OrderCampingGearDetails { get; set; }
    }
}
