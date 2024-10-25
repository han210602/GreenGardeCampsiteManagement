using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Activity
    {
        public Activity()
        {
            Orders = new HashSet<Order>();
        }

        public int ActivityId { get; set; }
        public string ActivityName { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
