using BusinessObject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DashBoard
{
   public interface  IDashBoardRepository
    {
        public ProfitDTO Profit(int datetime);
        public decimal TotalAmount();
        public int TotalOrderOnline();
        public int TotalDepositOrderOnline();
        public decimal MoneyTotalDepositOrderOnline();
        public int TotalOrderCancel();
        public int TotalDepositOrderCancel();
        public decimal MoneyTotalDepositOrderCancel();

        public int TotalOrderUsing();
        public int TotalDepositOrderUsing();

        public decimal MoneyTotalDepositOrderUsing();
        public int TotalOrderCheckout();
        public decimal MoneyTotalAmountOrderCheckout();


        public int TotalEmployee();
        public List<UserDTO> ListCustomer();
        public List<FoodAndDrinkDTO> ListFoodAndDrink();


    }
}
