namespace GreenGardenCampsiteManagementAPI.DTO
{
    public class User
    {
        public int Userid { get; set; } 
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Ví dụ: Admin, User, etc.
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

}
