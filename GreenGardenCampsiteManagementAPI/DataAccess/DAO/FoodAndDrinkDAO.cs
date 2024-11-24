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
    public class FoodAndDrinkDAO
    {
        private static GreenGardenContext context = new GreenGardenContext();

        public static List<FoodAndDrinkDTO> GetAllFoodAndDrink()
        {
            try
            {
                var items = context.FoodAndDrinks
                .Include(x => x.Category)
                .Select(item => new FoodAndDrinkDTO
                {
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    Price = item.Price,
                    Description = item.Description,
                    CategoryName = item.Category.CategoryName, // Lấy tên từ danh mục
                    ImgUrl = item.ImgUrl,
                    Status = item.Status,
                    CategoryId = item.Category.CategoryId
                }).ToList();

                return items;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static List<FoodAndDrinkDTO> GetAllCustomerFoodAndDrink()
        {
            try
            {
                var items = context.FoodAndDrinks
               .Include(x => x.Category).Where(s => s.Status == true)
               .Select(item => new FoodAndDrinkDTO
               {
                   ItemId = item.ItemId,
                   ItemName = item.ItemName,
                   Price = item.Price,
                   Description = item.Description,
                   CategoryName = item.Category.CategoryName, // Lấy tên từ danh mục
                   ImgUrl = item.ImgUrl
               }).ToList();

                return items;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static FoodAndDrinkDTO GetFoodAndDrinkDetail(int itemId)
        {
            try
            {
                var item = context.FoodAndDrinks
              .Include(x => x.Category)
              .Where(i => i.ItemId == itemId) // Filter by ItemId
              .Select(i => new FoodAndDrinkDTO
              {
                  ItemId = i.ItemId,
                  ItemName = i.ItemName,
                  Price = i.Price,
                  Description = i.Description,
                  CategoryName = i.Category.CategoryName,
                  ImgUrl = i.ImgUrl,
                  Status = i.Status,
                  CategoryId = i.Category.CategoryId
              })
              .FirstOrDefault(); // Return the first match or null if not found

                return item;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public static void AddFoodAndDrink(AddFoodOrDrinkDTO item)
        {
            try
            {
                var foodAndDrink = new FoodAndDrink
                {
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    Price = item.Price,
                    CreatedAt = DateTime.Now,
                    Description = item.Description,
                    ImgUrl = item.ImgUrl,
                    CategoryId = item.CategoryId,
                    Status = true

                };

                context.FoodAndDrinks.Add(foodAndDrink);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static void UpdateFoodOrDrink(UpdateFoodOrDrinkDTO itemDto)
        {
            try
            {
                var foodAndDrink = context.FoodAndDrinks.FirstOrDefault(f => f.ItemId == itemDto.ItemId);

                if (foodAndDrink == null)
                {
                    throw new Exception($"Food and Drink with ID {itemDto.ItemId} does not exist.");
                }

                foodAndDrink.ItemName = itemDto.ItemName;
                foodAndDrink.Price = itemDto.Price;
                foodAndDrink.Description = itemDto.Description;
                foodAndDrink.CategoryId = itemDto.CategoryId; // Cập nhật danh mục
                foodAndDrink.ImgUrl = itemDto.ImgUrl;

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static (List<FoodAndDrinkDTO> foodAndDrinks, int totalPages) GetFoodAndDrinks(int? categoryId, int? sortBy, int? priceRange, int page = 1, int pageSize = 6)
        {
            try
            {
                var query = context.FoodAndDrinks
                    .Include(food => food.Category)
                    .Where(s => s.Status == true) // Bao gồm thông tin danh mục
                    .AsNoTracking() // Không theo dõi thực thể để cải thiện hiệu suất
                    .AsQueryable();

                // Lọc theo danh mục nếu categoryId được cung cấp
                if (categoryId.HasValue)
                {
                    query = query.Where(food => food.CategoryId == categoryId.Value);
                }

                // Lọc theo khoảng giá
                if (priceRange.HasValue)
                {
                    switch (priceRange.Value)
                    {
                        case 1: // Dưới 300,000
                            query = query.Where(food => food.Price < 300000);
                            break;
                        case 2: // 300,000 - 500,000
                            query = query.Where(food => food.Price >= 300000 && food.Price <= 500000);
                            break;
                        case 3: // Trên 500,000
                            query = query.Where(food => food.Price > 500000);
                            break;
                    }
                }

                // Sắp xếp nếu có tiêu chí sắp xếp
                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 1: // Giá tăng dần
                            query = query.OrderBy(food => food.Price);
                            break;
                        case 2: // Giá giảm dần
                            query = query.OrderByDescending(food => food.Price);
                            break;
                        case 3: // Theo tên món ăn (tăng dần)
                            query = query.OrderBy(food => food.ItemName);
                            break;
                        case 4: // Theo tên món ăn (giảm dần)
                            query = query.OrderByDescending(food => food.ItemName);
                            break;
                    }
                }

                // Phân trang
                var totalItems = query.Count(); // Tổng số sản phẩm
                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize); // Tổng số trang
                var skip = (page - 1) * pageSize;

                query = query.Skip(skip).Take(pageSize);

                // Chuyển đổi sang DTO và lấy các thuộc tính cần thiết
                var foodAndDrinks = query.Select(food => new FoodAndDrinkDTO
                {
                    ItemId = food.ItemId,
                    ItemName = food.ItemName,
                    Price = food.Price,
                    Description = food.Description,
                    CategoryName = food.Category.CategoryName,
                    ImgUrl = food.ImgUrl
                }).ToList();

                // Trả về danh sách món ăn và tổng số trang
                return (foodAndDrinks, totalPages);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<FoodAndDrinkCategoryDTO> GetAllFoodAndDrinkCategories()
        {
            try
            {
                var items = context.FoodAndDrinkCategories

               .Select(item => new FoodAndDrinkCategoryDTO
               {
                   CategoryId = item.CategoryId,
                   CategoryName = item.CategoryName,
                   Description = item.Description,
                   CreatedAt = item.CreatedAt,

               }).ToList();

                return items;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void ChangeFoodStatus(int itemId)
        {
            // Tìm thiết bị dựa trên GearId
            try
            {
                var campingGear = context.FoodAndDrinks.FirstOrDefault(g => g.ItemId == itemId);

                // Kiểm tra xem thiết bị có tồn tại không
                if (campingGear == null)
                {
                    throw new Exception($"Food and Drink with ID {itemId} does not exist.");
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
    }
}
