using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
   public class CreateComboOrderRequest
    {
        public OrderAddDTO Order { get; set; }
        public List<OrderComboAddDTO> OrderCombo { get; set; }
        public List<OrderCampingGearAddDTO> OrderCampingGear { get; set; }
        public List<OrderFoodAddDTO> OrderFood { get; set; }
        public List<OrderFoodComboAddDTO> OrderFoodCombo { get; set; }
    }
    public class CheckoutCombo
    {
        public CustomerOrderAddDTO Order { get; set; }
        public List<OrderComboAddDTO> OrderCombo { get; set; }
        public List<OrderCampingGearAddDTO> OrderCampingGear { get; set; }
        public List<OrderFoodAddDTO> OrderFood { get; set; }
        public List<OrderFoodComboAddDTO> OrderFoodCombo { get; set; }
    }
}
