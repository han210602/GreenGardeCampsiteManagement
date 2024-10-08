using BusinessObject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Orders
{
     public interface IOrderRepository
     {
        List<OrderDTO> GetAllOrders();
        bool EnterDeposit(int id,decimal money);
        bool DeleteOrder(int id);
        bool AddOrder(
             OrderDTO order
            ,OrderTicketDetailDTO order_ticket
            ,OrderCampingGearDetailDTO order_camping_gear
            ,OrderFoodDetailDTO order_food
            ,OrderFoodComboDetailDTO order_foot_combo
            ,OrderComboDetailDTO order_combo);
    }
}
