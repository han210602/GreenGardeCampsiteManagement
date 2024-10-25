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
        public static CampingGearDTO GetCampingGearDetail(int gearId)
        {
            var campingGear = context.CampingGears
                .Include(gear => gear.GearCategory)
                .Where(gear => gear.GearId == gearId) // Filter by GearId
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
                })
                .FirstOrDefault(); // Return the first match or null if not found

            return campingGear;
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

        public static List<CampingGearDTO> GetCampingGears(int? categoryId, int? sortBy, int? priceRange, int? popularity)
        {
            var query = context.CampingGears
                .Include(gear => gear.GearCategory)
                .AsNoTracking() // Không theo dõi thực thể để cải thiện hiệu suất
                .AsQueryable();

            // Lọc theo danh mục nếu categoryId được cung cấp
            if (categoryId.HasValue)
            {
                query = query.Where(gear => gear.GearCategoryId == categoryId.Value);
            }

            // Lọc theo khoảng giá
            if (priceRange.HasValue)
            {
                switch (priceRange.Value)
                {
                    case 1: // Dưới 100,000
                        query = query.Where(gear => gear.RentalPrice < 100000);
                        break;
                    case 2: // 100,000 - 300,000
                        query = query.Where(gear => gear.RentalPrice >= 100000 && gear.RentalPrice <= 300000);
                        break;
                    case 3: // Trên 300,000
                        query = query.Where(gear => gear.RentalPrice > 300000);
                        break;
                }
            }

            // Áp dụng sắp xếp nếu có tiêu chí sắp xếp hoặc độ phổ biến
            bool isSorted = false;

            // Sắp xếp theo độ phổ biến
            if (popularity.HasValue && !sortBy.HasValue) // Ưu tiên `sortBy` nếu cả hai đều được yêu cầu
            {
                switch (popularity.Value)
                {
                    case 1: // Phổ biến nhất (số lượng có sẵn nhiều nhất)
                        query = query.OrderByDescending(gear => gear.QuantityAvailable);
                        isSorted = true;
                        break;
                    case 2: // Mới nhất (theo ngày tạo)
                        query = query.OrderByDescending(gear => gear.CreatedAt);
                        isSorted = true;
                        break;
                }
            }

            // Sắp xếp theo tiêu chí sortBy
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
                    case 3: // Sắp xếp theo tên từ A-Z
                        query = query.OrderBy(gear => gear.GearName);
                        break;
                    case 4: // Sắp xếp theo tên từ Z-A
                        query = query.OrderByDescending(gear => gear.GearName);
                        break;
                }
                isSorted = true;
            }

            // Sắp xếp mặc định (nếu không có tiêu chí sắp xếp nào)
            if (!isSorted)
            {
                query = query; // Mặc định sắp xếp theo tên
            }

            // Chọn các thuộc tính cần thiết và chuyển đổi sang DTO
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

