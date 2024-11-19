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
                        totalamount += totalamount;
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
    }
}
