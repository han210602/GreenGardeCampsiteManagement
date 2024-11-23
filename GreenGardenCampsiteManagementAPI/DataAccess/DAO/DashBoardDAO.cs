using BusinessObject.DTOs;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class DashBoardDAO
    {
        // private static GreenGardenContext context = new GreenGardenContext();
        private static GreenGardenContext _context;
        public static void InitializeContext(GreenGardenContext context)
        {
            _context = context;
        }

        public static List<UserDTO> ListCustomer()
        {
            try
            {
                var list = new List<UserDTO>();
                var data = _context.Orders.Include(o => o.Customer).Where(o => o.CustomerId != null && o.Customer.IsActive == true).ToList();
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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }



        public static ProfitDTO Profit(string datetime)
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
                if (datetime.Equals("0"))
                {
                    foreach (var item in _context.Orders)
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
                    DateTime targetDate = DateTime.ParseExact(datetime, "yyyy-MM", CultureInfo.InvariantCulture);

                    // Filter orders directly using Year and Month comparison
                    var list = _context.Orders.Where(s =>
                        (s.OrderDate.HasValue && s.OrderDate.Value.Year == targetDate.Year && s.OrderDate.Value.Month == targetDate.Month) ||
                        (s.OrderCheckoutDate.HasValue && s.OrderCheckoutDate.Value.Year == targetDate.Year && s.OrderCheckoutDate.Value.Month == targetDate.Month) ||
                        (s.OrderUsageDate.HasValue && s.OrderUsageDate.Value.Year == targetDate.Year && s.OrderUsageDate.Value.Month == targetDate.Month))
                        .ToList();
                    foreach (var item in list)
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
                    MoneyTotalDepositOrderUsing = MoneyTotalDepositOrderUsing,
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