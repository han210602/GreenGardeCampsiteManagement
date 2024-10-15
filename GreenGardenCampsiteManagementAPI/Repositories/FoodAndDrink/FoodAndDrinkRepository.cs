using BusinessObject.DTOs;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.FoodAndDrink
{
    public class FoodAndDrinkRepository : IFoodAndDrinkRepository
    {
        public List<FoodAndDrinkDTO> GetAllFoodAndDrink()
        {
            return FoodAndDrinkDAO.GetAllFoodAndDrink();
        }
       
        public void AddFoodOrDrink(AddFoodOrDrinkDTO item)
        {
            FoodAndDrinkDAO.AddFoodAndDrink(item);
        }

        public void UpdateFoodOrDrink(UpdateFoodOrDrinkDTO item)
        {
            FoodAndDrinkDAO.UpdateFoodOrDrink(item); // Gọi phương thức trong DAO
        }

        public List<FoodAndDrinkCategoryDTO> GetAllFoodAndDrinkCategories()
        {
            return FoodAndDrinkDAO.GetAllFoodAndDrinkCategories();

        }
        public List<FoodAndDrinkDTO> GetFoodAndDrinksBySort(int? categoryId, int? sortBy)
        {
            return FoodAndDrinkDAO.GetFoodAndDrinksBySort(categoryId, sortBy);
        }
    }
}
