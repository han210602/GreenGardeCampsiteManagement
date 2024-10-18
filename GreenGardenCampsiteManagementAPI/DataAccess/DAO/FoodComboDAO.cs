using BusinessObject.DTOs;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class FoodComboDAO
    {
        public static List<ComboFoodDTO> getAllComboFoods()
        {
            var listProducts = new List<ComboFoodDTO>();
            try
            {
                using (var context = new GreenGardenContext())
                {
                    listProducts = context.FoodCombos.Select(f =>new ComboFoodDTO
                    {
                        ComboId = f.ComboId,
                        ComboName = f.ComboName,
                        Price = f.Price,
                        ImgUrl = f.ImgUrl,
                    }).ToList();    
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listProducts;
        }


    }
}
