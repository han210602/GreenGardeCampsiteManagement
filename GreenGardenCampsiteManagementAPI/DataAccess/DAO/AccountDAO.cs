using BusinessObject.DTOs;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

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
                if (user == null)
                {
                    throw new Exception("Địa chỉ email không tồn tại.");
                }
                if (user.IsActive == false)
                {
                    throw new Exception("Tài khoản của bạn đã bị khóa.");

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
                    throw new Exception("Mật khẩu không đúng");
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


        public static bool SendResetPassword(string email, IConfiguration configuration)
        {
            try
            {
                using (var context = new GreenGardenContext())
                {
                    // Kiểm tra sự tồn tại của email trong cơ sở dữ liệu
                    var user = context.Users.SingleOrDefault(u => u.Email == email);
                    if (user == null)
                    {
                        return false;
                    }

                    // Tạo mật khẩu mới ngẫu nhiên
                    Random random = new Random();
                    int newPassword = random.Next(100000, 1000000);

                    // Cập nhật mật khẩu mới vào cơ sở dữ liệu
                    user.Password = newPassword.ToString();
                    context.Users.Update(user);
                    context.SaveChanges();

                    // Cấu hình gửi email
                    var fromAddress = new MailAddress("CustomerService94321@gmail.com", "Dịch vụ Khách hàng");
                    var toAddress = new MailAddress(email);
                    const string fromPassword = "lwrtmwkgshlqaycp";  // Mật khẩu ứng dụng của bạn
                    const string subject = "Reset Password";
                    string body = $"Mật khẩu mới của bạn là: {newPassword}";

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
                        // Gửi email và xử lý ngoại lệ
                        try
                        {
                            smtp.Send(message);
                            Console.WriteLine("Email đã được gửi thành công!");
                        }
                        catch (Exception ex)
                        {
                            // Xử lý lỗi khi gửi email
                            Console.WriteLine($"Gửi email thất bại: {ex.Message}");
                            throw new Exception("Gửi email thất bại.");
                        }
                    }
                }

                return true;  // Trả về true nếu gửi email thành công và cập nhật mật khẩu thành công
            }
            catch (Exception ex)
            {
                // Xử lý lỗi chung và gửi thông báo lỗi
                Console.WriteLine($"Lỗi: {ex.Message}");
                return false;  // Trả về false nếu có lỗi
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
                user.ProfilePictureUrl = updateProfileDto.ProfilePictureUrl;

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

        public static bool ChangePassword(ChangePassword changePasswordDto)
        {
            using (var context = new GreenGardenContext())
            {
                var user = context.Users.SingleOrDefault(u => u.UserId == changePasswordDto.UserId);
                if (user == null)
                {
                    // Nếu người dùng không tồn tại, trả về false.
                    return false;
                }

                if (user.Password != changePasswordDto.OldPassword)
                {
                    // Nếu mật khẩu cũ không đúng, trả về false.
                    return false;
                }

                if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
                {
                    // Nếu mật khẩu mới và xác nhận không khớp, trả về false.
                    return false;
                }

                user.Password = changePasswordDto.NewPassword;

                try
                {
                    context.SaveChanges(); // Gọi SaveChanges đồng bộ
                                           // Nếu thay đổi mật khẩu thành công, trả về true.
                    return true;
                }
                catch (DbUpdateException ex)
                {
                    // Nếu có lỗi khi lưu vào cơ sở dữ liệu, ném ngoại lệ.
                    throw new Exception("Đã xảy ra lỗi khi cập nhật mật khẩu: " + ex.InnerException?.Message);
                }
            }
        }


    }
}

