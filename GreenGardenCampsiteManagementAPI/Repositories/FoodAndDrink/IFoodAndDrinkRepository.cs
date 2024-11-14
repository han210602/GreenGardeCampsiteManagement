using BusinessObject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.FoodAndDrink
{
    public interface IFoodAndDrinkRepository
    {
        List<FoodAndDrinkDTO> GetAllFoodAndDrink();
        List<FoodAndDrinkDTO> GetAllCustomerFoodAndDrink();
        FoodAndDrinkDTO GetFoodAndDrinkDetail(int itemId);
        List<FoodAndDrinkCategoryDTO> GetAllFoodAndDrinkCategories();

        List<FoodAndDrinkDTO> GetFoodAndDrinks(int? categoryId, int? sortBy, int? priceRange);

        void AddFoodOrDrink(AddFoodOrDrinkDTO item);
        void UpdateFoodOrDrink(UpdateFoodOrDrinkDTO item);
        void ChangeFoodStatus(int itemId, ChangeFoodStatus newStatus);
    }
}
