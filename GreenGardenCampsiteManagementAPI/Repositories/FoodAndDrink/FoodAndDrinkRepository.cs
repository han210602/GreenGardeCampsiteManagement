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
        public List<FoodAndDrinkDTO> GetAllCustomerFoodAndDrink()
        {
            return FoodAndDrinkDAO.GetAllCustomerFoodAndDrink();
        }
        public FoodAndDrinkDTO GetFoodAndDrinkDetail(int itemId)
        {
            return FoodAndDrinkDAO.GetFoodAndDrinkDetail(itemId);
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
        public (List<FoodAndDrinkDTO> FoodAndDrinks, int TotalPages) GetFoodAndDrinks(int? categoryId, int? sortBy, int? priceRange, int page, int pageSize)
        {
            return FoodAndDrinkDAO.GetFoodAndDrinks(categoryId, sortBy, priceRange, page, pageSize);
        }


        public void ChangeFoodStatus(int itemId)
        {
            FoodAndDrinkDAO.ChangeFoodStatus(itemId); // Call DAO method
        }

    }
}
