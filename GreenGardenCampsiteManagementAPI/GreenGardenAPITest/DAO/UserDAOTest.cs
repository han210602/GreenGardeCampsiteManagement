using BusinessObject.Models;
using DataAccess.DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGardenAPITest.DAO
{
    public class UserDAOTest
    {
        private async Task<GreenGardenContext> GetDbContext() // Create a database in memory with mock data.
        {
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // unique name for each test
                .Options;

            var databaseContext = new GreenGardenContext(options);
            databaseContext.Database.EnsureCreated();

            // Clear the change tracker to ensure no duplicates
            databaseContext.ChangeTracker.Clear();

            if (!await databaseContext.Users.AnyAsync())
            {
                databaseContext.Roles.AddRange(
                    new Role { RoleId = 1, RoleName = "Admin" },
                    new Role { RoleId = 2, RoleName = "User" }

                );

                databaseContext.Users.AddRange(
                    new User
                    {
                        UserId = 1,
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "john.doe@example.com",
                        Password = "Password123",
                        PhoneNumber = "0123456789",
                        Address = "123 Main St",
                        DateOfBirth = new DateTime(1990, 1, 1),
                        Gender = "Male",
                        ProfilePictureUrl = "http://example.com/john.jpg",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        RoleId = 1
                    },
                    new User
                    {
                        UserId = 2,
                        FirstName = "Jane",
                        LastName = "Smith",
                        Email = "jane.smith@example.com",
                        Password = "Password456",
                        PhoneNumber = "9876543210",
                        Address = "456 Elm St",
                        DateOfBirth = new DateTime(1995, 5, 5),
                        Gender = "Female",
                        ProfilePictureUrl = "http://example.com/jane.jpg",
                        IsActive = false,
                        CreatedAt = DateTime.UtcNow,
                        RoleId = 2
                    }

                );

                await databaseContext.SaveChangesAsync();
            }

            return databaseContext;
        }

        // Test for GetAllUsers method
        [Fact]
        public async Task GetAllUsers_ShouldReturnAllUsers()
        {
            // Arrange
            var dbContext = await GetDbContext();
            UserDAO.InitializeContext(dbContext); // Initialize context in DAO

            // Act
            var users = UserDAO.GetAllUsers();

            // Assert
            Assert.NotNull(users);
            Assert.Equal(2, users.Count); // Expecting 2 users

            var john = users.FirstOrDefault(u => u.FirstName == "John" && u.Email == "john.doe@example.com");
            Assert.NotNull(john);
            Assert.Equal("Admin", john.RoleName);
            Assert.True(john.IsActive);

            var jane = users.FirstOrDefault(u => u.FirstName == "Jane" && u.Email == "jane.smith@example.com");
            Assert.NotNull(jane);
            Assert.Equal("User", jane.RoleName);
            Assert.False(jane.IsActive);
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new GreenGardenContext(options);
            UserDAO.InitializeContext(dbContext); // No users added

            // Act
            var users = UserDAO.GetAllUsers();

            // Assert
            Assert.NotNull(users);
            Assert.Empty(users);
        }

        [Fact]
        public async Task GetAllUsers_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new GreenGardenContext(options);
            databaseContext.Database.EnsureCreated();

            // Simulate an exception scenario by setting the context to null or failing in some way
            ActivityDAO.InitializeContext(null);  // Provide null context to force an exception

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(UserDAO.GetAllUsers()));

        }

        // Test for GetAllEmployees method
        private async Task<GreenGardenContext> GetDbContext2() // Create a database in memory with mock data.
        {
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique name for each test
                .Options;

            var databaseContext = new GreenGardenContext(options);
            databaseContext.Database.EnsureCreated();

            // Clear the change tracker to ensure no duplicates
            databaseContext.ChangeTracker.Clear();

            if (!await databaseContext.Users.AnyAsync())
            {
                databaseContext.Roles.AddRange(
                    new Role { RoleId = 1, RoleName = "Admin" },
                    new Role { RoleId = 2, RoleName = "Employee" }
                );

                databaseContext.Users.AddRange(
                    new User
                    {
                        UserId = 1,
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "john.doe@example.com",
                        Password = "Password123",
                        PhoneNumber = "0123456789",
                        Address = "123 Main St",
                        DateOfBirth = new DateTime(1990, 1, 1),
                        Gender = "Male",
                        ProfilePictureUrl = "http://example.com/john.jpg",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        RoleId = 1 // Admin
                    },
                    new User
                    {
                        UserId = 2,
                        FirstName = "Jane",
                        LastName = "Smith",
                        Email = "jane.smith@example.com",
                        Password = "Password456",
                        PhoneNumber = "9876543210",
                        Address = "456 Elm St",
                        DateOfBirth = new DateTime(1995, 5, 5),
                        Gender = "Female",
                        ProfilePictureUrl = "http://example.com/jane.jpg",
                        IsActive = false,
                        CreatedAt = DateTime.UtcNow,
                        RoleId = 2 // Employee
                    }
                );

                await databaseContext.SaveChangesAsync();
            }

            return databaseContext;
        }

        [Fact]
        public async Task GetAllEmployees_ShouldReturnAllEmployees()
        {
            // Arrange
            var dbContext = await GetDbContext2();
            UserDAO.InitializeContext(dbContext); // Initialize context in DAO

            // Act
            var employees = UserDAO.GetAllEmployees();

            // Assert
            Assert.NotNull(employees);
            Assert.Single(employees); // Expecting 1 employee with RoleId = 2

            var jane = employees.FirstOrDefault(e => e.FirstName == "Jane" && e.Email == "jane.smith@example.com");
            Assert.NotNull(jane);
            Assert.Equal("Employee", jane.RoleName);
            Assert.False(jane.IsActive);

            // Ensure no admin is included
            var john = employees.FirstOrDefault(e => e.FirstName == "John");
            Assert.Null(john);
        }

        [Fact]
        public async Task GetAllEmployees_ShouldReturnEmptyList_WhenNoEmployeesExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new GreenGardenContext(options);
            UserDAO.InitializeContext(dbContext); // No users added

            // Act
            var employees = UserDAO.GetAllEmployees();

            // Assert
            Assert.NotNull(employees);
            Assert.Empty(employees);
        }

        [Fact]
        public async Task GetAllEmployees_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new GreenGardenContext(options);
            databaseContext.Database.EnsureCreated();

            // Simulate an exception scenario by setting the context to null
            UserDAO.InitializeContext(null); // Provide null context to force an exception

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(UserDAO.GetAllEmployees()));

            Assert.Contains("Value cannot be null.", exception.Message);
        }

        // Test for method GetAllCustomers
        private async Task<GreenGardenContext> GetDbContext3()
        {
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // unique name for each test
                .Options;

            var databaseContext = new GreenGardenContext(options);
            databaseContext.Database.EnsureCreated();

            // Clear the change tracker to ensure no duplicates
            databaseContext.ChangeTracker.Clear();

            if (!await databaseContext.Users.AnyAsync())
            {
                // Add roles
                databaseContext.Roles.AddRange(
                    new Role { RoleId = 1, RoleName = "Admin" },
                    new Role { RoleId = 2, RoleName = "User" },
                    new Role { RoleId = 3, RoleName = "Customer" }
                );

                // Add users
                databaseContext.Users.AddRange(
                    // Admin user
                    new User
                    {
                        UserId = 1,
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "john.doe@example.com",
                        Password = "Password123",
                        PhoneNumber = "0123456789",
                        Address = "123 Main St",
                        DateOfBirth = new DateTime(1990, 1, 1),
                        Gender = "Male",
                        ProfilePictureUrl = "http://example.com/john.jpg",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        RoleId = 1
                    },
                    // Regular user
                    new User
                    {
                        UserId = 2,
                        FirstName = "Jane",
                        LastName = "Smith",
                        Email = "jane.smith@example.com",
                        Password = "Password456",
                        PhoneNumber = "9876543210",
                        Address = "456 Elm St",
                        DateOfBirth = new DateTime(1995, 5, 5),
                        Gender = "Female",
                        ProfilePictureUrl = "http://example.com/jane.jpg",
                        IsActive = false,
                        CreatedAt = DateTime.UtcNow,
                        RoleId = 2
                    },
                    // Customer 1
                    new User
                    {
                        UserId = 3,
                        FirstName = "Hanna",
                        LastName = "Brown",
                        Email = "hanna.brown@example.com",
                        Password = "CustomerPass1",
                        PhoneNumber = "1234567890",
                        Address = "789 Oak St",
                        DateOfBirth = new DateTime(1996, 5, 5),
                        Gender = "Female",
                        ProfilePictureUrl = "http://example.com/hanna.jpg",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        RoleId = 3
                    }
                );

                await databaseContext.SaveChangesAsync();
            }

            return databaseContext;
        }
        [Fact]
        public async Task GetAllCustomers_ShouldReturnAllEmployees()
        {
            // Arrange
            var dbContext = await GetDbContext3();
            UserDAO.InitializeContext(dbContext); // Initialize context in DAO

            // Act
            var employees = UserDAO.GetAllCustomers();

            // Assert
            Assert.NotNull(employees);
            Assert.Single(employees); // Expecting 1 customer with RoleId = 3

            var hanna = employees.FirstOrDefault(e => e.FirstName == "Hanna" && e.Email == "hanna.brown@example.com");
            Assert.NotNull(hanna);
            Assert.Equal("Customer", hanna.RoleName);
            Assert.True(hanna.IsActive);

            // Ensure no admin is included
            var john = employees.FirstOrDefault(e => e.FirstName == "John");
            Assert.Null(john);
        }

        [Fact]
        public async Task GetAllCustomers_ShouldReturnEmptyList_WhenNoEmployeesExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new GreenGardenContext(options);
            UserDAO.InitializeContext(dbContext); // No users added

            // Act
            var employees = UserDAO.GetAllCustomers();

            // Assert
            Assert.NotNull(employees);
            Assert.Empty(employees);
        }

        [Fact]
        public async Task GetAllCustomers_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new GreenGardenContext(options);
            databaseContext.Database.EnsureCreated();

            // Simulate an exception scenario by setting the context to null
            UserDAO.InitializeContext(null); // Provide null context to force an exception

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(UserDAO.GetAllCustomers()));

            Assert.Contains("Value cannot be null.", exception.Message);
        }

        // Test for method GetUserById
        [Fact]
        public async Task GetUserById_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var dbContext = await GetDbContext();
            UserDAO.InitializeContext(dbContext); // Initialize context in DAO

            // Act
            var user = UserDAO.GetUserById(1); // Fetch user with UserId = 1

            // Assert
            Assert.NotNull(user);
            Assert.Equal(1, user.UserId);
            Assert.Equal("John", user.FirstName);
            Assert.Equal("Doe", user.LastName);
            Assert.Equal("Admin", user.RoleName);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var dbContext = await GetDbContext();
            UserDAO.InitializeContext(dbContext); // Initialize context in DAO

            // Act
            var user = UserDAO.GetUserById(999); // Fetch user with non-existing UserId

            // Assert
            Assert.Null(user);
        }

    }
}
