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
    public class UserDAO
    {
        private static GreenGardenContext context = new GreenGardenContext();
        public static List<UserDTO> GetAllUsers()
        {
            
                // Retrieve all users from the database and map them to UserDTO
                var users = context.Users
                .Include(user => user.Role)
                .Select(user => new UserDTO
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    DateOfBirth = user.DateOfBirth,
                    Gender = user.Gender,
                    ProfilePictureUrl = user.ProfilePictureUrl,
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt,
                    RoleName = user.Role.RoleName
                }).ToList();

                return users;
            
        }
    }
}
