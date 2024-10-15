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

        public static List<CampingGearDTO> GetCampingGearsBySort(int? categoryId, int? sortBy)
        {
            var query = context.CampingGears
                .Include(gear => gear.GearCategory)
                .AsQueryable();

            // Lọc theo danh mục nếu categoryId được cung cấp
            if (categoryId.HasValue)
            {
                query = query.Where(gear => gear.GearCategoryId == categoryId.Value);
            }

            // Kiểm tra và sắp xếp theo tiêu chí được chỉ định
            if (sortBy.HasValue)
            {
                switch (sortBy.Value)
                {
                    case 1: // Sắp xếp theo giá từ thấp đến cao
                        query = query.OrderBy(gear => gear.RentalPrice);
                        break;
                    case 2: // Sắp xếp theo giá từ cao đến thấp
                        query = query.OrderByDescending(gear => gear.RentalPrice);
                        break;
                    case 3: // Sắp xếp theo ngày tạo mới nhất
                        query = query.OrderByDescending(gear => gear.CreatedAt);
                        break;
                    case 4: // Sắp xếp theo độ phổ biến
                        query = query.OrderByDescending(gear => gear.QuantityAvailable); // hoặc một tiêu chí khác
                        break;
                    default:
                        // Không thực hiện sắp xếp nếu sortBy không hợp lệ
                        break;
                }
            }
            else
            {
                // Sắp xếp mặc định nếu sortBy là null
                query = query; 
            }

            // Chọn các thuộc tính cần thiết
            var campingGears = query.Select(gear => new CampingGearDTO
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


    }

}

