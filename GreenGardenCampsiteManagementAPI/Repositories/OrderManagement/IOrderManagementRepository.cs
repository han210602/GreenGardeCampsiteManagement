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
        List<OrderDTO> GetAllOrderOnline();

        List<OrderDTO> GetAllOrderDepositAndUsing();
        List<CustomerOrderDTO> GetCustomerOrders(int customerId, bool? statusOrder, int? activityId);
        OrderDetailDTO GetOrderDetail(int id);
        CustomerOrderDetailDTO GetCustomerOrderDetail(int customerId);
        bool CheckOutComboOrder(CheckoutCombo order);
        bool EnterDeposit(int id, decimal money);
        bool CancelDeposit(int id);
        bool DeleteOrder(int id);
        bool UpdateActivityOrder(int idorder, int idactivity);
        bool CheckOut(CheckOut order);
        bool CreateUniqueOrder(CreateUniqueOrderRequest order);
        List<OrderCampingGearByUsageDateDTO> GetListOrderGearByUsageDate(DateTime usagedate);
        bool CreateComboOrder(CreateComboOrderRequest order);
        bool UpdateOrder(UpdateOrderDTO order);
        bool UpdateTicket(List<OrderTicketAddlDTO>tickets);
        bool UpdateGear(List<OrderCampingGearAddDTO> gears);
        bool UpdateFood(List<OrderFoodAddDTO> foods);
        bool UpdateCombo(List<OrderComboAddDTO> combos);
        bool UpdateComboFood(List<OrderFoodComboAddDTO> foodcombos);
        bool UpdateActivity(int orderId);
    }
}
