using BusinessObject.DTOs;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public  class CampingCategoryDAO
    {
        //private static GreenGardenContext context = new GreenGardenContext();
        private static GreenGardenContext _context;
        public static void InitializeContext(GreenGardenContext context)
        {
            _context = context;
        }

        // Lấy tất cả trang thiết bị
        public List<CampingGearDTO> GetAllCampingGears()
        {
            return _context.CampingGears.Select(gear => new CampingGearDTO
            {
                GearId = gear.GearId,
                GearName = gear.GearName,
                RentalPrice = gear.RentalPrice,
                ImgUrl = gear.ImgUrl,
                Description = gear.Description,
            }).ToList();
        }

    }
}
