using BusinessObject.DTOs;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ComboDAO
    {
        public static List<ComboDTO> GetListCombo()
        {
            var listProducts = new List<ComboDTO>();
            try
            {
                using (var context = new GreenGardenContext())
                {
                    listProducts = context.Combos.Select(f => new ComboDTO
                    {
                        ComboId = f.ComboId,
                        ComboName = f.ComboName,
                        Description = f.Description,
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
