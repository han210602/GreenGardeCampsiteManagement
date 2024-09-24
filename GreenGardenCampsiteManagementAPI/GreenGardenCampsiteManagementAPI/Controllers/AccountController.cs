using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Accounts;

namespace GreenGardenCampsiteManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountRepository _repo;
        public AccountController(IAccountRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetAllAccounts")]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccounts()
        {
            return _repo.GetAllAccount();
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] AccountDTO loginRequest)
        {
            try
            {
                var loginResponse = _repo.Login(loginRequest);


                if (loginResponse == null)
                {
                    return Unauthorized("Invalid email or password.");
                }


                return Ok(loginResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("Register")]
        public ActionResult<Account> Register(Account request)
        {
            Account a = new Account
            {
                AccountId=request.AccountId,
                Email = request.Email,
                Password = request.Password,
                Fullname = request.Fullname,
                
            };
            _repo.CreateAccount(a);
            return NoContent();
        }
        [HttpGet("{id}")]
        public IActionResult GetAccountById(int id)
        {
            var account = _repo.GetAccountById(id);
            if (account == null)
            {
                return NotFound("Account not found.");
            }
            return Ok(account);
        }


        [HttpPut("UpdateAccount")]
        public IActionResult UpdateAccount(int id,[FromBody] Account updatedAccount)
        {
            var existingAccount = _repo.GetAccountById(id);
            if (existingAccount == null)
            {
                return NotFound("Account not found.");
            }

            existingAccount.Email = updatedAccount.Email;
            existingAccount.Password = updatedAccount.Password;
            existingAccount.Fullname = updatedAccount.Fullname;

            _repo.UpdateAccount(existingAccount);
            return NoContent();
        }

   
        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id)
        {
            var account = _repo.GetAccountById(id);
            if (account == null)
            {
                return NotFound("Account not found.");
            }

            _repo.DeleteAccount(id);
            return NoContent();
        }
    }
}
