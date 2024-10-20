using System.ComponentModel.DataAnnotations;

namespace GreenGardenClient.Models
{
    public class UserRegisterVM
    {

        [Required(ErrorMessage = "First Name là bắt buộc.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name là bắt buộc.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc.")]
        [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp.")]
        public string ConfirmPassword { get; set; }
    }
}
