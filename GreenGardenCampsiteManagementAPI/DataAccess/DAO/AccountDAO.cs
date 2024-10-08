using BusinessObject.Models;
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
        public static string Login(AccountDTO a, IConfiguration configuration)
        {
            using (var context = new GreenGardenContext())
            {
                // Retrieve the user from the database context
                var user = context.Users.SingleOrDefault(u => u.Email == a.Email && u.Password == a.Password);

                if (user != null)
                {
                    // Define the JWT claims, including the RoleId
                    var claims = new[]
                    {
                new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("Email", user.Email),
                new Claim("RoleId", user.RoleId.ToString()) // Add RoleId claim
            };

                    // Create security key and signing credentials
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    // Create the JWT token
                    var token = new JwtSecurityToken(
                        issuer: configuration["Jwt:Issuer"],
                        audience: configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(60),
                        signingCredentials: signIn);

                    // Generate the token string
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                    // Create a response DTO with RoleId
                    var response = new LoginResponseDTO
                    {
                        Token = tokenString,
                        UserName = user.FirstName + " " + user.LastName,
                        Email = user.Email,
                        Phone = user.PhoneNumber,
                        RoleId = (int)user.RoleId // Add RoleId to response
                    };

                    // Return the response as a JSON string
                    return JsonSerializer.Serialize(response);
                }
            }

            throw new Exception("Invalid email or password.");
        }
        private static readonly ConcurrentDictionary<string, string> VerificationCodes = new ConcurrentDictionary<string, string>();

        public static async Task<string> Register(Register registerDto, string enteredCode, IConfiguration configuration)
        {
            using (var context = new GreenGardenContext())
            {
              
                if (!VerificationCodes.TryGetValue(registerDto.Email, out var verificationCode) || verificationCode != enteredCode)
                {
                    throw new Exception("Mã xác thực không đúng.");
                }

                
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
                    Password = registerDto.Password, // Đảm bảo rằng mật khẩu đã được băm
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    RoleId = 3
                };

                try
                {
                   
                    context.Users.Add(newUser);
                    await context.SaveChangesAsync();

                    
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

            
            VerificationCodes[email] = verificationCode;

           
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
            var fromAddress = new MailAddress("CustomerService94321@gmail.com", "Dịch vụ Khách hàng");
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

    }
}
