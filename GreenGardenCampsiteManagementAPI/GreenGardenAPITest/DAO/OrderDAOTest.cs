using BusinessObject.Models;
using Castle.Core.Resource;
using DataAccess.DAO;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGardenAPITest.DAO
{
    public class OrderDAOTest
    {
        private async Task<GreenGardenContext> GetDbContextWithMockData()
        {
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated();

            if (!await dbContext.Orders.AnyAsync())
            {
                dbContext.Roles.AddRange(
                    new Role { RoleId = 1, RoleName = "Admin" },
                    new Role { RoleId = 2, RoleName = "Customer" },
                    new Role { RoleId = 3, RoleName = "Employee" }
                );
                dbContext.Users.AddRange(
                    new User // Customer 1
                    {
                        UserId = 1,
                        FirstName = "John",
                        LastName = "Doe",
                        PhoneNumber = "123456789",
                        RoleId = 2,
                        Email = "john.doe@example.com", // Provide email
                        Password = "password123" // Provide password
                    },
                    new User // Customer 2
                    {
                        UserId = 2,
                        FirstName = "Jane",
                        LastName = "Smith",
                        PhoneNumber = "987654321",
                        RoleId = 2,
                        Email = "jane.smith@example.com", // Provide email
                        Password = "password123" // Provide password
                    },
                    new User // Employee 1
                    {
                        UserId = 3,
                        FirstName = "Alice",
                        LastName = "Brown",
                        RoleId = 3,
                        Email = "alice.brown@example.com", // Provide email
                        Password = "password123" // Provide password
                    },
                    new User // Employee 2
                    {
                        UserId = 4,
                        FirstName = "Bob",
                        LastName = "White",
                        RoleId = 3,
                        Email = "bob.white@example.com", // Provide email
                        Password = "password123" // Provide password
                    }
                );

                dbContext.Activities.AddRange(
                    new Activity { ActivityId = 1, ActivityName = "Outdoor Event" },
                    new Activity { ActivityId = 2, ActivityName = "Indoor Meeting" }
                );

                dbContext.Orders.AddRange(
                    new Order
                    {
                        OrderId = 1,
                        CustomerId = 1,
                        EmployeeId = 1,
                        OrderDate = DateTime.UtcNow.AddDays(-5),
                        OrderUsageDate = DateTime.UtcNow.AddDays(1),
                        Deposit = 50.00m,
                        TotalAmount = 200.00m,
                        AmountPayable = 150.00m,
                        StatusOrder = true,
                        ActivityId = 1,
                        OrderCheckoutDate = DateTime.UtcNow
                    },
                    new Order
                    {
                        OrderId = 2,
                        CustomerId = 2,
                        EmployeeId = 2,
                        OrderDate = DateTime.UtcNow.AddDays(-3),
                        OrderUsageDate = DateTime.UtcNow.AddDays(2),
                        Deposit = 30.00m,
                        TotalAmount = 100.00m,
                        AmountPayable = 70.00m,
                        StatusOrder = false,
                        ActivityId = 2,
                        OrderCheckoutDate = null
                    }
                );

                await dbContext.SaveChangesAsync();
            }

            return dbContext;
        }


        // Unit test for getAllOrder method
        [Fact]
        public async Task GetAllOrder_ShouldReturnListOfOrderDTO()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData();
            OrderDAO.InitializeContext(dbContext); // Initialize DAO context

            // Act
            var result = OrderDAO.getAllOrder();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2); // Ensure there are 2 orders in the result

            var firstOrder = result.First();
            firstOrder.OrderId.Should().Be(1);
            firstOrder.CustomerId.Should().Be(1);
            firstOrder.EmployeeId.Should().Be(1);
            firstOrder.ActivityName.Should().Be("Outdoor Event");
            firstOrder.TotalAmount.Should().Be(200.00m);
            firstOrder.StatusOrder.Should().BeTrue();
        }

        [Fact]
        public async Task GetAllOrder_ShouldReturnEmptyList_WhenNoOrdersExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var emptyContext = new GreenGardenContext(options);
            emptyContext.Database.EnsureCreated();

            OrderDAO.InitializeContext(emptyContext);

            // Act
            var result = OrderDAO.getAllOrder();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty(); // Expect an empty list
        }

        [Fact]
        public async Task GetAllOrder_ShouldThrowException_WhenDatabaseThrowsException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated();

            OrderDAO.InitializeContext(null);

            // Act & Assert
            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(OrderDAO.getAllOrder()));
            exception.Message.Should().Be("Object reference not set to an instance of an object.");

        }

        // Unit test for getAllOrder method
        private async Task<GreenGardenContext> GetDbContextWithMockData2()
        {
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated();

            // Seed mock data for customers, activities, and orders
            if (!await dbContext.Orders.AnyAsync())
            {
                dbContext.Users.AddRange(
                    new User { UserId = 1, FirstName = "John", LastName = "Doe", PhoneNumber = "123456789", RoleId = 2 },
                    new User { UserId = 2, FirstName = "Jane", LastName = "Smith", PhoneNumber = "987654321", RoleId = 2 }
                );

                dbContext.Activities.AddRange(
                    new Activity { ActivityId = 1, ActivityName = "Outdoor Event" },
                    new Activity { ActivityId = 2, ActivityName = "Indoor Meeting" }
                );

                dbContext.Orders.AddRange(
                    new Order
                    {
                        OrderId = 1,
                        CustomerId = 1,
                        EmployeeId = 1,
                        OrderDate = DateTime.UtcNow.AddDays(-5),
                        OrderUsageDate = DateTime.UtcNow.AddDays(1),
                        Deposit = 50.00m,
                        TotalAmount = 200.00m,
                        AmountPayable = 150.00m,
                        StatusOrder = true,
                        ActivityId = 1
                    },
                    new Order
                    {
                        OrderId = 2,
                        CustomerId = 2,
                        EmployeeId = 2,
                        OrderDate = DateTime.UtcNow.AddDays(-3),
                        OrderUsageDate = DateTime.UtcNow.AddDays(2),
                        Deposit = 30.00m,
                        TotalAmount = 100.00m,
                        AmountPayable = 70.00m,
                        StatusOrder = false,
                        ActivityId = 2
                    }
                );

                await dbContext.SaveChangesAsync();
            }

            return dbContext;
        }

        [Fact]
        public async Task GetCustomerOrders_ShouldReturnCustomerOrders_WhenValidCustomerId()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData2();
            OrderDAO.InitializeContext(dbContext); // Initialize DAO context

            // Act
            var result = OrderDAO.GetCustomerOrders(1); // Get orders for customer with ID 1

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count); // There should be only one order for customer with ID 1
            Assert.Equal(1, result[0].OrderId); // Check that the order ID matches
            Assert.Equal("John Doe", result[0].CustomerName); // Check customer name
            Assert.Equal("Outdoor Event", result[0].ActivityName); // Check activity name
            Assert.Equal(200.00m, result[0].TotalAmount); // Check total amount
        }

        [Fact]
        public async Task GetCustomerOrders_ShouldReturnEmptyList_WhenNoOrdersExistForCustomer()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData2();
            OrderDAO.InitializeContext(dbContext);

            // Act
            var result = OrderDAO.GetCustomerOrders(3); // Customer with ID 3 does not exist

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result); // Expect empty list since no orders exist for customer with ID 3
        }

        [Fact]
        public async Task GetCustomerOrders_ShouldReturnFilteredOrders_WhenStatusOrderIsProvided()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData2();
            OrderDAO.InitializeContext(dbContext);

            // Act
            var result = OrderDAO.GetCustomerOrders(1, statusOrder: true); // Filter by StatusOrder = true

            // Assert
            Assert.NotNull(result);
            Assert.Single(result); // Only one order should match with StatusOrder = true
            Assert.Equal(1, result[0].OrderId); // Check that the correct order is returned
        }

        [Fact]
        public async Task GetCustomerOrders_ShouldReturnFilteredOrders_WhenActivityIdIsProvided()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData2();
            OrderDAO.InitializeContext(dbContext);

            // Act
            var result = OrderDAO.GetCustomerOrders(1, activityId: 1); // Filter by ActivityId = 1

            // Assert
            Assert.NotNull(result);
            Assert.Single(result); // Only one order should match with ActivityId = 1
            Assert.Equal(1, result[0].OrderId); // Check that the correct order is returned
        }

        [Fact]
        public async Task GetCustomerOrders_ShouldThrowException_WhenDatabaseThrowsException()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData2();
            OrderDAO.InitializeContext(dbContext);

            // Simulate a database exception by disposing the context
            dbContext.Dispose();

            // Act & Assert
            await Assert.ThrowsAsync<ObjectDisposedException>(() => Task.Run(() => OrderDAO.GetCustomerOrders(1)));
        }
    }

}
