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
        public List<FoodAndDrinkDTO> GetFADByCategoryId(int categoryId)
        {
            return FoodAndDrinkDAO.GetFADByCategoryId(categoryId);
        }
        public void AddFoodOrDrink(AddFoodOrDrinkDTO item)
        {
            FoodAndDrinkDAO.AddFoodAndDrink(item);
        }

        public void UpdateFoodOrDrink(UpdateFoodOrDrinkDTO item)
        {
            FoodAndDrinkDAO.UpdateFoodOrDrink(item); // Gọi phương thức trong DAO
        }
    }
}
