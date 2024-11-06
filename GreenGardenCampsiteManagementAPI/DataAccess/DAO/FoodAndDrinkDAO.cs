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
            var items = context.FoodAndDrinks
                .Include(x => x.Category)
                .Select(item => new FoodAndDrinkDTO
                {
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    Price = item.Price,
                    QuantityAvailable = item.QuantityAvailable,
                    Description = item.Description,
                    CategoryName = item.Category.CategoryName, // Lấy tên từ danh mục
                    ImgUrl = item.ImgUrl
                }).ToList();

            return items;
        }
        public static FoodAndDrinkDTO GetFoodAndDrinkDetail(int itemId)
        {
            var item = context.FoodAndDrinks
                .Include(x => x.Category)
                .Where(i => i.ItemId == itemId) // Filter by ItemId
                .Select(i => new FoodAndDrinkDTO
                {
                    ItemId = i.ItemId,
                    ItemName = i.ItemName,
                    Price = i.Price,
                    QuantityAvailable = i.QuantityAvailable,
                    Description = i.Description,
                    CategoryName = i.Category.CategoryName,
                    ImgUrl = i.ImgUrl
                })
                .FirstOrDefault(); // Return the first match or null if not found

            return item;
        }

        public static void AddFoodAndDrink(AddFoodOrDrinkDTO item)
        {
            var foodAndDrink = new FoodAndDrink
            {
                ItemId = item.ItemId,
                ItemName = item.ItemName,
                Price = item.Price,
                QuantityAvailable = item.QuantityAvailable,
                CreatedAt = item.CreatedAt,
                Description = item.Description,
                ImgUrl = item.ImgUrl,
                CategoryId = item.CategoryId // Id của danh mục
            };

            context.FoodAndDrinks.Add(foodAndDrink);
            context.SaveChanges();
        }

        public static void UpdateFoodOrDrink(UpdateFoodOrDrinkDTO itemDto)
        {
            var foodAndDrink = context.FoodAndDrinks.FirstOrDefault(f => f.ItemId == itemDto.ItemId);

            if (foodAndDrink == null)
            {
                throw new Exception($"Food and Drink with ID {itemDto.ItemId} does not exist.");
            }

            foodAndDrink.ItemName = itemDto.ItemName;
            foodAndDrink.Price = itemDto.Price;
            foodAndDrink.QuantityAvailable = itemDto.QuantityAvailable;
            foodAndDrink.Description = itemDto.Description;
            foodAndDrink.CategoryId = itemDto.CategoryId; // Cập nhật danh mục
            foodAndDrink.ImgUrl = itemDto.ImgUrl;

            context.SaveChanges();
        }
        public static List<FoodAndDrinkDTO> GetFoodAndDrinks(int? categoryId, int? sortBy, int? priceRange)
        {
            var query = context.FoodAndDrinks
                .Include(food => food.Category) // Bao gồm thông tin danh mục
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
                    case 1: // Dưới 100,000
                        query = query.Where(food => food.Price < 300000);
                        break;
                    case 2: // 100,000 - 300,000
                        query = query.Where(food => food.Price >= 300000 && food.Price <= 500000);
                        break;
                    case 3: // Trên 300,000
                        query = query.Where(food => food.Price >= 500000);
                        break;
                }
            }

            // Áp dụng sắp xếp nếu có tiêu chí sắp xếp hoặc độ phổ biến
            bool isSorted = false;

            // Sắp xếp theo độ phổ biến

            // Sắp xếp theo tiêu chí sortBy
            if (sortBy.HasValue)
            {
                switch (sortBy.Value)
                {
                    case 1: // Sắp xếp theo giá từ thấp đến cao
                        query = query.OrderBy(food => food.Price);
                        break;
                    case 2: // Sắp xếp theo giá từ cao đến thấp
                        query = query.OrderByDescending(food => food.Price);
                        break;
                    case 3: // Sắp xếp theo ngày tạo mới nhất
                        query = query.OrderByDescending(food => food.CreatedAt);
                        break;
                    case 4: // Sắp xếp theo số lượng có sẵn
                        query = query.OrderByDescending(food => food.QuantityAvailable);
                        break;
                    case 5: // Sắp xếp theo số lượng có sẵn
                        query = query.OrderByDescending(food => food.QuantityAvailable);
                        break;
                    case 6: // Sắp xếp theo số lượng có sẵn
                        query = query.OrderByDescending(food => food.CreatedAt);
                        break;
                }
                isSorted = true;
            }

            // Sắp xếp mặc định (nếu không có tiêu chí sắp xếp nào)
            if (!isSorted)
            {
                query = query; // Mặc định sắp xếp theo tên món ăn
            }

            // Chuyển đổi sang DTO và lấy các thuộc tính cần thiết
            var foodAndDrinks = query.Select(food => new FoodAndDrinkDTO
            {
                ItemId = food.ItemId,
                ItemName = food.ItemName,
                Price = food.Price,
                QuantityAvailable = food.QuantityAvailable,
                Description = food.Description,
                CategoryName = food.Category.CategoryName, // Lấy tên danh mục
                ImgUrl = food.ImgUrl
            }).ToList();

            return foodAndDrinks;
        }

        public static List<FoodAndDrinkCategoryDTO> GetAllFoodAndDrinkCategories()
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
    }
}
