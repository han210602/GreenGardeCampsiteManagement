﻿using BusinessObject.DTOs;
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
        //void CreateAccount(Account a);
        List<ViewUserDTO> GetAllAccount();
        string Login(AccountDTO a);
    }
}
