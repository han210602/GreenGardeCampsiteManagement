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
            DashBoardDAO dashBoardDAO = new DashBoardDAO(); 
            return dashBoardDAO.ListCustomer();
        }


        public ProfitDTO Profit(string datetime)
        {
            DashBoardDAO dashBoardDAO = new DashBoardDAO();

            return dashBoardDAO.Profit(datetime);
        }

       
    }
}
