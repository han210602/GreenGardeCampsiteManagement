using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class AccountDAO
    {
        private static List<Account> accounts = new List<Account>
        {
            new Account(){
            AccountId = 1,
            Email="hoangndhe164015@fpt.edu.vn",
            Password="123456",
            Fullname="Nguyen Dang Hoang"
            },
            new Account(){
            AccountId = 2,
            Email="hoangndhe164015@fpt.edu.vn",
            Password="123456",
            Fullname="Nguyen Dang Hoang"
            },
            
        };
        public static void CreateAccount(Account a)
        {
            accounts.Add(a);
        }

        public static List<Account> GetAllAccount()
        {
            return accounts;
        }
    }
}
