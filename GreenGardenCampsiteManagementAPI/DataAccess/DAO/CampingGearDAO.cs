using BusinessObject.DTOs;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CampingGearDAO
    {
        private static GreenGardenContext context = new GreenGardenContext();

        // Lấy tất cả trang thiết bị
        public static List<CampingGearDTO> GetAllCampingGears()
        {
            var campingGears = context.CampingGears
                .Include(gear => gear.GearCategory)
                .Select(gear => new CampingGearDTO
                {
                    GearId = gear.GearId,
                    GearName = gear.GearName,
                    QuantityAvailable = gear.QuantityAvailable,
                    RentalPrice = gear.RentalPrice,
                    Description = gear.Description,
                    CreatedAt = gear.CreatedAt,
                    GearCategoryName = gear.GearCategory.GearCategoryName,
                    ImgUrl = gear.ImgUrl
                }).ToList();
            return campingGears;
        }

        public static void AddCampingGear(AddCampingGearDTO gearDto)
        {
            var campingGear = new CampingGear
            {
                GearId = gearDto.GearId,
                GearName = gearDto.GearName,
                QuantityAvailable = gearDto.QuantityAvailable,
                RentalPrice = gearDto.RentalPrice,
                Description = gearDto.Description,
                CreatedAt = DateTime.Now,
                GearCategoryId = gearDto.GearCategoryId,
                ImgUrl = gearDto.ImgUrl,
            };
            context.CampingGears.Add(campingGear);
            context.SaveChanges();
        }

        public static void UpdateCampingGear(UpdateCampingGearDTO gearDto)
        {
            var campingGear = context.CampingGears.FirstOrDefault(g => g.GearId == gearDto.GearId);
            if (campingGear == null)
            {
                throw new Exception($"Camping gear with ID {gearDto.GearId} does not exist.");
            }

            campingGear.GearName = gearDto.GearName;
            campingGear.QuantityAvailable = gearDto.QuantityAvailable;
            campingGear.RentalPrice = gearDto.RentalPrice;
            campingGear.Description = gearDto.Description;
            campingGear.GearCategoryId = gearDto.GearCategoryId;
            campingGear.ImgUrl = gearDto.ImgUrl;
            context.SaveChanges();
        }
        public static List<CampingGearDTO> GetCampingGearsByCategoryId(int categoryId)
        {
            var campingGears = context.CampingGears
                .Include(gear => gear.GearCategory)
                .Where(gear => gear.GearCategoryId == categoryId)
                .Select(gear => new CampingGearDTO
                {
                    GearId = gear.GearId,
                    GearName = gear.GearName,
                    QuantityAvailable = gear.QuantityAvailable,
                    RentalPrice = gear.RentalPrice,
                    Description = gear.Description,
                    CreatedAt = gear.CreatedAt,
                    GearCategoryName = gear.GearCategory.GearCategoryName,
                    ImgUrl = gear.ImgUrl
                }).ToList();
            return campingGears;
        }

        public static List<CampingCategoryDTO> GetAllCampingGearCategories()
        {
            var campingGears = context.CampingCategories

                .Select(gear => new CampingCategoryDTO
                {
                    GearCategoryId = gear.GearCategoryId,
                    GearCategoryName = gear.GearCategoryName,
                    Description = gear.Description,
                    CreatedAt = gear.CreatedAt,
                }).ToList();
            return campingGears;
        }
    }
}
