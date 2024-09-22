using BusinessObject.Models;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Accounts
{
    public class AccountRepository : IAccountRepository
    {
        public void CreateAccount(Account a)
        {
            AccountDAO.CreateAccount(a);
        }
        public List<Account> GetAllAccount()
        {
            return AccountDAO.GetAllAccount();
        }
    }
}
