using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Orders
{
    public class OrderManagementRepository : IOrderManagementRepository
    {
     

        public bool CancelDeposit(int id)
        {
            return OrderDAO.CancelDeposit(id);
        }

        public bool CreateComboOrder(CreateComboOrderRequest order)
        {
            return OrderDAO.CreateComboOrder(order);
        }

        public bool CreateUniqueOrder(CreateUniqueOrderRequest order)
        {
            return OrderDAO.CreateUniqueOrder(order);
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
        public List<OrderDTO> GetAllOrderDepositAndUsing()

        {
            return OrderDAO.getAllOrderDepositAndUsing();
        }
        public List<OrderCampingGearByUsageDateDTO> GetListOrderGearByUsageDate(DateTime usagedate)
        {
            return OrderDAO.GetListOrderGearByUsageDate( usagedate);

        }

        public OrderDetailDTO GetOrderDetail(int id)
        {
            return OrderDAO.GetOrderDetail(id);

        }

        public bool UpdateActivityOrder(int idorder, int idactivity)
        {
            return OrderDAO.UpdateActivityOrder(idorder, idactivity);
        }

        public bool UpdateCombo(List<OrderComboAddDTO> combos)
        {
            return OrderDAO.UpdateCombo(combos);
        }

        public bool UpdateComboFood(List<OrderFoodComboAddDTO> foodcombos)
        {
            return OrderDAO.UpdateFoodCombo(foodcombos);
        }

        public bool UpdateFood(List<OrderFoodAddDTO> foods)
        {
            return OrderDAO.UpdateFood(foods);
        }

        public bool UpdateGear(List<OrderCampingGearAddDTO> gears)
        {
            return OrderDAO.UpdateGear(gears);
        }

        public bool UpdateOrder(UpdateOrderDTO order)
        {
            return OrderDAO.UpdateOrder(order);
        }

        public bool UpdateTicket(List<OrderTicketAddlDTO> tickets)
        {
            return OrderDAO.UpdateTicket(tickets);

        }
    }
}
