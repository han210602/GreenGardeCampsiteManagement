using BusinessObject.DTOs;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        public bool AddOrder(OrderDTO order
            , OrderTicketDetailDTO order_ticket
            , OrderCampingGearDetailDTO order_camping_gear
            , OrderFoodDetailDTO order_food
            , OrderFoodComboDetailDTO order_foot_combo
            , OrderComboDetailDTO order_combo)
        {
            return OrderDAO.CreateOrder(order, order_ticket, order_camping_gear, order_food, order_foot_combo, order_combo);
        }

        public bool CreateComboOrder(OrderDTO order, OrderCampingGearDetailDTO order_camping_gear, OrderFoodDetailDTO order_food, OrderFoodComboDetailDTO order_foot_combo, OrderComboDetailDTO order_combo)
        {
            throw new NotImplementedException();
        }

        public bool CreateUniqueOrder(OrderDTO order, List<OrderTicketDetailDTO> order_ticket, List<OrderCampingGearDetailDTO> order_camping_gear, List<OrderFoodDetailDTO> order_food, List<OrderFoodComboDetailDTO> order_foot_combo)
        {
            return OrderDAO.CreateUniqueOrder(order, order_ticket, order_camping_gear, order_food, order_foot_combo);
        }

        public bool DeleteOrder(int id)
        {
            return OrderDAO.DeleteOrder(id);

        }

        public bool EnterDeposit(int id, decimal money)
        {
            return OrderDAO.EnterDeposit(id, money);
        }

        public List<OrderDTO> GetAllOrders()
        {
            return OrderDAO.getAllOrder();
        }

        public bool UpdateOrder(OrderDTO order, OrderTicketDetailDTO order_ticket, OrderCampingGearDetailDTO order_camping_gear, OrderFoodDetailDTO order_food, OrderFoodComboDetailDTO order_foot_combo, OrderComboDetailDTO order_combo)
        {
            throw new NotImplementedException();
        }
    }
}
