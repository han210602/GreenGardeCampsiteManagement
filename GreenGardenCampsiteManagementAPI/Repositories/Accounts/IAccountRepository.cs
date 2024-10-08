using BusinessObject.DTOs;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Accounts
{
    public interface IAccountRepository
    {
        List<ViewUserDTO> GetAllAccount();
        string Login(AccountDTO a);
        Task<string> SendResetPassword(string email);
        Task<string> Register(Register a, string enteredCode); 
        Task<string> SendVerificationCode(string email); 
    }
}
