using BusinessObject.DTOs;
using DataAccess.DAO;
using Microsoft.Extensions.Configuration;
using Repositories.IReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Reponsitory
{
    public class UserReponsitory : IUserReponsitory
    {
         private readonly IConfiguration _configuration;

        public UserReponsitory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<UserDTO> GetAllUser()
        {
            return UserDAO.GetAllUsers();
        }

    }
}
