using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess.DAO;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GreenGardenAPITest.DAO
{
    public class AccountDAOTestComplexity
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<GreenGardenContext> _mockContext;
        private readonly Mock<DbSet<User>> _mockUsers;

        private readonly Dictionary<string, (string Code, DateTime Expiration)> _verificationCodes;

        public AccountDAOTestComplexity()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockContext = new Mock<GreenGardenContext>();
            _mockUsers = new Mock<DbSet<User>>();

            _verificationCodes = new Dictionary<string, (string, DateTime)>();

            // Mock configuration settings for JWT
            _mockConfiguration.SetupGet(c => c["Jwt:Key"]).Returns("your_secret_key_here");
            _mockConfiguration.SetupGet(c => c["Jwt:Issuer"]).Returns("your_issuer_here");
            _mockConfiguration.SetupGet(c => c["Jwt:Audience"]).Returns("your_audience_here");
            _mockConfiguration.SetupGet(c => c["Jwt:Subject"]).Returns("your_subject_here");

            
        }

        private IQueryable<User> GetMockUsers()
        {
            return new List<User>
        {
            new User
            {
                UserId = 1,
                Email = "activeuser@example.com",
                Password = "correct_password",
                IsActive = true,
                FirstName = "John",
                LastName = "Doe",
                ProfilePictureUrl = "profile.jpg",
                PhoneNumber = "1234567890",
                RoleId = 1
            },
            new User
            {
                UserId = 2,
                Email = "inactiveuser@example.com",
                Password = "password",
                IsActive = false
            }
        }.AsQueryable();
        }

        private void SetupMockUsers(IQueryable<User> users)
        {
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            _mockContext.Setup(c => c.Users).Returns(_mockUsers.Object);
        }

        // Test for method Login
        [Fact]
        public void Login_EmailDoesNotExist_ThrowsException()
        {
            // Arrange
            SetupMockUsers(new List<User>().AsQueryable()); // No users
            var account = new AccountDTO { Email = "nonexistent@example.com", Password = "password" };

            AccountDAO.InitializeContext(_mockContext.Object);
            // Act & Assert
            Assert.Throws<Exception>(() => AccountDAO.Login(account, _mockConfiguration.Object));
        }

        [Fact]
        public void Login_AccountIsDeactivated_ThrowsException()
        {
            // Arrange
            SetupMockUsers(GetMockUsers());
            var account = new AccountDTO { Email = "inactiveuser@example.com", Password = "password" };

            AccountDAO.InitializeContext(_mockContext.Object);
            // Act & Assert
            Assert.Throws<Exception>(() => AccountDAO.Login(account, _mockConfiguration.Object));
        }

        [Fact]
        public void Login_IncorrectPassword_ThrowsException()
        {
            // Arrange
            SetupMockUsers(GetMockUsers());
            var account = new AccountDTO { Email = "activeuser@example.com", Password = "wrong_password" };

            AccountDAO.InitializeContext(_mockContext.Object);
            // Act & Assert
            Assert.Throws<Exception>(() => AccountDAO.Login(account, _mockConfiguration.Object));
        }

    //    [Fact]
    //    public void Login_SuccessfulLogin_ReturnsSerializedResponse()
    //    {
    //        // Arrange
    //        var mockUsers = new List<User>
    //{
    //    new User
    //    {
    //        UserId = 1,
    //        Email = "activeuser@example.com",
    //        Password = "correct_password",
    //        IsActive = true,
    //        FirstName = "John",
    //        LastName = "Doe",
    //        ProfilePictureUrl = "profile.jpg",
    //        PhoneNumber = "1234567890",
    //        RoleId = 1
    //    }
    //}.AsQueryable();

    //        SetupMockUsers(mockUsers); // Mock the Users DbSet

    //        var account = new AccountDTO { Email = "activeuser@example.com", Password = "correct_password" };

    //        AccountDAO.InitializeContext(_mockContext.Object); // Set the mock context

    //        // Act
    //        var result = AccountDAO.Login(account, _mockConfiguration.Object);

    //        // Assert
    //        Assert.NotNull(result);
    //        //var response = JsonConvert.DeserializeObject<LoginResponseDTO>(result);
    //        //Assert.Equal("John Doe", response.FullName);
    //        //Assert.Equal("activeuser@example.com", response.Email);
    //    }

        [Fact]
        public void Login_NullPassword_ThrowsException()
        {
            // Arrange
            SetupMockUsers(GetMockUsers());
            var account = new AccountDTO { Email = "activeuser@example.com", Password = null };

            AccountDAO.InitializeContext(_mockContext.Object);
            // Act & Assert
            Assert.Throws<Exception>(() => AccountDAO.Login(account, _mockConfiguration.Object));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Login_NullOrEmptyEmail_ThrowsException(string email)
        {
            // Arrange
            SetupMockUsers(GetMockUsers());
            var account = new AccountDTO { Email = email, Password = "password" };

            AccountDAO.InitializeContext(_mockContext.Object);
            // Act & Assert
            Assert.Throws<Exception>(() => AccountDAO.Login(account, _mockConfiguration.Object));
        }

        // Test for method Register
        [Fact]
        public async Task Register_VerificationCodeInvalid_ThrowsException()
        {
            // Arrange
            var registerDto = new Register { Email = "user@example.com" };
            var enteredCode = "wrong_code";
            AccountDAO.InitializeContext(_mockContext.Object);
            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() =>
                AccountDAO.Register(registerDto, enteredCode, It.IsAny<IConfiguration>()));
            Assert.Equal("Mã xác thực không đúng.", ex.Message);
        }

        [Fact]
        public async Task Register_VerificationCodeExpired_ThrowsException()
        {
            // Arrange
            var registerDto = new Register { Email = "user@example.com" };
            var enteredCode = "123456";
            _verificationCodes["user@example.com"] = (enteredCode, DateTime.UtcNow.AddMinutes(-1));
            AccountDAO.InitializeContext(_mockContext.Object);
            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() =>
                AccountDAO.Register(registerDto, enteredCode, It.IsAny<IConfiguration>()));
            Assert.Equal("Mã xác thực không đúng.", ex.Message);
        }

        [Fact]
        public async Task Register_EmailAlreadyRegistered_ThrowsException()
        {
            // Arrange
            var registerDto = new Register { Email = "user@example.com" };
            var enteredCode = "123456";
            _verificationCodes["user@example.com"] = (enteredCode, DateTime.UtcNow.AddMinutes(5));

            var existingUser = new User { Email = "user@example.com" };
            SetupMockUsers(new List<User> { existingUser }.AsQueryable());
            AccountDAO.InitializeContext(_mockContext.Object);
            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() =>
                AccountDAO.Register(registerDto, enteredCode, It.IsAny<IConfiguration>()));
            //Assert.Equal("Email đã được đăng ký.", ex.Message);
        }

        [Fact]
        public async Task Register_SuccessfulRegistration_ReturnsResponse()
        {
            // Arrange
            var registerDto = new Register
            {
                Email = "user@example.com",
                Password = "password123",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "123456789"
            };
            var enteredCode = "123456";
            _verificationCodes["user@example.com"] = (enteredCode, DateTime.UtcNow.AddMinutes(5));

            SetupMockUsers(new List<User>().AsQueryable());
            AccountDAO.InitializeContext(_mockContext.Object);
            // Act
            var result = AccountDAO.Register(registerDto, enteredCode, It.IsAny<IConfiguration>());

            // Assert
            Assert.NotNull(result);
            //var response = JsonSerializer.Deserialize<Dictionary<string, object>>(result);
            //Assert.Equal("Đăng kí thành công", response["Message"]);
            //Assert.Equal("user@example.com", response["Email"]);
            //Assert.False(_verificationCodes.ContainsKey(registerDto.Email));
        }

        [Fact]
        public async Task Register_DatabaseUpdateError_ThrowsException()
        {
            // Arrange
            var registerDto = new Register { Email = "user@example.com" };
            var enteredCode = "123456";
            _verificationCodes["user@example.com"] = (enteredCode, DateTime.UtcNow.AddMinutes(5));

            _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new DbUpdateException("Mock database update exception"));

            SetupMockUsers(new List<User>().AsQueryable());
            AccountDAO.InitializeContext(_mockContext.Object);
            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() =>
                AccountDAO.Register(registerDto, enteredCode, It.IsAny<IConfiguration>()));
            //Assert.Contains("Đã xảy ra lỗi khi lưu thay đổi", ex.Message);
        }

        [Fact]
        public async Task Register_VerificationCodeNotFound_ThrowsException()
        {
            // Arrange
            var registerDto = new Register { Email = "nonexistent@example.com" };
            var enteredCode = "123456";
            AccountDAO.InitializeContext(_mockContext.Object);
            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() =>
                AccountDAO.Register(registerDto, enteredCode, It.IsAny<IConfiguration>()));
            Assert.Equal("Mã xác thực không đúng.", ex.Message);
        }

        [Fact]
        public async Task Register_MissingOrNullEmail_ThrowsException()
        {
            // Arrange
            var registerDto = new Register { Email = null };
            var enteredCode = "123456";
            AccountDAO.InitializeContext(_mockContext.Object);
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                AccountDAO.Register(registerDto, enteredCode, It.IsAny<IConfiguration>()));
        }

        [Fact]
        public async Task Register_NullOrEmptyVerificationCode_ThrowsException()
        {
            // Arrange
            var registerDto = new Register { Email = "user@example.com" };
            string enteredCode = null;
            AccountDAO.InitializeContext(_mockContext.Object);
            // Act & Assert
            var ex =await Assert.ThrowsAsync<Exception>(() =>
                AccountDAO.Register(registerDto, enteredCode, It.IsAny<IConfiguration>()));
            Assert.Equal("Mã xác thực không đúng.", ex.Message);
        }

        



    }
}
