using BusinessObject.DTOs;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDAO
    {
        public static List<OrderDTO> getAllOrder()
        {
            var listProducts = new List<OrderDTO>();
            try
            {
                using (var context = new GreenGardenContext())
                {
                    listProducts = context.Orders
                        .Include(u => u.Customer)
                        .Include(e => e.Employee)
                        .Include(a => a.Activity)
                        .Select(o => new OrderDTO()
                        {
                            OrderId = o.OrderId,
                            EmployeeName = o.Employee.FirstName + "" + o.Employee.LastName,
                            CustomerName = o.CustomerId != null ? o.Customer.FirstName + "" + o.Customer.LastName : o.CustomerName,
                            OrderDate = o.OrderDate,
                            OrderUsageDate = o.OrderUsageDate,
                            Deposit = o.Deposit,
                            TotalAmount = o.TotalAmount,
                            AmountPayable = o.AmountPayable,
                            StatusOrder = o.StatusOrder,
                            ActivityId = o.Activity.ActivityName
                        })
                        .ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listProducts;
        }
        public static bool EnterDeposit(int id, decimal money)
        {
            try
            {
                using (var context = new GreenGardenContext())
                {
                    var order = context.Orders.FirstOrDefault(o => o.OrderId == id);
                    if ((order!=null))
                    {
                        order.Deposit = money;
                        order.StatusOrder = true;
                        order.AmountPayable = order.TotalAmount - money;
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public static bool DeleteOrder(int id)
        {

            try
            {
                using (var context = new GreenGardenContext())
                {
                    var order = context.Orders.FirstOrDefault(o => o.OrderId == id);
                    if (order != null)
                    { 
                        var list_ticket=context.OrderTicketDetails.Where(o=>o.OrderId==id).ToList();
                        var list_foot=context.OrderFoodDetails.Where(o=>o.OrderId==id).ToList();    
                        var list_combo=context.OrderComboDetails.Where(o => o.OrderId == id).ToList();
                        var list_camping=context.OrderCampingGearDetails.Where(o=>o.OrderId == id).ToList();
                        var list_combofoot=context.OrderFoodComboDetails.Where(o=>o.OrderId==id).ToList();
                        context.RemoveRange(list_ticket);
                        context.RemoveRange(list_foot);
                        context.RemoveRange(list_camping);
                        context.RemoveRange(list_combofoot);
                        context.RemoveRange(list_combo);
                        context.SaveChanges();
                        context.Remove(order);
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }



        }

        public static bool CreateOrder(OrderDTO order, OrderTicketDetailDTO order_ticket, OrderCampingGearDetailDTO order_camping_gear, OrderFoodDetailDTO order_food, OrderFoodComboDetailDTO order_foot_combo, OrderComboDetailDTO order_combo)
        {
            if(order_combo!=null)
            {

            }
            else
            {
                if (order_ticket != null)
                {

                }
            }


            return false;
        }
    }
}
