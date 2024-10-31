using BusinessObject.DTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Users
{
    public interface IUserRepository
    {
        List<UserDTO> GetAllUsers();
        List<UserDTO> GetAllEmployees();
        List<UserDTO> GetAllCustomers();
        UserDTO GetUserById(int userId);
        bool AddEmployee(AddUserDTO newEmployeeDto, IConfiguration configuration);
        bool AddCustomer(AddUserDTO newCustomerDto, IConfiguration configuration);
        bool UpdateUser(UpdateUserDTO updatedUserDto);
        bool DeleteUser(int userId, IConfiguration configuration);
        bool BlockUser(int userId, IConfiguration configuration);
    }
}
