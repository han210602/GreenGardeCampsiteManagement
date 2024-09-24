using BusinessObject.Models;
using DataAccess.DAO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Accounts
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IConfiguration _configuration;

        // Inject IConfiguration to pass it to AccountDAO
        public AccountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void CreateAccount(Account a)
        {
            AccountDAO.CreateAccount(a);
        }
        public List<Account> GetAllAccount()
        {
            return AccountDAO.GetAllAccount();
        }
        public string Login(AccountDTO a)
        {
            // Call the static Login method in AccountDAO and pass _configuration
            return AccountDAO.Login(a, _configuration);
        }
        public Account GetAccountById(int accountId)
        {
            return AccountDAO.GetAccountById(accountId);
        }

        public void UpdateAccount(Account updatedAccount)
        {
            AccountDAO.UpdateAccount(updatedAccount);
        }
        public void DeleteAccount(int accountId)
        {
            AccountDAO.DeleteAccount(accountId);
        }
    }
}
