using BusinessObject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IReponsitory
{
    public interface IUserReponsitory
    {
        //void CreateAccount(Account a);
        List<UserDTO> GetAllUser();
        
    }
}
