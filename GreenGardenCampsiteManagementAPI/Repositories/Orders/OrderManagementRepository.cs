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

        
    }
}
