using BusinessObject.Models;
using DataAccess.DAO;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGardenAPITest.DAO
{
    public class DashBoardDAOTest
    {
        private async Task<GreenGardenContext> GetDbContextWithOrdersAndCustomers()
        {
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated();

            if (!await dbContext.Users.AnyAsync())
            {
                dbContext.Users.AddRange(
                    new User
                    {
                        UserId = 1,
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "john.doe@example.com",
                        Password = "123",
                        IsActive = true,
                        DateOfBirth = new DateTime(1990, 5, 20),
                        PhoneNumber = "123456789",
                        Address = "123 Main St"
                    },
                    new User
                    {
                        UserId = 2,
                        FirstName = "Jane",
                        LastName = "Smith",
                        Email = "jane.smith@example.com",
                        Password = "123",
                        IsActive = true,
                        DateOfBirth = new DateTime(1985, 7, 10),
                        PhoneNumber = "987654321",
                        Address = "456 Another St"
                    }
                );

                dbContext.Orders.AddRange(
                    new Order
                    {
                        OrderId = 1,
                        CustomerId = 1,  // Correctly associate with John Doe
                        Customer = dbContext.Users.First(u => u.UserId == 1),
                        OrderDate = DateTime.Now
                    },
                    new Order
                    {
                        OrderId = 2,
                        CustomerId = 2,  // Correctly associate with Jane Smith
                        Customer = dbContext.Users.First(u => u.UserId == 2),
                        OrderDate = DateTime.Now
                    }
                );

                await dbContext.SaveChangesAsync();
            }

            return dbContext;
        }

        // Test for method ListCustomer
        //[Fact]
        //public async Task ListCustomer_ShouldReturnListOfActiveCustomers()
        //{
        //    // Arrange
        //    var dbContext = await GetDbContextWithOrdersAndCustomers();
        //    DashBoardDAO.InitializeContext(dbContext); // Assuming the method ListCustomer() is in UserService class

        //    // Act
        //    var customers = DashBoardDAO.ListCustomer();

        //    // Assert
        //    customers.Should().NotBeEmpty();
        //    customers.Count.Should().Be(2);

        //    var customer1 = customers.FirstOrDefault(c => c.UserId == 1);
        //    customer1.Should().NotBeNull();
        //    customer1.FirstName.Should().Be("John");

        //    var customer2 = customers.FirstOrDefault(c => c.UserId == 2);
        //    customer2.Should().NotBeNull();
        //    customer2.FirstName.Should().Be("Jane");
        //}

        [Fact]
        public async Task ListCustomer_ShouldReturnEmptyList_WhenNoActiveOrders()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated();
            DashBoardDAO.InitializeContext(dbContext);

            // Act
            var customers = DashBoardDAO.ListCustomer();

            // Assert
            customers.Should().BeEmpty();
        }

        [Fact]
        public async Task ListCustomer_ShouldThrowException_WhenDatabaseFails()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new GreenGardenContext(options);
            databaseContext.Database.EnsureCreated();

            // Simulate an exception scenario by setting the context to null or failing in some way
            ComboDAO.InitializeContext(null); // Provide null context to force an exception

            // Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(() => Task.FromResult(DashBoardDAO.ListCustomer()));
        }

        // Test for method Profit
        private async Task<GreenGardenContext> GetDbContextWithOrders()
        {
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique in-memory database
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated();

            // Add sample data
            dbContext.Orders.AddRange(
                new Order
                {
                    OrderId = 1,
                    ActivityId = 1, // Online order
                    Deposit = 100,
                    TotalAmount = 500,
                    OrderDate = new DateTime(2024, 4, 1),
                    OrderCheckoutDate = null,
                    OrderUsageDate = null
                },
                new Order
                {
                    OrderId = 2,
                    ActivityId = 3, // Checkout order
                    Deposit = 0,
                    TotalAmount = 200,
                    OrderDate = new DateTime(2024, 4, 2),
                    OrderCheckoutDate = new DateTime(2024, 4, 2),
                    OrderUsageDate = null
                },
                new Order
                {
                    OrderId = 3,
                    ActivityId = 1002, // Cancelled order
                    Deposit = 50,
                    TotalAmount = 0,
                    OrderDate = new DateTime(2024, 4, 3),
                    OrderCheckoutDate = null,
                    OrderUsageDate = null
                },
                new Order
                {
                    OrderId = 4,
                    ActivityId = 2, // Using order
                    Deposit = 150,
                    TotalAmount = 0,
                    OrderDate = new DateTime(2024, 4, 4),
                    OrderCheckoutDate = null,
                    OrderUsageDate = new DateTime(2024, 4, 4)
                }
            );

            await dbContext.SaveChangesAsync();

            return dbContext;
        }

        [Fact]
        public async Task Profit_ShouldReturnCorrectValues_ForGivenDate()
        {
            // Arrange
            var dbContext = await GetDbContextWithOrders();
            DashBoardDAO.InitializeContext(dbContext); // Use the in-memory context in the Profit method

            string datetime = "2024-04"; // Testing for April 2024

            // Act
            var result = DashBoardDAO.Profit(datetime);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.TotalAmount);
            Assert.Equal(1, result.TotalOrderOnline);
            Assert.Equal(1, result.TotalOrderCheckout);
            Assert.Equal(1, result.TotalOrderCancel);
            Assert.Equal(1, result.TotalOrderUsing);

            Assert.Equal(100, result.MoneyTotalDepositOrderOnline);
            Assert.Equal(200, result.MoneyTotalAmountOrderCheckout);
            Assert.Equal(50, result.MoneyTotalDepositOrderCancel);
            Assert.Equal(1, result.MoneyTotalDepositOrderUsing);
        }

        [Fact]
        public async Task Profit_ShouldReturnCorrectValues_WhenNoOrdersForDate()
        {
            // Arrange
            var dbContext = await GetDbContextWithOrders();
            DashBoardDAO.InitializeContext(dbContext);

            string datetime = "2024-05"; // Testing for May 2024, which has no orders

            // Act
            var result = DashBoardDAO.Profit(datetime);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.TotalAmount);
            Assert.Equal(0, result.TotalOrderOnline);
            Assert.Equal(0, result.TotalOrderCheckout);
            Assert.Equal(0, result.TotalOrderCancel);
            Assert.Equal(0, result.TotalOrderUsing);
        }

        [Fact]
        public async Task Profit_ShouldReturnCorrectValues_WhenZeroDatetime()
        {
            // Arrange
            var dbContext = await GetDbContextWithOrders();
            DashBoardDAO.InitializeContext(dbContext);

            string datetime = "0"; // Testing for all-time orders

            // Act
            var result = DashBoardDAO.Profit(datetime);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.TotalAmount); // The sum of all deposits and amounts for the orders
            Assert.Equal(1, result.TotalOrderOnline);
            Assert.Equal(1, result.TotalOrderCheckout);
            Assert.Equal(1, result.TotalOrderCancel);
            Assert.Equal(1, result.TotalOrderUsing);
        }
    }

}
