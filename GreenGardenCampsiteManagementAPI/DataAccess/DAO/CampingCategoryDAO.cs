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
        private static GreenGardenContext context = new GreenGardenContext();

        // Lấy tất cả trang thiết bị
        public List<CampingGearDTO> GetAllCampingGears()
        {
            try { 
            return context.CampingGears.Select(gear => new CampingGearDTO
            {
                GearId = gear.GearId,
                GearName = gear.GearName,
                RentalPrice = gear.RentalPrice,
                ImgUrl = gear.ImgUrl,
                Description = gear.Description,
            }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
