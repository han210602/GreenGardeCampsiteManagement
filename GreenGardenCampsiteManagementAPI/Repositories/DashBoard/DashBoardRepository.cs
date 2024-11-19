using BusinessObject.DTOs;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.DashBoard
{
    public class DashBoardRepository : IDashBoardRepository
    {
        public List<UserDTO> ListCustomer()
        {
            return DashBoardDAO.ListCustomer();
        }

        public List<FoodAndDrinkDTO> ListFoodAndDrink()
        {
            throw new NotImplementedException();
        }

        public decimal MoneyTotalAmountOrderCheckout()
        {
            return DashBoardDAO.MoneyTotalOrderCheckout();
        }

        public decimal MoneyTotalDepositOrderCancel()
        {
            return DashBoardDAO.MoneyTotalDepositOrderCancel();
        }

        public decimal MoneyTotalDepositOrderOnline()
        {
            return DashBoardDAO.MoneyTotalDepositOrderOnline();
                
        }

        public decimal MoneyTotalDepositOrderUsing()
        {
            return DashBoardDAO.MoneyTotalDepositOrderUsing();
        }

        public decimal TotalAmount()
        {
            return DashBoardDAO.TotalAmount();
        }

        public int TotalDepositOrderCancel()
        {
            return DashBoardDAO.TotalDepositOrderCancel();        
        }

        public int TotalDepositOrderOnline()
        {
            return DashBoardDAO.TotalDepositOrderOnline();
        }

        public int TotalDepositOrderUsing()
        {
            return DashBoardDAO.TotalDepositOrderUsing();
        }

        public int TotalEmployee()
        {
            throw new NotImplementedException();
        }

        public int TotalOrderCancel()
        {
            return DashBoardDAO.TotalOrderCancel();
                
        }

        public int TotalOrderCheckout()
        {
            return DashBoardDAO.TotalOrderCheckout();
        }

        public int TotalOrderOnline()
        {
            return DashBoardDAO.TotalOrderOnline();
        }

        public int TotalOrderUsing()
        {
            return DashBoardDAO.TotalOrderUsing();
        }
    }
}
