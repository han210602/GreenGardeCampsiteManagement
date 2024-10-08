using BusinessObject.DTOs;
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

      
        public AccountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<ViewUserDTO> GetAllAccount()
        {
            return AccountDAO.GetAllAccounts();
        }

        public string Login(AccountDTO a)
        {
           
            return AccountDAO.Login(a, _configuration);
        }

        public async Task<string> SendResetPassword(string email)
        {
            
            return await AccountDAO.SendResetPassword(email, _configuration);
        }

        public async Task<string> Register(Register a, string enteredCode)
        {
            
            return await AccountDAO.Register(a, enteredCode, _configuration);
        }

        public async Task<string> SendVerificationCode(string email)
        {
            
            return await AccountDAO.SendVerificationCode(email, _configuration);
        }
    }
}
