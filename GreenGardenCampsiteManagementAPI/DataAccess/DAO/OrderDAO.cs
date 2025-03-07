﻿using BusinessObject.DTOs;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

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
                            EmployeeName = o.Employee.FirstName + " " + o.Employee.LastName,
                            CustomerName = o.CustomerId != null ? o.Customer.FirstName + " " + o.Customer.LastName : o.CustomerName,
                            PhoneCustomer = o.PhoneCustomer == null ? o.Customer.PhoneNumber : o.PhoneCustomer,
                            OrderDate = o.OrderDate,
                            OrderUsageDate = o.OrderUsageDate,
                            Deposit = o.Deposit,
                            TotalAmount = o.TotalAmount,
                            AmountPayable = o.AmountPayable,
                            StatusOrder = o.StatusOrder,
                            ActivityId = o.ActivityId,
                            ActivityName = o.Activity.ActivityName,
                            OrderCheckoutDate = o.OrderCheckoutDate,
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
        public static List<CustomerOrderDTO> GetCustomerOrders(int customerId, bool? statusOrder = null, int? activityId = null)
        {
            var customerOrders = new List<CustomerOrderDTO>();
            try
            {
                using (var context = new GreenGardenContext())
                {
                    customerOrders = context.Orders
                        .Where(o => o.CustomerId == customerId
                                    && (statusOrder == null || o.StatusOrder == statusOrder)
                                    && (activityId == null || o.ActivityId == activityId))
                        .Include(u => u.Customer)
                        .Include(a => a.Activity)
                        .Select(o => new CustomerOrderDTO()
                        {
                            OrderId = o.OrderId,
                            CustomerId = o.CustomerId,
                            CustomerName = o.CustomerId != null ? o.Customer.FirstName + " " + o.Customer.LastName : o.CustomerName,
                            PhoneCustomer = o.PhoneCustomer == null ? o.Customer.PhoneNumber : o.PhoneCustomer,
                            OrderDate = o.OrderDate,
                            OrderUsageDate = o.OrderUsageDate,
                            Deposit = o.Deposit,
                            TotalAmount = o.TotalAmount,
                            AmountPayable = o.AmountPayable,
                            StatusOrder = o.StatusOrder,
                            ActivityId = o.ActivityId,
                            ActivityName = o.Activity.ActivityName,

                        })
                        .OrderBy(o => o.ActivityId) // Sắp xếp theo ActivityId
                        .ThenByDescending(o => o.OrderDate) // Sau đó sắp xếp theo OrderDate giảm dần
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customerOrders;
        }


        public static List<OrderDTO> getAllOrderDepositAndUsing()
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
                        .Where(o => o.ActivityId == 2 && o.StatusOrder == true)
                        .Select(o => new OrderDTO()
                        {
                            OrderId = o.OrderId,
                            CustomerId = o.CustomerId,
                            EmployeeId = o.EmployeeId,
                            EmployeeName = o.Employee.FirstName + "" + o.Employee.LastName,
                            CustomerName = o.CustomerId != null ? o.Customer.FirstName + " " + o.Customer.LastName : o.CustomerName,
                            PhoneCustomer = o.PhoneCustomer != null ? o.Customer.PhoneNumber : o.PhoneCustomer,
                            OrderDate = o.OrderDate,
                            OrderUsageDate = o.OrderUsageDate,
                            Deposit = o.Deposit,
                            TotalAmount = o.TotalAmount,
                            AmountPayable = o.AmountPayable,
                            StatusOrder = o.StatusOrder,
                            ActivityId = o.ActivityId,
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
                    if ((order != null))
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
                        var list_ticket = context.OrderTicketDetails.Where(o => o.OrderId == id).ToList();
                        var list_foot = context.OrderFoodDetails.Where(o => o.OrderId == id).ToList();
                        var list_combo = context.OrderComboDetails.Where(o => o.OrderId == id).ToList();
                        var list_camping = context.OrderCampingGearDetails.Where(o => o.OrderId == id).ToList();
                        var list_combofoot = context.OrderFoodComboDetails.Where(o => o.OrderId == id).ToList();
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
            if (order_combo != null)
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

        public static bool CreateUniqueOrder(CreateUniqueOrderRequest order_request)
        {
            var order = order_request.Order;
            var order_ticket = order_request.OrderTicket;
            var order_camping_gear = order_request.OrderCampingGear;
            var order_food = order_request.OrderFood;
            var order_foot_combo = order_request.OrderFoodCombo;


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

                        Order newOrder;

                        
                            newOrder = new Order
                            {
                                EmployeeId = order.EmployeeId,
                                CustomerName = order.CustomerName,
                                OrderUsageDate = order.OrderUsageDate,
                                Deposit = order.Deposit,
                                TotalAmount = order.TotalAmount,
                                AmountPayable = order.TotalAmount - order.Deposit,
                                StatusOrder = false,
                                ActivityId = 1,
                                PhoneCustomer = order.PhoneCustomer

                            };
                        if (order.Deposit > 0)
                        {
                            newOrder.StatusOrder = true;
                        }


                        // Add the order and save to the database
                        context.Orders.Add(newOrder);
                        context.SaveChanges();

                        int id = newOrder.OrderId;


                        List<OrderTicketDetail> tickets = order_ticket.Select(t => new OrderTicketDetail
                        {
                            TicketId = t.TicketId,
                            OrderId = id,
                            Quantity = t.Quantity,
                        }).ToList();
                        context.OrderTicketDetails.AddRange(tickets);

                        if (order_camping_gear != null)
                        {
                            List<OrderCampingGearDetail> gears = order_camping_gear.Select(g => new OrderCampingGearDetail
                            {
                                GearId = g.GearId,
                                Quantity = g.Quantity,
                                OrderId = id,
                            }).ToList();
                            context.OrderCampingGearDetails.AddRange(gears);
                            context.SaveChanges();

                        }
                        if (order_food != null)
                        {
                            List<OrderFoodDetail> foods = order_food.Select(f => new OrderFoodDetail
                            {
                                OrderId = id,
                                ItemId = f.ItemId,
                                Quantity = f.Quantity,
                                Description = f.Description,
                            }).ToList();
                            context.OrderFoodDetails.AddRange(foods);
                            context.SaveChanges();

                        }
                        if (order_foot_combo != null)
                        {
                            List<OrderFoodComboDetail> foodCombos = order_foot_combo.Select(c => new OrderFoodComboDetail
                            {
                                OrderId = id,
                                ComboId = c.ComboId,
                                Quantity = c.Quantity,
                            }).ToList();
                            context.OrderFoodComboDetails.AddRange(foodCombos);
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
        public static bool CreateUniqueOrderUsing(CreateUniqueOrderRequest order_request)
        {
            var order = order_request.Order;
            var order_ticket = order_request.OrderTicket;
            var order_camping_gear = order_request.OrderCampingGear;
            var order_food = order_request.OrderFood;
            var order_foot_combo = order_request.OrderFoodCombo;


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

                        Order newOrder;


                        newOrder = new Order
                        {
                            EmployeeId = order.EmployeeId,
                            CustomerName = order.CustomerName,
                            OrderUsageDate = order.OrderUsageDate,
                            Deposit = order.Deposit,
                            TotalAmount = order.TotalAmount,
                            AmountPayable = order.TotalAmount - order.Deposit,
                            StatusOrder = false,
                            ActivityId = 2,
                            PhoneCustomer = order.PhoneCustomer

                        };


                        // Add the order and save to the database
                        context.Orders.Add(newOrder);
                        context.SaveChanges();

                        int id = newOrder.OrderId;


                        List<OrderTicketDetail> tickets = order_ticket.Select(t => new OrderTicketDetail
                        {
                            TicketId = t.TicketId,
                            OrderId = id,
                            Quantity = t.Quantity,
                        }).ToList();
                        context.OrderTicketDetails.AddRange(tickets);

                        if (order_camping_gear != null)
                        {
                            List<OrderCampingGearDetail> gears = order_camping_gear.Select(g => new OrderCampingGearDetail
                            {
                                GearId = g.GearId,
                                Quantity = g.Quantity,
                                OrderId = id,
                            }).ToList();
                            context.OrderCampingGearDetails.AddRange(gears);
                            context.SaveChanges();

                        }
                        if (order_food != null)
                        {
                            List<OrderFoodDetail> foods = order_food.Select(f => new OrderFoodDetail
                            {
                                OrderId = id,
                                ItemId = f.ItemId,
                                Quantity = f.Quantity,
                                Description = f.Description,
                            }).ToList();
                            context.OrderFoodDetails.AddRange(foods);
                            context.SaveChanges();

                        }
                        if (order_foot_combo != null)
                        {
                            List<OrderFoodComboDetail> foodCombos = order_foot_combo.Select(c => new OrderFoodComboDetail
                            {
                                OrderId = id,
                                ComboId = c.ComboId,
                                Quantity = c.Quantity,
                            }).ToList();
                            context.OrderFoodComboDetails.AddRange(foodCombos);
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

        //        context.Add(new Order()
        //        {
        //            CustomerId = order.CustomerId,
        //                            EmployeeId = order.EmployeeId,
        //                            CustomerName = order.CustomerName,
        //                            OrderUsageDate = order.OrderUsageDate,
        //                            TotalAmount = order.TotalAmount,
        //                            AmountPayable = order.TotalAmount,
        //                            StatusOrder = order.StatusOrder,
        //                            ActivityId = order.ActivityId,
        //                        });

        //                        context.SaveChanges();

        //                        int id_order = order.OrderId;
        //        Console.WriteLine("Add thanh cong"+id_order);
        //                        foreach (var item in order_ticket)
        //                        {
        //                            context.OrderTicketDetails.Add(new OrderTicketDetail { 
        //                            OrderId=id_order,
        //                            TicketId=item.TicketId,
        //                            Quantity=item.Quantity,
        //                            });
        //                            context.SaveChanges();

        //                        }
        //if (order_food != null)
        //{
        //    foreach (var item in order_food)
        //    {
        //        context.OrderFoodDetails.Add(new OrderFoodDetail
        //        {
        //            OrderId = id_order,
        //            ItemId = item.ItemId,
        //            Quantity = item.Quantity,
        //        });
        //    }
        //    context.SaveChanges();
        //}


        //if (order_foot_combo != null)
        //{
        //    foreach (var item in order_foot_combo)
        //    {
        //        context.OrderFoodComboDetails.Add(new OrderFoodComboDetail
        //        {
        //            OrderId = id_order,
        //            ComboId = item.ComboId,
        //            Quantity = item.Quantity,
        //        });
        //    }
        //    context.SaveChanges();
        //}



        //return true;
        public static OrderDetailDTO GetOrderDetail(int id)
        {

            OrderDetailDTO order = new OrderDetailDTO();
            try
            {
                using (var context = new GreenGardenContext())
                {
                    order = context.Orders
                        .Include(o => o.OrderTicketDetails).ThenInclude(t => t.Ticket)
                        .Include(o => o.OrderFoodDetails).ThenInclude(t => t.Item)
                        .Include(o => o.OrderCampingGearDetails).ThenInclude(g => g.Gear)
                        .Include(o => o.OrderComboDetails).ThenInclude(c => c.Combo)
                        .Include(o => o.OrderFoodComboDetails).ThenInclude(f => f.Combo)
                        .Select(o => new OrderDetailDTO()
                        {
                            OrderId = o.OrderId,
                            EmployeeName = o.Employee.FirstName + "" + o.Employee.LastName,
                            CustomerName = o.CustomerId != null ? o.Customer.FirstName + " " + o.Customer.LastName : o.CustomerName,
                            OrderDate = o.OrderDate,
                            OrderUsageDate = o.OrderUsageDate,
                            Deposit = o.Deposit,
                            TotalAmount = o.TotalAmount,
                            AmountPayable = o.AmountPayable,
                            StatusOrder = o.StatusOrder,
                            ActivityId = o.ActivityId,
                            ActivityName = o.Activity.ActivityName,
                            PhoneCustomer = o.PhoneCustomer == null ? o.Customer.PhoneNumber : o.PhoneCustomer,

                            OrderTicketDetails = o.OrderTicketDetails.Select(o => new OrderTicketDetailDTO
                            {
                                TicketId = o.TicketId,
                                Name = o.Ticket.TicketName,
                                Quantity = o.Quantity,
                                Price = o.Ticket.Price,
                                Description = o.Description,
                                ImgUrl = o.Ticket.ImgUrl,
                            }).ToList(),
                            OrderCampingGearDetails = o.OrderCampingGearDetails.Select(o => new OrderCampingGearDetailDTO
                            {
                                GearId = o.GearId,
                                Name = o.Gear.GearName,
                                Quantity = o.Quantity,
                                Price = o.Gear.RentalPrice,
                                ImgUrl = o.Gear.ImgUrl,

                            }).ToList(),
                            OrderFoodDetails = o.OrderFoodDetails.Select(o => new OrderFoodDetailDTO
                            {
                                ItemId = o.ItemId,
                                Name = o.Item.ItemName,
                                Quantity = o.Quantity,
                                Price = o.Item.Price,
                                ImgUrl = o.Item.ImgUrl,
                            }).ToList(),
                            OrderFoodComboDetails = o.OrderFoodComboDetails.Select(o => new OrderFoodComboDetailDTO
                            {
                                ComboId = o.ComboId,
                                Name = o.Combo.ComboName,
                                Quantity = o.Quantity,
                                Price = o.Combo.Price,
                                ImgUrl = o.Combo.ImgUrl,
                            }).ToList(),
                            OrderComboDetails = o.OrderComboDetails.Select(o => new OrderComboDetailDTO
                            {
                                ComboId = o.ComboId,
                                Name = o.Combo.ComboName,
                                Quantity = o.Quantity,
                                Price = o.Combo.Price,
                                ImgUrl = o.Combo.ImgUrl,
                            }).ToList()

                        }).FirstOrDefault(o => o.OrderId == id);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;

        }
        public static CustomerOrderDetailDTO GetCustomerOrderDetail(int orderId)
        {
            CustomerOrderDetailDTO orderDetail = new CustomerOrderDetailDTO();
            try
            {
                using (var context = new GreenGardenContext())
                {
                    orderDetail = context.Orders
                        .Include(o => o.OrderTicketDetails).ThenInclude(t => t.Ticket)
                        .Include(o => o.OrderFoodDetails).ThenInclude(f => f.Item)
                        .Include(o => o.OrderCampingGearDetails).ThenInclude(g => g.Gear)
                        .Include(o => o.OrderComboDetails).ThenInclude(c => c.Combo)
                        .Include(o => o.OrderFoodComboDetails).ThenInclude(fc => fc.Combo)
                        .Select(o => new CustomerOrderDetailDTO()
                        {
                            OrderId = o.OrderId,
                            CustomerName = o.CustomerId != null ? o.Customer.FirstName + " " + o.Customer.LastName : o.CustomerName,
                            PhoneCustomer = o.PhoneCustomer == null ? o.Customer.PhoneNumber : o.PhoneCustomer,
                            OrderDate = o.OrderDate,
                            OrderUsageDate = o.OrderUsageDate,
                            Deposit = o.Deposit,
                            TotalAmount = o.TotalAmount,
                            AmountPayable = o.AmountPayable,
                            StatusOrder = o.StatusOrder,
                            ActivityId = o.ActivityId,
                            ActivityName = o.Activity.ActivityName,

                            OrderTicketDetails = o.OrderTicketDetails.Select(o => new OrderTicketDetailDTO
                            {
                                TicketId = o.TicketId,
                                Name = o.Ticket.TicketName,
                                Quantity = o.Quantity,
                                Price = o.Ticket.Price,
                                Description = o.Description,
                                ImgUrl = o.Ticket.ImgUrl,
                            }).ToList(),
                            OrderCampingGearDetails = o.OrderCampingGearDetails.Select(o => new OrderCampingGearDetailDTO
                            {
                                GearId = o.GearId,
                                Name = o.Gear.GearName,
                                Quantity = o.Quantity,
                                Price = o.Gear.RentalPrice,
                                ImgUrl = o.Gear.ImgUrl,

                            }).ToList(),
                            OrderFoodDetails = o.OrderFoodDetails.Select(o => new OrderFoodDetailDTO
                            {
                                ItemId = o.ItemId,
                                Name = o.Item.ItemName,
                                Quantity = o.Quantity,
                                Price = o.Item.Price,
                                ImgUrl = o.Item.ImgUrl,
                            }).ToList(),
                            OrderFoodComboDetails = o.OrderFoodComboDetails.Select(o => new OrderFoodComboDetailDTO
                            {
                                ComboId = o.ComboId,
                                Name = o.Combo.ComboName,
                                Quantity = o.Quantity,
                                Price = o.Combo.Price,
                                ImgUrl = o.Combo.ImgUrl,
                            }).ToList(),
                            OrderComboDetails = o.OrderComboDetails.Select(o => new OrderComboDetailDTO
                            {
                                ComboId = o.ComboId,
                                Name = o.Combo.ComboName,
                                Quantity = o.Quantity,
                                Price = o.Combo.Price,
                                ImgUrl = o.Combo.ImgUrl,
                            }).ToList()

                        }).FirstOrDefault(o => o.OrderId == orderId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetail;
        }

        public static bool UpdateActivityOrder(int idorder, int idactivity)
        {
            try
            {
                using (var context = new GreenGardenContext())
                {
                    var order = context.Orders.FirstOrDefault(o => o.OrderId == idorder);
                    if (order != null)
                    {
                        order.ActivityId = idactivity;
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
        public static List<OrderDTO> getAllOrderOnline()
        {
            var listProducts = new List<OrderDTO>();
            try
            {
                using (var context = new GreenGardenContext())
                {
                    listProducts = context.Orders
                        .Include(u => u.Customer)
                        .Include(e => e.Employee)
                        .Include(a => a.Activity).Where(s => s.ActivityId == 1)
                        .Select(o => new OrderDTO()
                        {
                            OrderId = o.OrderId,
                            CustomerId = o.CustomerId,
                            EmployeeId = o.EmployeeId,
                            EmployeeName = o.Employee.FirstName + "" + o.Employee.LastName,
                            CustomerName = o.CustomerId != null ? o.Customer.FirstName + " " + o.Customer.LastName : o.CustomerName,
                            PhoneCustomer = o.PhoneCustomer == null ? o.Customer.PhoneNumber : o.PhoneCustomer,
                            OrderDate = o.OrderDate,
                            OrderUsageDate = o.OrderUsageDate,
                            Deposit = o.Deposit,
                            TotalAmount = o.TotalAmount,
                            AmountPayable = o.AmountPayable,
                            StatusOrder = o.StatusOrder,
                            ActivityId = o.ActivityId,
                            ActivityName = o.Activity.ActivityName,
                            mustDeposit = (
    (o.OrderCampingGearDetails != null && o.OrderCampingGearDetails.Any()) ||
    (o.OrderFoodComboDetails != null && o.OrderFoodComboDetails.Any()) ||
    (o.OrderFoodDetails != null && o.OrderFoodDetails.Any()) ||
    (o.OrderComboDetails != null && o.OrderComboDetails.Any())
) ? true : false
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
        public static bool CancelDeposit(int id)
        {
            try
            {
                using (var context = new GreenGardenContext())
                {
                    var order = context.Orders.FirstOrDefault(o => o.OrderId == id);
                    if ((order != null))
                    {

                        order.StatusOrder = false;
                        order.AmountPayable = order.TotalAmount;
                        order.Deposit = 0;
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

        public static List<OrderCampingGearByUsageDateDTO> GetListOrderGearByUsageDate(DateTime usagedate)
        {
            var listProducts = new List<OrderCampingGearByUsageDateDTO>();
            try
            {
                using (var context = new GreenGardenContext())
                {
                    listProducts = context.OrderCampingGearDetails.Include(o => o.Order)
                        .Where(s => s.Order.OrderUsageDate.Value.Date == usagedate.Date)
                        .Where(s => s.Order.ActivityId != 1002 && s.Order.ActivityId != 3)// Compare dates directly
                        .Select(s => new OrderCampingGearByUsageDateDTO
                        {
                            GearId = s.GearId,
                            Quantity = s.Quantity,
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

        public static bool CreateComboOrder(CreateComboOrderRequest order_request)
        {


            var order = order_request.Order;
            var order_combo = order_request.OrderCombo;
            var order_camping_gear = order_request.OrderCampingGear;
            var order_food = order_request.OrderFood;
            var order_foot_combo = order_request.OrderFoodCombo;


            try
            {
                using (var context = new GreenGardenContext())
                {

                    if (order_combo == null)
                    {
                        return false;
                    }
                    else
                    {

                        Order newOrder;

                        
                            newOrder = new Order
                            {
                                EmployeeId = order.EmployeeId,
                                CustomerName = order.CustomerName,
                                OrderUsageDate = order.OrderUsageDate,
                                Deposit = order.Deposit,
                                TotalAmount = order.TotalAmount,
                                AmountPayable = order.TotalAmount - order.Deposit,
                                StatusOrder = false,
                                ActivityId = 1,
                                PhoneCustomer = order.PhoneCustomer

                            };
                        if (order.Deposit > 0)
                        {
                            newOrder.StatusOrder = true;
                        }
                       

                        // Add the order and save to the database
                        context.Orders.Add(newOrder);
                        context.SaveChanges();

                        int id = newOrder.OrderId;


                        List<OrderComboDetail> tickets = order_combo.Select(t => new OrderComboDetail
                        {
                            ComboId = t.ComboId,
                            OrderId = id,
                            Quantity = t.Quantity,
                        }).ToList();
                        context.OrderComboDetails.AddRange(tickets);

                        if (order_camping_gear != null)
                        {
                            List<OrderCampingGearDetail> gears = order_camping_gear.Select(g => new OrderCampingGearDetail
                            {
                                GearId = g.GearId,
                                Quantity = g.Quantity,
                                OrderId = id,
                            }).ToList();
                            context.OrderCampingGearDetails.AddRange(gears);
                            context.SaveChanges();

                        }
                        if (order_food != null)
                        {
                            List<OrderFoodDetail> foods = order_food.Select(f => new OrderFoodDetail
                            {
                                OrderId = id,
                                ItemId = f.ItemId,
                                Quantity = f.Quantity,
                                Description = f.Description,
                            }).ToList();
                            context.OrderFoodDetails.AddRange(foods);
                            context.SaveChanges();

                        }
                        if (order_foot_combo != null)
                        {
                            List<OrderFoodComboDetail> foodCombos = order_foot_combo.Select(c => new OrderFoodComboDetail
                            {
                                OrderId = id,
                                ComboId = c.ComboId,
                                Quantity = c.Quantity,
                            }).ToList();
                            context.OrderFoodComboDetails.AddRange(foodCombos);
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
        public static bool CreateComboOrderUsing(CreateComboOrderRequest order_request)
        {


            var order = order_request.Order;
            var order_combo = order_request.OrderCombo;
            var order_camping_gear = order_request.OrderCampingGear;
            var order_food = order_request.OrderFood;
            var order_foot_combo = order_request.OrderFoodCombo;


            try
            {
                using (var context = new GreenGardenContext())
                {

                    if (order_combo == null)
                    {
                        return false;
                    }
                    else
                    {

                        Order newOrder;


                        newOrder = new Order
                        {
                            EmployeeId = order.EmployeeId,
                            CustomerName = order.CustomerName,
                            OrderUsageDate = order.OrderUsageDate,
                            Deposit = order.Deposit,
                            TotalAmount = order.TotalAmount,
                            AmountPayable = order.TotalAmount - order.Deposit,
                            StatusOrder = false,
                            ActivityId = 2,
                            PhoneCustomer = order.PhoneCustomer

                        };



                        // Add the order and save to the database
                        context.Orders.Add(newOrder);
                        context.SaveChanges();

                        int id = newOrder.OrderId;


                        List<OrderComboDetail> tickets = order_combo.Select(t => new OrderComboDetail
                        {
                            ComboId = t.ComboId,
                            OrderId = id,
                            Quantity = t.Quantity,
                        }).ToList();
                        context.OrderComboDetails.AddRange(tickets);

                        if (order_camping_gear != null)
                        {
                            List<OrderCampingGearDetail> gears = order_camping_gear.Select(g => new OrderCampingGearDetail
                            {
                                GearId = g.GearId,
                                Quantity = g.Quantity,
                                OrderId = id,
                            }).ToList();
                            context.OrderCampingGearDetails.AddRange(gears);
                            context.SaveChanges();

                        }
                        if (order_food != null)
                        {
                            List<OrderFoodDetail> foods = order_food.Select(f => new OrderFoodDetail
                            {
                                OrderId = id,
                                ItemId = f.ItemId,
                                Quantity = f.Quantity,
                                Description = f.Description,
                            }).ToList();
                            context.OrderFoodDetails.AddRange(foods);
                            context.SaveChanges();

                        }
                        if (order_foot_combo != null)
                        {
                            List<OrderFoodComboDetail> foodCombos = order_foot_combo.Select(c => new OrderFoodComboDetail
                            {
                                OrderId = id,
                                ComboId = c.ComboId,
                                Quantity = c.Quantity,
                            }).ToList();
                            context.OrderFoodComboDetails.AddRange(foodCombos);
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
        public static bool UpdateTicket(List<OrderTicketAddlDTO> tickets)
        {
            try
            {
                using (var context = new GreenGardenContext())
                {


                    if (tickets[0].TicketId != 0)
                    {
                        var list = context.OrderTicketDetails.Where(s => s.OrderId == tickets[0].OrderId);
                        context.OrderTicketDetails.RemoveRange(list);
                        context.SaveChanges();

                        var newlist = tickets.Select(s => new OrderTicketDetail()
                        {
                            OrderId = s.OrderId,
                            TicketId = s.TicketId,
                            Quantity = s.Quantity,

                        });
                        context.OrderTicketDetails.AddRange(newlist);
                    }
                    else
                    {
                        DeleteOrder(tickets[0].OrderId);
                    }
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }


        }
        public static bool UpdateGear(List<OrderCampingGearAddDTO> tickets)
        {
            try
            {
                using (var context = new GreenGardenContext())
                {
                    var list = context.OrderCampingGearDetails.Where(s => s.OrderId == tickets[0].OrderId);
                    context.OrderCampingGearDetails.RemoveRange(list);
                    context.SaveChanges();

                    if (tickets[0].GearId != 0)
                    {
                        var newlist = tickets.Select(s => new OrderCampingGearDetail()
                        {
                            OrderId = s.OrderId,
                            GearId = s.GearId,
                            Quantity = s.Quantity,

                        });
                        context.OrderCampingGearDetails.AddRange(newlist);
                    }

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }


        }
        public static bool UpdateFood(List<OrderFoodAddDTO> tickets)
        {
            try
            {
                using (var context = new GreenGardenContext())
                {
                    var list = context.OrderFoodDetails.Where(s => s.OrderId == tickets[0].OrderId);
                    context.OrderFoodDetails.RemoveRange(list);
                    context.SaveChanges();

                    if (tickets[0].ItemId != 0)
                    {
                        var newlist = tickets.Select(s => new OrderFoodDetail()
                        {
                            OrderId = s.OrderId,
                            ItemId = s.ItemId,
                            Quantity = s.Quantity,

                        });
                        context.OrderFoodDetails.AddRange(newlist);
                    }
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }


        }
        public static bool UpdateCombo(List<OrderComboAddDTO> tickets)
        {
            try
            {
                using (var context = new GreenGardenContext())
                {
                    if (tickets[0].ComboId != 0)
                    {
                        var list = context.OrderComboDetails.Where(s => s.OrderId == tickets[0].OrderId);
                        context.OrderComboDetails.RemoveRange(list);
                        context.SaveChanges();

                        var newlist = tickets.Select(s => new OrderComboDetail()
                        {
                            OrderId = s.OrderId,
                            ComboId = s.ComboId,
                            Quantity = s.Quantity,

                        });
                        context.OrderComboDetails.AddRange(newlist);
                    }
                    else
                    {
                        DeleteOrder(tickets[0].OrderId);
                    }

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }


        }
        public static bool UpdateFoodCombo(List<OrderFoodComboAddDTO> tickets)
        {
            try
            {
                using (var context = new GreenGardenContext())
                {
                    var list = context.OrderFoodComboDetails.Where(s => s.OrderId == tickets[0].OrderId);
                    context.OrderFoodComboDetails.RemoveRange(list);
                    context.SaveChanges();

                    if (tickets[0].ComboId != 0)
                    {
                        var newlist = tickets.Select(s => new OrderFoodComboDetail()
                        {
                            OrderId = s.OrderId,
                            ComboId = s.ComboId,
                            Quantity = s.Quantity,

                        });
                        context.OrderFoodComboDetails.AddRange(newlist);
                    }
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }


        }

        public static bool UpdateOrder(UpdateOrderDTO order)
        {

            try
            {
                using (var context = new GreenGardenContext())
                {
                    var item = context.Orders.FirstOrDefault(s => s.OrderId == order.OrderId);
                    if (item != null)
                    {
                        item.OrderUsageDate = order.OrderUsageDate;
                        item.TotalAmount = order.TotalAmount;
                        item.AmountPayable = order.TotalAmount - item.Deposit;
                        item.OrderCheckoutDate = order.OrderCheckoutDate;
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
        public static bool CheckOut(CheckOut order_request)
        {
            var order = order_request.Order;
            var order_ticket = order_request.OrderTicket;
            var order_camping_gear = order_request.OrderCampingGear;
            var order_food = order_request.OrderFood;
            var order_foot_combo = order_request.OrderFoodCombo;


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

                        Order newOrder;


                        newOrder = new Order
                        {
                            CustomerId = order.CustomerId,
                            CustomerName = order.CustomerName,
                            OrderUsageDate = order.OrderUsageDate,
                            OrderDate = DateTime.Now,
                            Deposit = 0,
                            TotalAmount = order.TotalAmount,
                            AmountPayable = order.TotalAmount,
                            StatusOrder = false,
                            ActivityId = 1,
                            PhoneCustomer = order.PhoneCustomer
                        };


                        // Add the order and save to the database
                        context.Orders.Add(newOrder);
                        context.SaveChanges();

                        int id = newOrder.OrderId;


                        List<OrderTicketDetail> tickets = order_ticket.Select(t => new OrderTicketDetail
                        {
                            TicketId = t.TicketId,
                            OrderId = id,
                            Quantity = t.Quantity,
                        }).ToList();
                        context.OrderTicketDetails.AddRange(tickets);

                        if (order_camping_gear != null)
                        {
                            List<OrderCampingGearDetail> gears = order_camping_gear.Select(g => new OrderCampingGearDetail
                            {
                                GearId = g.GearId,
                                Quantity = g.Quantity,
                                OrderId = id,
                            }).ToList();
                            context.OrderCampingGearDetails.AddRange(gears);
                            context.SaveChanges();

                        }
                        if (order_food != null)
                        {
                            List<OrderFoodDetail> foods = order_food.Select(f => new OrderFoodDetail
                            {
                                OrderId = id,
                                ItemId = f.ItemId,
                                Quantity = f.Quantity,
                                Description = f.Description,
                            }).ToList();
                            context.OrderFoodDetails.AddRange(foods);
                            context.SaveChanges();

                        }
                        if (order_foot_combo != null)
                        {
                            List<OrderFoodComboDetail> foodCombos = order_foot_combo.Select(c => new OrderFoodComboDetail
                            {
                                OrderId = id,
                                ComboId = c.ComboId,
                                Quantity = c.Quantity,
                            }).ToList();
                            context.OrderFoodComboDetails.AddRange(foodCombos);
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
        public static bool CheckoutComboOrder(CheckoutCombo order_request)
        {


            var order = order_request.Order;
            var order_combo = order_request.OrderCombo;
            var order_camping_gear = order_request.OrderCampingGear;
            var order_food = order_request.OrderFood;
            var order_foot_combo = order_request.OrderFoodCombo;


            try
            {
                using (var context = new GreenGardenContext())
                {

                    if (order_combo == null)
                    {
                        return false;
                    }
                    else
                    {

                        Order newOrder;


                        newOrder = new Order
                        {
                            CustomerId = order.CustomerId,
                            CustomerName = order.CustomerName,
                            OrderUsageDate = order.OrderUsageDate,
                            OrderDate = DateTime.Now,
                            Deposit = 0,
                            TotalAmount = order.TotalAmount,
                            AmountPayable = order.TotalAmount,
                            StatusOrder = false,
                            ActivityId = 1,
                            PhoneCustomer = order.PhoneCustomer
                        };

                        // Add the order and save to the database
                        context.Orders.Add(newOrder);
                        context.SaveChanges();

                        int id = newOrder.OrderId;


                        List<OrderComboDetail> tickets = order_combo.Select(t => new OrderComboDetail
                        {
                            ComboId = t.ComboId,
                            OrderId = id,
                            Quantity = t.Quantity,
                        }).ToList();
                        context.OrderComboDetails.AddRange(tickets);

                        if (order_camping_gear != null)
                        {
                            List<OrderCampingGearDetail> gears = order_camping_gear.Select(g => new OrderCampingGearDetail
                            {
                                GearId = g.GearId,
                                Quantity = g.Quantity,
                                OrderId = id,
                            }).ToList();
                            context.OrderCampingGearDetails.AddRange(gears);
                            context.SaveChanges();

                        }
                        if (order_food != null)
                        {
                            List<OrderFoodDetail> foods = order_food.Select(f => new OrderFoodDetail
                            {
                                OrderId = id,
                                ItemId = f.ItemId,
                                Quantity = f.Quantity,
                                Description = f.Description,
                            }).ToList();
                            context.OrderFoodDetails.AddRange(foods);
                            context.SaveChanges();

                        }
                        if (order_foot_combo != null)
                        {
                            List<OrderFoodComboDetail> foodCombos = order_foot_combo.Select(c => new OrderFoodComboDetail
                            {
                                OrderId = id,
                                ComboId = c.ComboId,
                                Quantity = c.Quantity,
                            }).ToList();
                            context.OrderFoodComboDetails.AddRange(foodCombos);
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
        public static bool UpdateActivity(int orderId)
        {
            try
            {
                using (var context = new GreenGardenContext())
                {
                    // Tìm đơn hàng theo OrderId
                    var order = context.Orders.FirstOrDefault(o => o.OrderId == orderId);

                    if (order != null)
                    {
                        // Cập nhật ActivityId thành 1002
                        order.ActivityId = 1002;

                        // Lưu thay đổi vào cơ sở dữ liệu
                        context.SaveChanges();
                        return true; // Trả về true nếu cập nhật thành công
                    }
                    else
                    {
                        return false; // Trả về false nếu không tìm thấy đơn hàng
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật ActivityId: " + ex.Message);
            }
        }
    }
}
