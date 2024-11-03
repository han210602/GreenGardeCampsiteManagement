﻿using BusinessObject.Models;
using BusinessObject.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Collections.Concurrent;

namespace DataAccess.DAO
{
    public class AccountDAO
    {
        // Assuming you have a list of User objects
        private static Dictionary<string, int> loginAttempts = new Dictionary<string, int>();

        public static string Login(AccountDTO a, IConfiguration configuration)
        {
            using (var context = new GreenGardenContext())
            {
                // Retrieve the user by email
                var user = context.Users.SingleOrDefault(u => u.Email == a.Email);
                // Check if the user exists and is active
                if (user == null || user.IsActive == false)
                {
                    throw new Exception("Invalid email or account inactive.");
                }

                // Check if the password is correct
                if (user.Password == a.Password)
                {
                    // Reset login attempts on success
                    if (loginAttempts.ContainsKey(a.Email))
                    {
                        loginAttempts[a.Email] = 0;
                    }

                    // Define JWT claims, including RoleId
                    var claims = new[]
                    {
                new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("Email", user.Email),
                new Claim("RoleId", user.RoleId.ToString())
            };

                    // Create JWT token
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        issuer: configuration["Jwt:Issuer"],
                        audience: configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(60),
                        signingCredentials: signIn);

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                    var response = new LoginResponseDTO
                    {
                        Token = tokenString,
                        FullName = user.FirstName + " " + user.LastName,
                        UserId = user.UserId,
                        ProfilePictureUrl = user.ProfilePictureUrl,
                        Email = user.Email,
                        Password = user.Password,
                        Phone = user.PhoneNumber,
                        RoleId = (int)user.RoleId
                    };

                    return JsonSerializer.Serialize(response);
                }
                else
                {
                    // Track login attempts
                    if (!loginAttempts.ContainsKey(a.Email))
                    {
                        loginAttempts[a.Email] = 1;
                    }
                    else
                    {
                        loginAttempts[a.Email]++;
                    }

                    // Throw an error message if the login attempt fails
                    throw new Exception("Invalid email or password.");
                }
            }
        }


        private static readonly ConcurrentDictionary<string, (string Code, DateTime Expiration)> VerificationCodes = new ConcurrentDictionary<string, (string, DateTime)>();

        public static async Task<string> Register(Register registerDto, string enteredCode, IConfiguration configuration)
        {
            using (var context = new GreenGardenContext())
            {
                if (!VerificationCodes.TryGetValue(registerDto.Email, out var storedCodeInfo) ||
                    storedCodeInfo.Code != enteredCode)
                {
                    throw new Exception("Mã xác thực không đúng.");
                }

                // Kiểm tra mã đã hết hạn chưa
                if (DateTime.UtcNow > storedCodeInfo.Expiration)
                {
                    VerificationCodes.TryRemove(registerDto.Email, out _); // Xóa mã xác thực đã hết hạn
                    throw new Exception("Mã xác thực đã hết hạn.");
                }

                // Kiểm tra email đã tồn tại chưa
                var existingUser = context.Users.SingleOrDefault(u => u.Email == registerDto.Email);
                if (existingUser != null)
                {
                    throw new Exception("Email đã được đăng ký.");
                }

                var newUser = new User
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    PhoneNumber = registerDto.PhoneNumber,
                    Email = registerDto.Email,
                    Password = registerDto.Password,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    RoleId = 3
                };

                try
                {
                    context.Users.Add(newUser);
                    await context.SaveChangesAsync();

                    // Xóa mã xác thực khi đăng ký thành công
                    VerificationCodes.TryRemove(registerDto.Email, out _);

                    var response = new
                    {
                        Message = "Đăng kí thành công",
                        UserId = newUser.UserId,
                        Email = newUser.Email,
                        PhoneNumber = newUser.PhoneNumber,
                        CreatedAt = newUser.CreatedAt,
                        RoleId = newUser.RoleId
                    };

                    return JsonSerializer.Serialize(response);
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception("Đã xảy ra lỗi khi lưu thay đổi: " + ex.InnerException?.Message);
                }
            }
        }

       public static async Task<string> SendVerificationCode(string email, IConfiguration configuration)
        {
            var verificationCode = GenerateVerificationCode();

            // Thiết lập thời gian hết hạn là 120 giây (2 phút)
            var expirationTime = DateTime.UtcNow.AddSeconds(120);
            VerificationCodes[email] = (verificationCode, expirationTime);

            await SendVerificationEmail(email, verificationCode, configuration);
            return "Mã xác thực đã được gửi đến email của bạn.";
        }

        private static string GenerateVerificationCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); // Mã xác thực 6 chữ số
        }

        private static async Task SendVerificationEmail(string email, string verificationCode, IConfiguration configuration)
        {
            var fromAddress = new MailAddress("CustomerService94321@gmail.com", "SongQue Green Garden");
            var toAddress = new MailAddress(email);
            const string fromPassword = "lwrtmwkgshlqaycp";
            const string subject = "Mã xác thực đăng ký";
            string body = $"Mã xác thực của bạn là: {verificationCode}";

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


        public static async Task<string> SendResetPassword(string email, IConfiguration configuration)
        {
            using (var context = new GreenGardenContext())
            {

                var user = context.Users.SingleOrDefault(u => u.Email == email);
                if (user == null)
                {
                    throw new Exception("Email không tồn tại.");
                }

                Random random = new Random();
                int newPassword = random.Next(100000, 1000000);


                user.Password = newPassword.ToString();
                context.Users.Update(user);
                context.SaveChanges();


                var fromAddress = new MailAddress("CustomerService94321@gmail.com", "Dịch vụ Khách hàng");
                var toAddress = new MailAddress(email);
                const string fromPassword = "lwrtmwkgshlqaycp";
                const string subject = "Reset Password";
                string body = $"Mật khẩu mới của bạn là: {newPassword}";

                // Cấu hình SmtpClient
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
                    try
                    {
                        await smtp.SendMailAsync(message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to send email: {ex.Message}");
                        throw new Exception("Gửi email thất bại.");
                    }
                }

                Console.WriteLine("Email sent successfully!");
                return JsonSerializer.Serialize(new { Message = "Email đặt lại mật khẩu đã được gửi đến bạn." });
            }
        }

        public static List<ViewUserDTO> GetAllAccounts()
        {
            using (var context = new GreenGardenContext())
            {
                // Retrieve all users from the database and map them to UserDTO
                var users = context.Users.Select(user => new ViewUserDTO
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
                    RoleId = user.RoleId
                }).ToList();

                return users;
            }
        }
        public static ViewUserDTO GetAccountById(int userId)
        {
            using (var context = new GreenGardenContext())
            {
                // Retrieve the user with the given UserId from the database and map it to ViewUserDTO
                var user = context.Users
                    .Where(u => u.UserId == userId)
                    .Select(user => new ViewUserDTO
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
                        RoleId = user.RoleId
                    })
                    .FirstOrDefault(); // Returns null if no user is found

                return user;
            }
        }
        public static async Task<string> UpdateProfile(UpdateProfile updateProfileDto)
        {
            using (var context = new GreenGardenContext())
            {

                var user = await context.Users.SingleOrDefaultAsync(u => u.UserId == updateProfileDto.UserId);
                if (user == null)
                {
                    return "Người dùng không tồn tại.";
                }


                user.FirstName = updateProfileDto.FirstName;
                user.LastName = updateProfileDto.LastName;
                user.Email = updateProfileDto.Email;
                user.PhoneNumber = updateProfileDto.PhoneNumber;
                user.Address = updateProfileDto.Address;
                user.DateOfBirth = updateProfileDto.DateOfBirth;
                user.Gender = updateProfileDto.Gender;

                try
                {

                    await context.SaveChangesAsync();
                    return "Cập nhật thông tin thành công.";
                }
                catch (DbUpdateException ex)
                {

                    throw new Exception("Đã xảy ra lỗi khi cập nhật thông tin: " + ex.InnerException?.Message);
                }
            }
        }

        public static async Task<string> ChangePassword(ChangePassword changePasswordDto)
        {
            using (var context = new GreenGardenContext())
            {
                
                var user = await context.Users.SingleOrDefaultAsync(u => u.UserId == changePasswordDto.UserId);
                if (user == null)
                {
                    return "Người dùng không tồn tại."; 
                }

                if (user.Password != changePasswordDto.OldPassword)
                {
                    return "Mật khẩu cũ không đúng."; 
                }

                if (changePasswordDto.NewPassword != changePasswordDto.ConformPassword)
                {
                    return "Mật khẩu mới và xác nhận mật khẩu không khớp."; 
                }

                user.Password = changePasswordDto.NewPassword;

                try
                {
                    await context.SaveChangesAsync();
                    return "Cập nhật mật khẩu thành công."; 
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception("Đã xảy ra lỗi khi cập nhật mật khẩu: " + ex.InnerException?.Message);
                }
            }
        }

    }
}

