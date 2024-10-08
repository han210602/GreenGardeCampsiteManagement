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
                            CustomerId = o.CustomerId,
                            EmployeeId = o.EmployeeId,
                            EmployeeName = o.Employee.FirstName + "" + o.Employee.LastName,
                            CustomerName = o.CustomerId != null ? o.Customer.FirstName + "" + o.Customer.LastName : o.CustomerName,
                            OrderDate = o.OrderDate,
                            OrderUsageDate = o.OrderUsageDate,
                            Deposit = o.Deposit,
                            TotalAmount = o.TotalAmount,
                            AmountPayable = o.AmountPayable,
                            StatusOrder = o.StatusOrder,
                            ActivityName = o.Activity.ActivityName
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

        public static bool CreateUniqueOrder(OrderDTO order
            , List<OrderTicketDetailDTO> order_ticket
            , List<OrderCampingGearDetailDTO> order_camping_gear
            , List<OrderFoodDetailDTO> order_food
            , List<OrderFoodComboDetailDTO> order_foot_combo)
        {
            try
            {
                using (var context = new GreenGardenContext())
                {
                    
                    if (order_ticket == null)
                    {
                        return false;
                    }
                    else
                    {
                        context.Add(new Order() {
                            CustomerId=order.CustomerId,
                            EmployeeId=order.EmployeeId,
                            CustomerName=order.CustomerName,
                            OrderUsageDate=order.OrderUsageDate,
                            TotalAmount=order.TotalAmount,
                            AmountPayable=order.TotalAmount,
                            StatusOrder=order.StatusOrder,
                            ActivityId=order.ActivityId,
                        });
                      
                        context.SaveChanges();
                       
                        int id_order=order.OrderId;
                        Console.WriteLine("Add thanh cong"+id_order);
                        foreach (var item in order_ticket)
                        {
                            context.OrderTicketDetails.Add(new OrderTicketDetail { 
                            OrderId=id_order,
                            TicketId=item.TicketId,
                            Quantity=item.Quantity,
                            });
                            context.SaveChanges();

                        }
                        if (order_food != null)
                        {
                            foreach (var item in order_food)
                            {
                                context.OrderFoodDetails.Add(new OrderFoodDetail
                                {
                                    OrderId = id_order,
                                    ItemId=item.ItemId,
                                    Quantity = item.Quantity,
                                });
                            }
                            context.SaveChanges();
                        }
                       

                        if (order_foot_combo != null)
                        {
                            foreach (var item in order_foot_combo)
                            {
                                context.OrderFoodComboDetails.Add(new OrderFoodComboDetail
                                {
                                    OrderId = id_order,
                                    ComboId = item.ComboId,
                                    Quantity = item.Quantity,
                                });
                            }
                            context.SaveChanges();
                        }
                        


                        return true;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public static OrderDetailDTO GetOrderDetail(int id)
        {

            OrderDetailDTO order = new OrderDetailDTO();
            try
            {
                using (var context = new GreenGardenContext())
                {
                    order = context.Orders
                        .Include(o => o.OrderTicketDetails).ThenInclude(t => t.Ticket)
                        .Include(o => o.OrderFoodDetails).ThenInclude(t=>t.Item)
                        .Include(o => o.OrderCampingGearDetails).ThenInclude(g=>g.Gear)
                        .Include(o => o.OrderComboDetails).ThenInclude(c=>c.Combo)
                        .Include(o => o.OrderFoodComboDetails).ThenInclude(f=>f.Combo)
                        .Select(o => new OrderDetailDTO()
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
                            ActivityId = o.Activity.ActivityName,
                            OrderTicketDetails=o.OrderTicketDetails.Select(o=>new OrderTicketDetailDTO
                            {
                                TicketId= o.TicketId,
                                Name=o.Ticket.TicketName,
                                Quantity=o.Quantity,
                                Price=o.Quantity.Value*o.Ticket.Price,
                                Description=o.Description,
                            }).ToList(),
                           OrderCampingGearDetails=o.OrderCampingGearDetails.Select(o=> new OrderCampingGearDetailDTO 
                           { 
                                GearId=o.GearId,
                                Name=o.Gear.GearName,
                                Quantity=o.Quantity,
                                Price = o.Quantity.Value * o.Gear.RentalPrice,

                           }).ToList(),
                           OrderFoodDetails=o.OrderFoodDetails.Select(o=>new OrderFoodDetailDTO
                           {
                               ItemId=o.ItemId,
                               Name=o.Item.ItemName,
                               Quantity=o.Quantity,
                               Price = o.Quantity.Value * o.Item.Price,
                           }).ToList(),
                           OrderFoodComboDetails = o.OrderFoodComboDetails.Select(o => new OrderFoodComboDetailDTO
                           {
                               ComboId=o.ComboId,
                               Name = o.Combo.ComboName,
                               Quantity = o.Quantity,
                               Price = o.Quantity.Value * o.Combo.Price,
                           }).ToList(),
                            OrderComboDetails = o.OrderComboDetails.Select(o => new OrderComboDetailDTO
                           {
                               ComboId=o.ComboId,
                               Name = o.Combo.ComboName,
                               Quantity = o.Quantity,
                               Price = o.Quantity.Value * o.Combo.Price,
                           }).ToList()

                        }).FirstOrDefault(o=>o.OrderId==id);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;

        }
    }
}
