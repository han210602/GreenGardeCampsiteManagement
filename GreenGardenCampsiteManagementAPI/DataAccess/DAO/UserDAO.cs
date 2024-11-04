using BusinessObject.DTOs;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class UserDAO
    {
        private static GreenGardenContext context = new GreenGardenContext();

        public static List<UserDTO> GetAllUsers()
        {
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
        public static List<UserDTO> GetAllEmployees()
        {
            var employees = context.Users
                .Include(user => user.Role)
                .Where(user => user.RoleId == 2) // Filter where RoleId is 2
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
                })
                .ToList();

            return employees;
        }
        public static List<UserDTO> GetAllCustomers()
        {
            var customer = context.Users
                .Include(user => user.Role)
                .Where(user => user.RoleId == 3) // Filter where RoleId is 2
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
                })
                .ToList();

            return customer;
        }


        public static UserDTO GetUserById(int userId)
        {
            var user = context.Users
                .Include(u => u.Role)
                .Where(u => u.UserId == userId)
                .Select(u => new UserDTO
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Password = u.Password,
                    PhoneNumber = u.PhoneNumber,
                    Address = u.Address,
                    DateOfBirth = u.DateOfBirth,
                    Gender = u.Gender,
                    ProfilePictureUrl = u.ProfilePictureUrl,
                    IsActive = u.IsActive,
                    CreatedAt = u.CreatedAt,
                    RoleName = u.Role.RoleName
                })
                .FirstOrDefault();

            return user;
        }
        public static bool AddEmployee(AddUserDTO newEmployeeDto, IConfiguration configuration)
        {
            try
            {
                // Check if the email already exists
                var existingUser = context.Users.SingleOrDefault(u => u.Email == newEmployeeDto.Email);
                if (existingUser != null)
                {
                    Console.WriteLine("Email already registered.");
                    return false; // Indicate failure
                }

                // Map UserDTO to User entity
                var newEmployee = new User
                {
                    FirstName = newEmployeeDto.FirstName,
                    LastName = newEmployeeDto.LastName,
                    Email = newEmployeeDto.Email,
                    Password = newEmployeeDto.Password, // Ensure to hash this password
                    PhoneNumber = newEmployeeDto.PhoneNumber,
                    Address = newEmployeeDto.Address,
                    DateOfBirth = newEmployeeDto.DateOfBirth,
                    Gender = newEmployeeDto.Gender,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    RoleId = 2 // Set RoleId to 2 for employees
                };

                // Add the new employee to the context
                context.Users.Add(newEmployee);

                // Save changes to the database
                context.SaveChanges();

                // Send email with login credentials
                SendEmailWithCredentials(newEmployee.Email, newEmployee.Password, configuration).Wait();

                return true; // Indicate success
            }
            catch (Exception ex)
            {
                // Handle exceptions (logging, etc.)
                Console.WriteLine($"Error adding employee: {ex.Message}");
                return false; // Indicate failure
            }
        }

        // Similar update for AddCustomer
        public static bool AddCustomer(AddUserDTO newCustomerDto, IConfiguration configuration)
        {
            try
            {
                // Check if the email already exists
                var existingUser = context.Users.SingleOrDefault(u => u.Email == newCustomerDto.Email);
                if (existingUser != null)
                {
                    Console.WriteLine("Email already registered.");
                    return false; // Indicate failure
                }

                // Map UserDTO to User entity
                var newCustomer = new User
                {
                    FirstName = newCustomerDto.FirstName,
                    LastName = newCustomerDto.LastName,
                    Email = newCustomerDto.Email,
                    Password = newCustomerDto.Password, // Ensure to hash this password
                    PhoneNumber = newCustomerDto.PhoneNumber,
                    Address = newCustomerDto.Address,
                    DateOfBirth = newCustomerDto.DateOfBirth,
                    Gender = newCustomerDto.Gender,
                    IsActive = newCustomerDto.IsActive,
                    CreatedAt = DateTime.Now,
                    RoleId = 3 // Set RoleId to 3 for customers
                };

                // Add the new customer to the context
                context.Users.Add(newCustomer);

                // Save changes to the database
                context.SaveChanges();

                // Send email with login credentials
                SendEmailWithCredentials(newCustomerDto.Email, newCustomerDto.Password, configuration).Wait();

                return true; // Indicate success
            }
            catch (Exception ex)
            {
                // Handle exceptions (logging, etc.)
                Console.WriteLine($"Error adding customer: {ex.Message}");
                return false; // Indicate failure
            }
        }


        // Method to send email with login credentials
        private static async Task SendEmailWithCredentials(string email, string password, IConfiguration configuration)
        {
            var fromAddress = new MailAddress("CustomerService94321@gmail.com", "SongQue Green Garden");
            var toAddress = new MailAddress(email);
            const string fromPassword = "lwrtmwkgshlqaycp";
            const string subject = "Welcome to SongQue Green Garden - Your Account Details";
            string body = $"Hello, \n\nYour account has been created successfully.\n\n" +
                          $"Email: {email}\n" +
                          $"Password: {password}\n\n" +
                          "Please log in and change your password as soon as possible.\n\nThank you!";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                await smtp.SendMailAsync(message);
            }
        }

        public static bool UpdateUser(UpdateUserDTO updatedUserDto)
        {
            try
            {
                // Find the user by UserId
                var existingUser = context.Users.FirstOrDefault(u => u.UserId == updatedUserDto.UserId);

                if (existingUser == null)
                {
                    Console.WriteLine("User not found.");
                    return false; // User not found
                }
                existingUser.UserId = updatedUserDto.UserId;
                // Update the existing user properties
                existingUser.FirstName = updatedUserDto.FirstName;
                existingUser.LastName = updatedUserDto.LastName;
                existingUser.Email = updatedUserDto.Email;
                existingUser.Password = updatedUserDto.Password;
                existingUser.PhoneNumber = updatedUserDto.PhoneNumber;
                existingUser.Address = updatedUserDto.Address;
                existingUser.Gender = updatedUserDto.Gender;
                existingUser.Address = updatedUserDto.Address;
                existingUser.DateOfBirth = updatedUserDto.DateOfBirth;
                existingUser.IsActive = updatedUserDto.IsActive; 

                // Save changes to the database
                context.SaveChanges();

                return true; // Indicate success
            }
            catch (Exception ex)
            {
                // Handle exceptions (logging, etc.)
                Console.WriteLine($"Error updating user: {ex.Message}");
                return false; // Indicate failure
            }
        }
        public static bool DeleteUser(int userId, IConfiguration configuration)
        {
            try
            {
                // Retrieve the employee by userId
                var user = context.Users.SingleOrDefault(u => u.UserId == userId);

                if (user == null)
                {
                    Console.WriteLine("User not found.");
                    return false; // Employee does not exist
                }

                // Prepare email notification
                string deleteSubject = "Thông báo tài khoản đã bị xóa ";
                string deleteBody = "\r\nKính gửi Nhân viên,\\n\\nTài khoản của bạn đã bị xóa vì bạn đã nghỉ việc.\\n\\nCảm ơn bạn đã dành thời gian làm việc với chúng tôi.";
                SendEmailAsync(user.Email, deleteSubject, deleteBody).Wait();

                // Remove the employee from the context
                context.Users.Remove(user);

                // Save changes to the database
                context.SaveChanges();

                return true; // Deletion successful
            }
            catch (Exception ex)
            {
                // Handle exceptions (logging, etc.)
                Console.WriteLine($"Error deleting employee: {ex.Message}");
                return false; // Deletion failed
            }
        }

        public static bool BlockUser(int userId, IConfiguration configuration)
        {
            try
            {
                // Retrieve the user by userId
                var user = context.Users.SingleOrDefault(u => u.UserId == userId);

                if (user == null)
                {
                    Console.WriteLine("User not found.");
                    return false; // User does not exist
                }

                // Set IsActive to false to block the user
                user.IsActive = false;

                // Send notification email to the user
                string blockSubject = "Thông báo chặn tài khoản";
                string blockBody = "Kính gửi khách hàng,\n\nTài khoản của bạn đã bị chặn do vi phạm chính sách hoặc các lý do khác. Vui lòng liên hệ với bộ phận hỗ trợ để biết thêm thông tin.\n\nCảm ơn bạn.";
                SendEmailAsync(user.Email, blockSubject, blockBody).Wait();

                // Save changes to the database
                context.SaveChanges();

                return true; // Blocking successful
            }
            catch (Exception ex)
            {
                // Handle exceptions (logging, etc.)
                Console.WriteLine($"Error blocking user: {ex.Message}");
                return false; // Blocking failed
            }
        }

        // Consolidated method to send email
        private static async Task SendEmailAsync(string email, string subject, string body)
        {
            var fromAddress = new MailAddress("CustomerService94321@gmail.com", "SongQue Green Garden");
            var toAddress = new MailAddress(email);
            const string fromPassword = "lwrtmwkgshlqaycp"; // Use secure methods to store passwords

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                await smtp.SendMailAsync(message);
            }
        }


    }
}
