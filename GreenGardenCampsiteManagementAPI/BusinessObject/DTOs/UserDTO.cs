using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Họ không được để trống.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Tên không được để trống.")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        public string Password { get; set; } = null!;

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại phải có 10 số.")]
        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? RoleName { get; set; }
    }

    public class AddUserDTO
    {
        [Required(ErrorMessage = "Họ không được để trống.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Tên không được để trống.")]
        public string LastName { get; set; } = null!;

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại phải có 10 số.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        public string Email { get; set; } = null!;

        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        public string Password { get; set; } = null!;

        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int RoleId { get; set; }
    }

    public class UpdateUserDTO
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Họ không được để trống.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Tên không được để trống.")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        public string Password { get; set; } = null!;

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại phải có 10 số.")]
        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public bool? IsActive { get; set; }
    }
}
