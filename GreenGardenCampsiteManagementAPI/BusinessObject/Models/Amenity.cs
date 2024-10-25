using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Amenity
    {
        public int AmenityId { get; set; }
        public string AmenityName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
