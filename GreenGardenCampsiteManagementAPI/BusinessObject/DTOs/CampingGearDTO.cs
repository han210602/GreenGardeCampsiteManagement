using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class CampingGearDTO
    {
        public int GearId { get; set; }
        public string GearName { get; set; } = null!;
        public int QuantityAvailable { get; set; }
        public decimal RentalPrice { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string GearCategoryName { get; set; }
        public string? ImgUrl { get; set; }
    }
    public class AddCampingGearDTO
    {
        public int GearId { get; set; }
        public string GearName { get; set; } = null!;
        public int QuantityAvailable { get; set; }
        public decimal RentalPrice { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? GearCategoryId { get; set; }
        public string? ImgUrl { get; set; }
        public bool? Status { get; set; }

    }
    public class UpdateCampingGearDTO
    {
        public int GearId { get; set; }
        public string GearName { get; set; } = null!;
        public int QuantityAvailable { get; set; }
        public decimal RentalPrice { get; set; }
        public string? Description { get; set; }
        public int? GearCategoryId { get; set; }
        public string? ImgUrl { get; set; }
    }
    public class ChangeGearStatus
    {
        public bool? Status { get; set; }
    }
}
