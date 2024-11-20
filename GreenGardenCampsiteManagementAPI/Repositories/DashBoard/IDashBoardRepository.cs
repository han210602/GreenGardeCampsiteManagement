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
        public ProfitDTO Profit(string datetime);

        public List<UserDTO> ListCustomer();


    }
}
