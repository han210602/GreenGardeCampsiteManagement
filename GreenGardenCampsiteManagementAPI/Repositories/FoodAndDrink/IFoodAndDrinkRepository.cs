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

        List<FoodAndDrinkDTO> GetFADByCategoryId(int categoryId);
        void AddFoodOrDrink(AddFoodOrDrinkDTO item);
        void UpdateFoodOrDrink(UpdateFoodOrDrinkDTO item);
    }
}
