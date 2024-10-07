//using BusinessObject.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    //public class AccountDAO
    //{
    //    private static List<Account> accounts = new List<Account>
    //    {
    //        new Account(){
    //        AccountId = 1,
    //        Email="tupche163707@fpt.edu.vn",
    //        Password="1",
    //        Fullname="Nguyen Dang Hoang"
    //        },
    //        new Account(){
    //        AccountId = 2,
    //        Email="hoangndhe164015@fpt.edu.vn",
    //        Password="123456",
    //        Fullname="Nguyen Dang Hoang"
    //        },
            
    //    };
    //    public static void CreateAccount(Account a)
    //    {
    //        accounts.Add(a);
    //    }
    //    public static string Login(AccountDTO a, IConfiguration configuration)
    //    {
    //        var user = accounts.SingleOrDefault(u => u.Email == a.Email && u.Password == a.Password);
    //        if (user != null)
    //        {
    //            var claims = new[]
    //            {
    //        new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
    //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    //        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
    //        new Claim("AccountId", user.AccountId.ToString()),
    //        new Claim("Email", user.Email)
    //        // You can add Role claim here if needed
    //    };

    //            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
    //            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //            var token = new JwtSecurityToken(
    //                configuration["Jwt:Issuer"],
    //                configuration["Jwt:Audience"],
    //                claims,
    //                expires: DateTime.UtcNow.AddMinutes(60),
    //                signingCredentials: signIn);

    //            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

                
    //            return tokenValue;
    //        }

    //        throw new Exception("Invalid email or password.");
    //    }

    //    public static List<Account> GetAllAccount()
    //    {
    //        return accounts;
    //    }
    //}
}
