using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class CampingCategory
    {
        public CampingCategory()
        {
            CampingGears = new HashSet<CampingGear>();
        }

        public int GearCategoryId { get; set; }
        public string GearCategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<CampingGear> CampingGears { get; set; }
    }
}
