using BusinessObject.DTOs;
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
    public class AccountDAOTest
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
            // Seed data for Tickets
            if (!await databaseContext.Users.AnyAsync()) // Only add data if the table is empty
            {
                databaseContext.Users.AddRange(
                    new User
                    {
                        UserId = 1,
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "john.doe@example.com",
                        Password = "password123",
                        PhoneNumber = "1234567890",
                        Address = "123 Elm Street",
                        DateOfBirth = new DateTime(1990, 1, 1),
                        Gender = "Male",
                        ProfilePictureUrl = "http://example.com/john.jpg",
                        IsActive = true,
                        CreatedAt = DateTime.Now.AddDays(-30),
                        RoleId = 1
                    },
                    new User
                    {
                        UserId = 2,
                        FirstName = "Jane",
                        LastName = "Smith",
                        Email = "jane.smith@example.com",
                        Password = "securepassword",
                        PhoneNumber = "0987654321",
                        Address = "456 Oak Avenue",
                        DateOfBirth = new DateTime(1995, 5, 15),
                        Gender = "Female",
                        ProfilePictureUrl = "http://example.com/jane.jpg",
                        IsActive = false,
                        CreatedAt = DateTime.Now.AddDays(-15),
                        RoleId = 2
                    }
                    );
                await databaseContext.SaveChangesAsync();
            }

            return databaseContext;
        }

        // Test for method GetAllAccounts
        [Fact]
        public async Task GetAllAccounts_ShouldReturnAllUsers()
        {
            // Arrange
            var dbContext = await GetDbContext();
            AccountDAO.InitializeContext(dbContext);

            // Act
            var users = AccountDAO.GetAllAccounts();

            // Assert
            Assert.NotNull(users);
            Assert.Equal(2, users.Count); // Expecting 2 users
            Assert.Contains(users, u => u.FirstName == "John" && u.Email == "john.doe@example.com");
            Assert.Contains(users, u => u.FirstName == "Jane" && u.Email == "jane.smith@example.com");
        }

        [Fact]
        public async Task GetAllAccounts_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // unique name for each test
                 .Options;

            var emptyContext = new GreenGardenContext(options);
            emptyContext.Database.EnsureCreated();
            AccountDAO.InitializeContext(emptyContext);

            // Act
            var users = AccountDAO.GetAllAccounts();

            // Assert
            Assert.NotNull(users); // Ensure the method doesn't return null
            Assert.Empty(users);   // Check that the list is empty
        }

        [Fact]
        public async Task GetAllAccounts_ShouldThrowException_WhenRepositoryThrowsException()
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
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(AccountDAO.GetAllAccounts()));

        }

        // Test for method GetAccountById

    }
}
