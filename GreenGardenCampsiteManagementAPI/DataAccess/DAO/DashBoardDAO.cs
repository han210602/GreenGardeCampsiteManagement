﻿using BusinessObject.DTOs;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class DashBoardDAO
    {
        private static GreenGardenContext context = new GreenGardenContext();
        public static List<UserDTO> ListCustomer()
        {
            try
            {
                var list=new List<UserDTO>();
                var data=context.Orders.Include(o => o.Customer).Where(o=>o.CustomerId!=null&&o.Customer.IsActive==true).ToList();
                foreach (var item in data)
                {
                    if (list.FirstOrDefault(s => s.UserId == item.CustomerId) == null)
                    {
                        list.Add(new UserDTO()
                        {
                            UserId = item.CustomerId.Value,
                            FirstName = item.Customer.FirstName,
                            LastName = item.Customer.LastName,
                            Email = item.Customer.Email,
                            Password = item.Customer.Password,
                            PhoneNumber = item.Customer.PhoneNumber,
                            Address = item.Customer.Address,
                            DateOfBirth = item.Customer.DateOfBirth,
                            Gender = item.Customer.Gender,
                            ProfilePictureUrl = item.Customer.ProfilePictureUrl,
                            IsActive = item.Customer.IsActive,
                            CreatedAt = item.Customer.CreatedAt,

                        });
                    }
                }
                return list;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public static List<FoodAndDrinkDTO> ListFoodAndDrink()
        {
            throw new NotImplementedException();
        }

        public static decimal TotalAmount()
        {
            try
            {
                decimal totalamount = 0;
                foreach (var item in context.Orders)
                {
                    if (item.ActivityId == 3)
                    {
                        totalamount +=item.TotalAmount;

                    }else if (item.ActivityId == 1002)
                    {
                        totalamount += item.Deposit;
                    }
                    else if(item.ActivityId==2)
                    {
                        totalamount += item.Deposit;
                    }
                    else if(item.ActivityId==1)
                    {
                        totalamount += item.Deposit;
                    }
                }


                return totalamount;
            }catch (Exception ex) {
                throw new NotImplementedException(ex.ToString());


            }
        }

        public static decimal MoneyTotalDepositOrderOnline()
        {
            try
            {
                decimal totalamount = 0;
                foreach (var item in context.Orders)
                {
                   if(item.ActivityId == 1){
                        totalamount += item.Deposit;
                   }
                }


                return totalamount;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());


            }
        }
        public static int TotalDepositOrderOnline()
        {
            try
            {
                int totalamount = 0;
                foreach (var item in context.Orders)
                {
                    if (item.ActivityId == 1&&item.Deposit>0)
                    {
                        totalamount++;
                    }
                }


                return totalamount;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());


            }
        }

        public static int TotalOrderOnline()
        {
            try
            {
                int totalamount = 0;
                foreach (var item in context.Orders)
                {
                    if (item.ActivityId == 1)
                    {
                        totalamount++;
                    }
                }


                return totalamount;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());


            }
        }
        public static decimal MoneyTotalDepositOrderCancel()
        {
            try
            {
                decimal totalamount = 0;
                foreach (var item in context.Orders)
                {
                    if (item.ActivityId == 1002&&item.Deposit>0)
                    {
                        totalamount += item.Deposit;
                    }
                }


                return totalamount;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());


            }
        }
        public static int TotalOrderCancel()
        {
            try
            {
                int totalamount = 0;
                foreach (var item in context.Orders)
                {
                    if (item.ActivityId == 1002)
                    {
                        totalamount++;
                    }
                }


                return totalamount;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());


            }
        }
        public static int TotalDepositOrderCancel()
        {
            try
            {
                int totalamount = 0;
                foreach (var item in context.Orders)
                {
                    if (item.ActivityId == 1002&&item.Deposit>0)
                    {
                        totalamount++;
                    }
                }


                return totalamount;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());


            }
        }
        public static int TotalOrderCheckout()
        {
            try
            {
                int totalamount = 0;
                foreach (var item in context.Orders)
                {
                    if (item.ActivityId == 3)
                    {
                        totalamount++;
                    }
                }


                return totalamount;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());


            }
        }
        public static decimal MoneyTotalOrderCheckout()
        {
            try
            {
                decimal totalamount = 0;
                foreach (var item in context.Orders)
                {
                    if (item.ActivityId == 3)
                    {
                        totalamount += item.TotalAmount;
                    }
                }


                return totalamount;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());


            }
        }

        public static int TotalOrderUsing()
        {
            try
            {
                int totalamount = 0;
                foreach (var item in context.Orders)
                {
                    if (item.ActivityId == 2)
                    {
                        totalamount++;
                    }
                }


                return totalamount;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());


            }
        }
        public static int TotalEmployee()
        {
            throw new NotImplementedException();
        }

        public static int TotalDepositOrderUsing()
        {

            try
            {
                int totalamount = 0;
                foreach (var item in context.Orders)
                {
                    if (item.ActivityId == 2&&item.Deposit>0)
                    {
                        totalamount++;
                    }
                }


                return totalamount;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());


            }

        }
        public static decimal MoneyTotalDepositOrderUsing()
        {
            try
            {
                decimal totalamount = 0;
                foreach (var item in context.Orders)
                {
                    if (item.ActivityId == 2&&item.Deposit>0)
                    {
                        totalamount += item.Deposit;
                    }
                }


                return totalamount;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());


            }
        }

        public static ProfitDTO Profit(int datetime)
        {
            try
            {
                decimal TotalAmount = 0;
                int TotalOrderOnline = 0;
                int TotalDepositOrderOnline = 0;
                decimal MoneyTotalDepositOrderOnline = 0;
                int TotalOrderCancel = 0;
                int TotalDepositOrderCancel = 0;
                decimal MoneyTotalDepositOrderCancel = 0;
                int TotalOrderUsing = 0;
                int TotalDepositOrderUsing = 0;
                decimal MoneyTotalDepositOrderUsing = 0;
                int TotalOrderCheckout = 0;
                decimal MoneyTotalAmountOrderCheckout = 0;
                if (datetime==0)
                {
                    foreach (var item in context.Orders)
                    {
                        if (item.ActivityId == 3)
                        {
                            TotalAmount += item.TotalAmount;
                            MoneyTotalAmountOrderCheckout += item.TotalAmount;
                            TotalOrderCheckout++;

                        }
                        else if (item.ActivityId == 1002)
                        {
                            TotalAmount += item.Deposit;
                            TotalOrderCancel++;
                            if (item.Deposit > 0)
                            {
                                TotalDepositOrderCancel++;
                                MoneyTotalDepositOrderCancel += item.Deposit;
                            }
                        }
                        else if (item.ActivityId == 2)
                        {
                            TotalAmount += item.Deposit;
                            TotalOrderUsing++;
                            if (item.Deposit > 0)
                            {
                                TotalDepositOrderUsing++;
                                MoneyTotalDepositOrderUsing++;
                            }
                        }
                        else if (item.ActivityId == 1)
                        {
                            TotalAmount += item.Deposit;
                            TotalOrderOnline++;
                            if (item.Deposit > 0)
                            {
                                TotalDepositOrderOnline++;
                                MoneyTotalDepositOrderOnline += item.Deposit;
                            }
                        }
                       
                    }

                }
                else
                {
                    foreach (var item in context.Orders.Where(s=>
                     (s.OrderDate.Value.Month==datetime&&s.OrderDate.Value.Year==DateTime.Now.Year)
                    ||(s.OrderCheckoutDate.Value.Month==datetime && s.OrderCheckoutDate.Value.Year == DateTime.Now.Year)
                    ||(s.OrderUsageDate.Value.Month==datetime && s.OrderUsageDate.Value.Year == DateTime.Now.Year))
                    )
                    {
                        if (item.ActivityId == 3)
                        {
                            TotalAmount += item.TotalAmount;
                            MoneyTotalAmountOrderCheckout += item.TotalAmount;
                            TotalOrderCheckout++;

                        }
                        else if (item.ActivityId == 1002)
                        {
                            TotalAmount += item.Deposit;
                            TotalOrderCancel++;
                            if (item.Deposit > 0)
                            {
                                TotalDepositOrderCancel++;
                                MoneyTotalDepositOrderCancel += item.Deposit;
                            }
                        }
                        else if (item.ActivityId == 2)
                        {
                            TotalAmount += item.Deposit;
                            TotalOrderUsing++;
                            if (item.Deposit > 0)
                            {
                                TotalDepositOrderUsing++;
                                MoneyTotalDepositOrderUsing++;
                            }
                        }
                        else if (item.ActivityId == 1)
                        {
                            TotalAmount += item.Deposit;
                            TotalOrderOnline++;
                            if (item.Deposit > 0)
                            {
                                TotalDepositOrderOnline++;
                                MoneyTotalDepositOrderOnline += item.Deposit;
                            }
                        }

                    }
                }



                return new ProfitDTO()
                {
                    TotalAmount = TotalAmount,
                    TotalOrderOnline = TotalOrderOnline,
                    TotalDepositOrderOnline = TotalDepositOrderOnline,
                    MoneyTotalDepositOrderOnline = MoneyTotalDepositOrderOnline, 
                    TotalOrderCancel = TotalOrderCancel,
                    TotalDepositOrderCancel = TotalDepositOrderCancel,
                    MoneyTotalDepositOrderCancel = MoneyTotalDepositOrderCancel,
                    TotalOrderUsing = TotalOrderUsing,
                    TotalDepositOrderUsing = TotalDepositOrderUsing,
                    MoneyTotalDepositOrderUsing = MoneyTotalDepositOrderUsing                   ,
                    TotalOrderCheckout = TotalOrderCheckout,
                    MoneyTotalAmountOrderCheckout = MoneyTotalAmountOrderCheckout,


                };
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.ToString());


            }
        }    
    }
}
