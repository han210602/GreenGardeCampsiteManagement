using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ComboFood
{
    public class ComboFoodRepository : IComboFoodRepository
    {
        public List<ComboFoodDTO> ComboFoods()
        {
            return FoodComboDAO.getAllComboFoods();
        }
    }
}
