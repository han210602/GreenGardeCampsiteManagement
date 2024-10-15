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
        List<FoodAndDrinkCategoryDTO> GetAllFoodAndDrinkCategories();

        List<FoodAndDrinkDTO> GetFoodAndDrinksBySort(int? categoryId, int? sortBy);
        void AddFoodOrDrink(AddFoodOrDrinkDTO item);
        void UpdateFoodOrDrink(UpdateFoodOrDrinkDTO item);
    }
}
