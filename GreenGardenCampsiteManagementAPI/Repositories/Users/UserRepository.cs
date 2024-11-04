using BusinessObject.DTOs;
using DataAccess.DAO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Users
{
    public class UserRepository: IUserRepository
    {
        public List<UserDTO> GetAllUsers()
        {
            return UserDAO.GetAllUsers();
        }
        public List<UserDTO> GetAllEmployees()
        {
            return UserDAO.GetAllEmployees();
        }

        public List<UserDTO> GetAllCustomers()
        {
            return UserDAO.GetAllCustomers();
        }

        public UserDTO GetUserById(int userId)
        {
            return UserDAO.GetUserById(userId);
        }

        public bool AddEmployee(AddUserDTO newEmployeeDto, IConfiguration configuration)
        {
            return UserDAO.AddEmployee(newEmployeeDto, configuration);
        }

        public bool AddCustomer(AddUserDTO newCustomerDto, IConfiguration configuration)
        {
            return UserDAO.AddCustomer(newCustomerDto, configuration);
        }

        public bool UpdateUser( UpdateUserDTO updatedUserDto)
        {
            return UserDAO.UpdateUser( updatedUserDto);
        }

        public bool DeleteUser(int userId, IConfiguration configuration)
        {
            return UserDAO.DeleteUser(userId, configuration);
        }

        public bool BlockUser(int userId, IConfiguration configuration)
        {
            return UserDAO.BlockUser(userId, configuration);
        }
    }
}
