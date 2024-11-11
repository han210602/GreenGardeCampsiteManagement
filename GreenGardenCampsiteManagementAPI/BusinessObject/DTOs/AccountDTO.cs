using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class AccountDTO
    {
        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Định dạng email không hợp lệ.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        public string Password { get; set; } = null!;
    }

    public class ViewUserDTO
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Họ không được để trống.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Tên không được để trống.")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Định dạng email không hợp lệ.")]
        public string Email { get; set; } = null!;

        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        public string Password { get; set; } = null!;

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Số điện thoại phải có đúng 10 ký tự.")]
        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? RoleId { get; set; }
    }

    public class ChangePassword
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Mật khẩu cũ là bắt buộc.")]
        public string OldPassword { get; set; } = null!;

        [Required(ErrorMessage = "Mật khẩu mới là bắt buộc.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        public string NewPassword { get; set; } = null!;

        [Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc.")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu và mật khẩu xác nhận không khớp.")]
        public string ConformPassword { get; set; } = null!;
    }

    public class UpdateProfile
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Họ không được để trống.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Tên không được để trống.")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Định dạng email không hợp lệ.")]
        public string Email { get; set; } = null!;

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Số điện thoại phải có đúng 10 ký tự.")]
        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }

    public class Register
    {
        [Required(ErrorMessage = "Họ không được để trống.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Tên không được để trống.")]
        public string LastName { get; set; } = null!;

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Số điện thoại phải có đúng 10 ký tự.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Định dạng email không hợp lệ.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        public string Password { get; set; } = null!;

        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int RoleId { get; set; }
    }

    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }
}
