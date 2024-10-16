using BusinessObject.DTOs;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Orders
{
     public interface IOrderManagementRepository
     {
        List<OrderDTO> GetAllOrders();
        OrderDetailDTO GetOrderDetail(int id);
        bool EnterDeposit(int id, decimal money);
        bool CancelDeposit(int id);
        bool DeleteOrder(int id);
        bool UpdateActivityOrder(int idorder, int idactivity);
        bool CreateUniqueOrder(CreateUniqueOrderRequest order);
        List<OrderCampingGearByUsageDateDTO> GetListOrderGearByUsageDate(DateTime usagedate);
        bool CreateComboOrder(CreateComboOrderRequest order);

    }
}
