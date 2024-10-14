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
        public static List<FoodAndDrinkDTO> GetFADByCategoryId(int categoryId)
        {
            var items = context.FoodAndDrinks
                .Include(x => x.Category)
                .Where(ticket => ticket.CategoryId == categoryId)
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
