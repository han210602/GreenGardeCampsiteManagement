using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class ComboDTO
    {
        public int ComboId { get; set; }
        public string ComboName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImgUrl { get; set; }
    }
    public class ComboDetail
    {
        public int ComboId { get; set; }
        public string ComboName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImgUrl { get; set; }

        public virtual List<ComboCampingGearDetailDTO> ComboCampingGearDetails { get; set; }
        public virtual List<ComboFootDetailDTO> ComboFootDetails { get; set; }
        public virtual List<ComboTicketDetailDTO> ComboTicketDetails { get; set; }
    }
}
