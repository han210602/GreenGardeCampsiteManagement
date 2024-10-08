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
        OrderDetailDTO GetOrderDetail(int id);
        bool EnterDeposit(int id,decimal money);
        bool DeleteOrder(int id);
        bool CreateUniqueOrder(
             OrderDTO order
            ,List<OrderTicketDetailDTO> order_ticket
            , List<OrderCampingGearDetailDTO> order_camping_gear
            , List<OrderFoodDetailDTO> order_food
            , List<OrderFoodComboDetailDTO> order_foot_combo
            );
        bool CreateComboOrder(
             OrderDTO order
            , OrderCampingGearDetailDTO order_camping_gear
            , OrderFoodDetailDTO order_food
            , OrderFoodComboDetailDTO order_foot_combo
            , OrderComboDetailDTO order_combo);
        bool UpdateOrder(
             OrderDTO order
            , OrderTicketDetailDTO order_ticket
            , OrderCampingGearDetailDTO order_camping_gear
            , OrderFoodDetailDTO order_food
            , OrderFoodComboDetailDTO order_foot_combo
            , OrderComboDetailDTO order_combo);
    }
}
