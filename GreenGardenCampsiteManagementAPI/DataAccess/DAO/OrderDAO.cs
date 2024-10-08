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
    }
}
