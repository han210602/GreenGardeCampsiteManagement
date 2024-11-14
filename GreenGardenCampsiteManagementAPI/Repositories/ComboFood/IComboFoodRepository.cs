using BusinessObject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ComboFood
{
    public interface IComboFoodRepository
    {
        List<ComboFoodDTO> ComboFoods();
        List<ComboFoodDTO> CustomerComboFoods();
        ComboFoodDetailDTO ComboFoodDetail(int id);
        void ChangeComboFoodStatus(int comboId);
    }
}
