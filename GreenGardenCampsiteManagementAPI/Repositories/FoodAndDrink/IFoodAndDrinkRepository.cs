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

        public (List<FoodAndDrinkDTO> FoodAndDrinks, int TotalPages) GetFoodAndDrinks(int? categoryId, int? sortBy, int? priceRange, int page, int pageSize);



        void AddFoodOrDrink(AddFoodOrDrinkDTO item);
        void UpdateFoodOrDrink(UpdateFoodOrDrinkDTO item);
        void ChangeFoodStatus(int itemId);
    }
}
