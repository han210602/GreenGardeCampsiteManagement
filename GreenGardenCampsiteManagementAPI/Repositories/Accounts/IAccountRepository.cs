using BusinessObject.DTOs;

namespace Repositories.Accounts
{
    public interface IAccountRepository
    {
        List<ViewUserDTO> GetAllAccount();
        ViewUserDTO GetAccountById(int id);
        string Login(AccountDTO a);
        bool SendResetPassword(string email);
        Task<string> Register(Register a, string enteredCode);
        Task<string> SendVerificationCode(string email);
        Task<string> UpdateProfile(UpdateProfile updateProfile);
        bool ChangePassword(ChangePassword changePasswordDto);



    }
}
