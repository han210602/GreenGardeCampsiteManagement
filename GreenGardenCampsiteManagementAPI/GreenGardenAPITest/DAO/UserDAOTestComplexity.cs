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

            var existingUser = new User { UserId = userId };
           // _mockContext.Setup(c => c.Users.FirstOrDefault(It.IsAny<Func<User, bool>>())).Returns(existingUser);

            UserDAO.InitializeContext(_mockContext.Object);
            // Act
            var result = UserDAO.UpdateUser(updatedUserDto);

            // Assert
            Assert.True(result, "Update should succeed for a valid user.");
            Assert.Equal("Updated", existingUser.FirstName);
            Assert.Equal("Updated Address", existingUser.Address);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void UpdateUser_ExceptionDuringSave_ReturnsFalse()
        {
            // Arrange
            var userId = 1;
            var updatedUserDto = new UpdateUserDTO { UserId = userId };

            var existingUser = new User { UserId = userId };
            _mockContext.Setup(c => c.Users.FirstOrDefault(It.IsAny<Func<User, bool>>())).Returns(existingUser);
            _mockContext.Setup(c => c.SaveChanges()).Throws(new Exception("Database error"));

            // Act
            var result = UserDAO.UpdateUser(updatedUserDto);

            // Assert
            Assert.False(result, "Update should fail if an exception occurs during save.");
        }

        [Fact]
        public void UpdateUser_NullOrEmptyEmail_ReturnsFalse()
        {
            // Arrange
            var userId = 1;
            var updatedUserDto = new UpdateUserDTO
            {
                UserId = userId,
                Email = "" // Invalid email
            };

            var existingUser = new User { UserId = userId };
            _mockContext.Setup(c => c.Users.FirstOrDefault(It.IsAny<Func<User, bool>>())).Returns(existingUser);

            UserDAO.InitializeContext(_mockContext.Object);
            // Act
            var result = UserDAO.UpdateUser(updatedUserDto);

            // Assert
            Assert.False(result, "Update should fail for null or empty email.");
            _mockContext.Verify(m => m.SaveChanges(), Times.Never);
        }

        [Fact]
        public void UpdateUser_InvalidDateOfBirth_ReturnsFalse()
        {
            // Arrange
            var userId = 1;
            var updatedUserDto = new UpdateUserDTO
            {
                UserId = userId,
                DateOfBirth = DateTime.UtcNow.AddYears(1) // Future date
            };

            var existingUser = new User { UserId = userId };
            _mockContext.Setup(c => c.Users.FirstOrDefault(It.IsAny<Func<User, bool>>())).Returns(existingUser);

            UserDAO.InitializeContext(_mockContext.Object);
            // Act
            var result = UserDAO.UpdateUser(updatedUserDto);

            // Assert
            Assert.False(result, "Update should fail for an invalid date of birth.");
            _mockContext.Verify(m => m.SaveChanges(), Times.Never);
        }

        [Fact]
        public void UpdateUser_IsActiveStatusUpdated_ReturnsTrue()
        {
            // Arrange
            var userId = 1;
            var updatedUserDto = new UpdateUserDTO
            {
                UserId = userId,
                IsActive = false
            };

            var existingUser = new User { UserId = userId, IsActive = true };
            _mockContext.Setup(c => c.Users.FirstOrDefault(It.IsAny<Func<User, bool>>())).Returns(existingUser);

            UserDAO.InitializeContext(_mockContext.Object);
            // Act
            var result = UserDAO.UpdateUser(updatedUserDto);

            // Assert
            Assert.True(result, "Update should succeed for valid IsActive status.");
            Assert.False(existingUser.IsActive);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void UpdateUser_DuplicatePhoneNumber_ReturnsFalse()
        {
            // Arrange
            var userId = 1;
            var updatedUserDto = new UpdateUserDTO
            {
                UserId = userId,
                PhoneNumber = "1234567890"
            };

            var existingUser = new User { UserId = userId };
            var otherUser = new User { UserId = 2, PhoneNumber = "1234567890" }; // Duplicate phone number

            var data = new List<User> { existingUser, otherUser }.AsQueryable();
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            _mockContext.Setup(c => c.Users).Returns(_mockUsers.Object);

            UserDAO.InitializeContext(_mockContext.Object);
            // Act
            var result = UserDAO.UpdateUser(updatedUserDto);

            // Assert
            Assert.False(result, "Update should fail for duplicate phone numbers.");
            _mockContext.Verify(m => m.SaveChanges(), Times.Never);
        }


    }

}

