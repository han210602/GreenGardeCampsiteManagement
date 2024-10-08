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
        public bool EnterDeposit(int id, decimal money)
        {
            return OrderDAO.EnterDeposit(id, money);
        }

        public List<OrderDTO> GetAllOrders()
        {
            return OrderDAO.getAllOrder();
        }
    }
}
