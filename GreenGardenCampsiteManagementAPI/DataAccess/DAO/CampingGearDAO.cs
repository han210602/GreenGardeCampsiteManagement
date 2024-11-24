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
            try
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
                   GearCategoryId = gear.GearCategory.GearCategoryId,
                   GearCategoryName = gear.GearCategory.GearCategoryName,
                   ImgUrl = gear.ImgUrl,
                   Status = gear.Status,
               }).ToList();
                return campingGears;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static List<CampingGearDTO> GetAllCustomerCampingGears()
        {
            try
            {
                var campingGears = context.CampingGears
                .Include(gear => gear.GearCategory)
                .Where(s => s.Status == true)
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static CampingGearDTO GetCampingGearDetail(int gearId)
        {
            try
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
                   GearCategoryId = gear.GearCategory.GearCategoryId,
                   GearCategoryName = gear.GearCategory.GearCategoryName,
                   ImgUrl = gear.ImgUrl,
                   Status = gear.Status,
               })
               .FirstOrDefault(); // Return the first match or null if not found

                return campingGear;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void ChangeGearStatus(int gearId)
        {
            // Tìm thiết bị dựa trên GearId
            try
            {
                var campingGear = context.CampingGears.FirstOrDefault(g => g.GearId == gearId);

                // Kiểm tra xem thiết bị có tồn tại không
                if (campingGear == null)
                {
                    throw new Exception($"Camping gear with ID {gearId} does not exist.");
                }

                // Đổi trạng thái (nếu đang là true thì chuyển sang false, ngược lại)
                campingGear.Status = !campingGear.Status;

                // Lưu thay đổi vào cơ sở dữ liệu
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static void AddCampingGear(AddCampingGearDTO gearDto)
        {
            try
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
                    Status = true

                };
                context.CampingGears.Add(campingGear);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateCampingGear(UpdateCampingGearDTO gearDto)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static List<CampingCategoryDTO> GetAllCampingGearCategories()
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static (List<CampingGearDTO> campingGears, int totalPages) GetCampingGears(
      int? categoryId,
      int? sortBy,
      int? priceRange,
      int page = 1,
      int pageSize = 6)
        {
            try
            {
                // Khởi tạo truy vấn cho bảng CampingGears
                var query = context.CampingGears
                    .Include(gear => gear.GearCategory)  // Bao gồm thông tin danh mục thiết bị
                    .Where(s => s.Status == true) // Lọc các thiết bị có trạng thái active
                    .AsNoTracking() // Không theo dõi thực thể để cải thiện hiệu suất
                    .AsQueryable();

                // Lọc theo danh mục (nếu có)
                if (categoryId.HasValue)
                {
                    query = query.Where(gear => gear.GearCategoryId == categoryId.Value);
                }

                // Lọc theo khoảng giá (nếu có)
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


                // Sắp xếp theo tiêu chí sortBy nếu có
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
                        case 5: // Phổ biến nhất (số lượng có sẵn nhiều nhất)
                            query = query.OrderByDescending(gear => gear.QuantityAvailable);
                            break;
                        case 6: // Mới nhất (theo ngày tạo)
                            query = query.OrderByDescending(gear => gear.CreatedAt);
                            break;
                    }
                }

                // Tính toán tổng số thiết bị và tổng số trang
                var totalItems = query.Count(); // Tổng số thiết bị
                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize); // Tính số trang

                // Phân trang: Lấy các thiết bị trong phạm vi của trang hiện tại
                query = query.Skip((page - 1) * pageSize).Take(pageSize);

                // Chuyển đổi thành DTO và lấy các thuộc tính cần thiết
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

                return (campingGears, totalPages);
            }
            catch (Exception ex)
            {
                throw new Exception("Đã xảy ra lỗi khi lấy dữ liệu thiết bị cắm trại: " + ex.Message);
            }
        }


    }

}

