using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess.DAO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGardenAPITest.DAO
{
    public class UserDAOTestComplexity
    {
        private readonly Mock<GreenGardenContext> _mockContext;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<DbSet<User>> _mockUsers;

        public UserDAOTestComplexity()
        {
            _mockContext = new Mock<GreenGardenContext>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockUsers = new Mock<DbSet<User>>();
        }

        // Test for method UnBlockUser
        [Fact]
        public void UnBlockUser_UserExists_ReturnsTrue()
        {
            // Arrange
            var userId = 1;
            var user = new User
            {
                UserId = userId,
                Email = "john.doe@example.com",
                IsActive = false
            };

            var data = new List<User> { user }.AsQueryable();

            _mockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            _mockContext.Setup(c => c.Users).Returns(_mockUsers.Object);

            UserDAO.InitializeContext(_mockContext.Object);
            // Act
            var result = UserDAO.UnBlockUser(userId, _mockConfiguration.Object);

            // Assert
            Assert.True(result); // Expect the unblock to succeed
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void UnBlockUser_UserDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var userId = 99; // Non-existing user
            var data = new List<User>().AsQueryable();

            _mockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            _mockContext.Setup(c => c.Users).Returns(_mockUsers.Object);

            // Act
            var result = UserDAO.UnBlockUser(userId, _mockConfiguration.Object);

            // Assert
            Assert.False(result); // Expect the unblock to fail
            _mockContext.Verify(m => m.SaveChanges(), Times.Never); // SaveChanges should not be called
        }

        //[Fact]
        //public void UnBlockUser_ExceptionOccurs_ReturnsFalse()
        //{
        //    // Arrange
        //    var userId = 1;
        //    var user = new User
        //    {
        //        UserId = userId,
        //        Email = "john.doe@example.com",
        //        IsActive = false
        //    };

        //    var data = new List<User> { user }.AsQueryable();

        //    _mockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
        //    _mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
        //    _mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
        //    _mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        //    _mockContext.Setup(c => c.Users).Returns(_mockUsers.Object);
        //    _mockContext.Setup(m => m.SaveChanges()).Throws(new Exception("Database error"));

        //    // Act
        //    var result = UserDAO.UnBlockUser(userId, _mockConfiguration.Object);

        //    // Assert
        //    Assert.False(result); // Expect the unblock to fail due to exception
        //}

        [Fact]// Test for method BlockUser
        public void BlockUser_UserExists_ReturnsTrue()
        {
            // Arrange
            var userId = 1;
            var user = new User
            {
                UserId = userId,
                Email = "john.doe@example.com",
                IsActive = true
            };

            var data = new List<User> { user }.AsQueryable();

            _mockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            _mockContext.Setup(c => c.Users).Returns(_mockUsers.Object);

            UserDAO.InitializeContext(_mockContext.Object);
            // Act
            var result = UserDAO.BlockUser(userId, _mockConfiguration.Object);

            // Assert
            Assert.True(result, "Blocking should succeed.");
            Assert.False(user.IsActive, "User should be marked as inactive.");
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void BlockUser_UserDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var userId = 1; // No user with this ID in mock data
            var data = new List<User>().AsQueryable();

            _mockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            _mockContext.Setup(c => c.Users).Returns(_mockUsers.Object);

            UserDAO.InitializeContext(_mockContext.Object);
            // Act
            var result = UserDAO.BlockUser(userId, _mockConfiguration.Object);

            // Assert
            Assert.False(result, "Blocking should fail for a non-existent user.");
            _mockContext.Verify(m => m.SaveChanges(), Times.Never);
        }

        //[Fact]
        //public void BlockUser_ExceptionOccurs_ReturnsFalse()
        //{
        //    // Arrange
        //    var userId = 1;
        //    var user = new User
        //    {
        //        UserId = userId,
        //        Email = "john.doe@example.com",
        //        IsActive = false
        //    };

        //    var data = new List<User> { user }.AsQueryable();

        //    _mockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
        //    _mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
        //    _mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
        //    _mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        //    _mockContext.Setup(c => c.Users).Returns(_mockUsers.Object);
        //    _mockContext.Setup(m => m.SaveChanges()).Throws(new Exception("Database error"));

        //    // Act
        //    var result = UserDAO.UnBlockUser(userId, _mockConfiguration.Object);

        //    // Assert
        //    Assert.False(result); // Expect the unblock to fail due to exception
        //}

        [Fact] // Test for method DeleteUser
        public void DeleteUser_UserExists_ReturnsTrue()
        {
            // Arrange
            var userId = 1;
            var user = new User
            {
                UserId = userId,
                Email = "john.doe@example.com",
            };

            var data = new List<User> { user }.AsQueryable();

            _mockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            _mockContext.Setup(c => c.Users).Returns(_mockUsers.Object);

            UserDAO.InitializeContext(_mockContext.Object);
            // Act
            var result = UserDAO.DeleteUser(userId, _mockConfiguration.Object);

            // Assert
            Assert.True(result, "Deletion should succeed.");
            _mockUsers.Verify(m => m.Remove(It.Is<User>(u => u.UserId == userId)), Times.Once);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void DeleteUser_UserDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var userId = 1; // No user with this ID in mock data
            var data = new List<User>().AsQueryable();

            _mockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            _mockContext.Setup(c => c.Users).Returns(_mockUsers.Object);

            // Act
            var result = UserDAO.DeleteUser(userId, _mockConfiguration.Object);

            // Assert
            Assert.False(result, "Deletion should fail for a non-existent user.");
            _mockUsers.Verify(m => m.Remove(It.IsAny<User>()), Times.Never);
            _mockContext.Verify(m => m.SaveChanges(), Times.Never);
        }

        [Fact] // Test for method UpdateUser
        public void UpdateUser_UserNotFound_ReturnsFalse()
        {
            // Arrange
            var updatedUserDto = new UpdateUserDTO { UserId = 1 }; // Non-existent user ID
            var data = new List<User>().AsQueryable();

            _mockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            _mockContext.Setup(c => c.Users).Returns(_mockUsers.Object);

            UserDAO.InitializeContext(_mockContext.Object);
            // Act
            var result = UserDAO.UpdateUser(updatedUserDto);

            // Assert
            Assert.False(result, "Update should fail when the user is not found.");
            _mockContext.Verify(m => m.SaveChanges(), Times.Never);
        }

        [Fact]
        public void UpdateUser_SuccessfulUpdate_ReturnsTrue()
        {
            // Arrange
            var userId = 1;
            var updatedUserDto = new UpdateUserDTO
            {
                UserId = userId,
                FirstName = "Updated",
                LastName = "User",
                Email = "updated.user@example.com",
                Password = "NewPassword",
                PhoneNumber = "1234567890",
                Address = "Updated Address",
                Gender = "Non-binary",
                DateOfBirth = new DateTime(2000, 1, 1),
                IsActive = true
            };

            var userList = new List<User>
    {
        new User { UserId = userId, FirstName = "Old", LastName = "User", Email = "old.user@example.com" }
    }.AsQueryable();

            var mockUserSet = new Mock<DbSet<User>>();
            mockUserSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
            mockUserSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            mockUserSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            mockUserSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

            var mockContext = new Mock<GreenGardenContext>();
            mockContext.Setup(c => c.Users).Returns(mockUserSet.Object);

            UserDAO.InitializeContext(mockContext.Object);

            // Act
            var result = UserDAO.UpdateUser(updatedUserDto);

            // Assert
            Assert.True(result, "UpdateUser should return true for a successful update.");
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        // Test for method AddEmployee
        [Fact]
        public void AddEmployee_EmailAlreadyExists_ReturnsFalse()
        {
            // Arrange
            var existingUser = new User { Email = "existing@example.com" };
            var userList = new List<User> { existingUser }.AsQueryable();

            _mockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockContext.Setup(c => c.Users).Returns(_mockUsers.Object);

            UserDAO.InitializeContext(_mockContext.Object);

            var newEmployeeDto = new AddUserDTO { Email = "existing@example.com" };

            // Act
            var result = UserDAO.AddEmployee(newEmployeeDto, _mockConfiguration.Object);

            // Assert
            Assert.False(result, "AddEmployee should return false when email already exists.");
        }

        [Fact]
        public void AddEmployee_SuccessfulAddition_ReturnsTrue()
        {
            // Arrange
            var userList = new List<User>().AsQueryable();

            _mockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockContext.Setup(c => c.Users).Returns(_mockUsers.Object);

            UserDAO.InitializeContext(_mockContext.Object);

            var newEmployeeDto = new AddUserDTO
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "newuser@example.com",
                Password = "password123",
                PhoneNumber = "1234567890",
                Address = "123 Main St",
                DateOfBirth = new DateTime(2000, 1, 1),
                Gender = "Male"
            };

            // Act
            var result = UserDAO.AddEmployee(newEmployeeDto, _mockConfiguration.Object);

            // Assert
            Assert.True(result, "AddEmployee should return true for a successful addition.");
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void AddEmployee_NullOrEmptyEmail_ReturnsFalse()
        {
            // Arrange
            var userList = new List<User>().AsQueryable();
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockContext.Setup(c => c.Users).Returns(_mockUsers.Object);
            UserDAO.InitializeContext(_mockContext.Object);

            var newEmployeeDto = new AddUserDTO { Email = "" }; // Empty email

            // Act
            var result = UserDAO.AddEmployee(newEmployeeDto, _mockConfiguration.Object);

            // Assert
            Assert.False(result, "AddEmployee should return false when email is empty.");
        }

        [Fact]
        public void AddEmployee_DuplicatePhoneNumber_ReturnsFalse()
        {
            // Arrange
            var existingUser = new User { PhoneNumber = "1234567890" };
            var userList = new List<User> { existingUser }.AsQueryable();

            _mockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockContext.Setup(c => c.Users).Returns(_mockUsers.Object);

            UserDAO.InitializeContext(_mockContext.Object);

            var newEmployeeDto = new AddUserDTO
            {
                PhoneNumber = "1234567890" // Duplicate phone number
            };

            // Act
            var result = UserDAO.AddEmployee(newEmployeeDto, _mockConfiguration.Object);

            // Assert
            Assert.False(result, "AddEmployee should return false when phone number is duplicated.");
        }

        [Fact]
        public void AddEmployee_ExceptionDuringEmailSend_DoesNotAffectDatabaseChanges()
        {
            // Arrange
            var userList = new List<User>().AsQueryable();
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockContext.Setup(c => c.Users).Returns(_mockUsers.Object);

            UserDAO.InitializeContext(_mockContext.Object);

            var newEmployeeDto = new AddUserDTO
            {
                Email = "newuser@example.com"
            };

            _mockConfiguration.Setup(c => c["EmailSettings:Host"]).Throws(new Exception("Email send failed"));

            // Act
            var result = UserDAO.AddEmployee(newEmployeeDto, _mockConfiguration.Object);

            // Assert
            Assert.True(result, "AddEmployee should return true even if email sending fails.");
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        // Test for method AddCustomer
        [Fact]
        public void AddCustomer_EmailAlreadyExists_ReturnsFalse()
        {
            // Arrange
            var existingUser = new User { Email = "existing@example.com" };
            var userList = new List<User> { existingUser }.AsQueryable();

            _mockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockContext.Setup(c => c.Users).Returns(_mockUsers.Object);
            UserDAO.InitializeContext(_mockContext.Object);

            var newCustomerDto = new AddUserDTO { Email = "existing@example.com" };

            // Act
            var result = UserDAO.AddCustomer(newCustomerDto, _mockConfiguration.Object);

            // Assert
            Assert.False(result, "AddCustomer should return false when the email already exists.");
        }

        [Fact]
        public void AddCustomer_SuccessfulAddition_ReturnsTrue()
        {
            // Arrange
            var userList = new List<User>().AsQueryable();

            _mockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockContext.Setup(c => c.Users).Returns(_mockUsers.Object);
            UserDAO.InitializeContext(_mockContext.Object);

            var newCustomerDto = new AddUserDTO
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "newcustomer@example.com",
                Password = "password123",
                PhoneNumber = "1234567890",
                Address = "123 Main St",
                DateOfBirth = new DateTime(1990, 1, 1),
                Gender = "Male",
                IsActive = true
            };

            // Act
            var result = UserDAO.AddCustomer(newCustomerDto, _mockConfiguration.Object);

            // Assert
            Assert.True(result, "AddCustomer should return true for successful addition.");
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void AddCustomer_NullOrEmptyEmail_ReturnsFalse()
        {
            // Arrange
            var userList = new List<User>().AsQueryable();

            _mockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockContext.Setup(c => c.Users).Returns(_mockUsers.Object);
            UserDAO.InitializeContext(_mockContext.Object);

            var newCustomerDto = new AddUserDTO { Email = "" }; // Empty email

            // Act
            var result = UserDAO.AddCustomer(newCustomerDto, _mockConfiguration.Object);

            // Assert
            Assert.False(result, "AddCustomer should return false when the email is empty.");
        }

        [Fact]
        public void AddCustomer_DuplicatePhoneNumber_ReturnsFalse()
        {
            // Arrange
            var existingUser = new User { PhoneNumber = "1234567890" };
            var userList = new List<User> { existingUser }.AsQueryable();

            _mockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockContext.Setup(c => c.Users).Returns(_mockUsers.Object);
            UserDAO.InitializeContext(_mockContext.Object);

            var newCustomerDto = new AddUserDTO
            {
                PhoneNumber = "1234567890" // Duplicate phone number
            };

            // Act
            var result = UserDAO.AddCustomer(newCustomerDto, _mockConfiguration.Object);

            // Assert
            Assert.False(result, "AddCustomer should return false when the phone number already exists.");
        }

        [Fact]
        public void AddCustomer_ExceptionDuringEmailSend_ReturnsTrue()
        {
            // Arrange
            var userList = new List<User>().AsQueryable();

            _mockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockContext.Setup(c => c.Users).Returns(_mockUsers.Object);
            UserDAO.InitializeContext(_mockContext.Object);

            var newCustomerDto = new AddUserDTO
            {
                Email = "newcustomer@example.com"
            };

            _mockConfiguration.Setup(c => c["EmailSettings:Host"]).Throws(new Exception("Email sending failed"));

            // Act
            var result = UserDAO.AddCustomer(newCustomerDto, _mockConfiguration.Object);

            // Assert
            Assert.True(result, "AddCustomer should return true even if email sending fails.");
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void AddCustomer_InvalidDateOfBirth_ReturnsFalse()
        {
            // Arrange
            var userList = new List<User>().AsQueryable();

            _mockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockContext.Setup(c => c.Users).Returns(_mockUsers.Object);
            UserDAO.InitializeContext(_mockContext.Object);

            var newCustomerDto = new AddUserDTO
            {
                DateOfBirth = DateTime.Now.AddYears(1) // Invalid: Future date
            };

            // Act
            var result = UserDAO.AddCustomer(newCustomerDto, _mockConfiguration.Object);

            // Assert
            Assert.False(result, "AddCustomer should return false when the date of birth is invalid.");
        }
    }

}

