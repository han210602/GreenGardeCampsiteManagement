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
                    // Define the JWT claims
                    var claims = new[]
                    {
                new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("Email", user.Email)
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

                    // Create a response DTO
                    var response = new LoginResponseDTO
                    {
                        Token = tokenString,
                        UserName = user.FirstName + " " + user.LastName,
                        Email = user.Email,
                        Phone = user.PhoneNumber
                    };

                    // Return the response as a JSON string
                    return JsonSerializer.Serialize(response);
                }
            }

            throw new Exception("Invalid email or password.");
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
