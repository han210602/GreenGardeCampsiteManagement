using System.ComponentModel.DataAnnotations;

public class Account
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string ProfilePictureUrl { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? RoleId { get; set; } // Nullable int
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
public class ChangePassword
{
    public int UserId { get; set; }
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!; // Ensure this matches
}
public class Employee
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Gender { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}