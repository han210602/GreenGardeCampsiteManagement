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
    }
}
