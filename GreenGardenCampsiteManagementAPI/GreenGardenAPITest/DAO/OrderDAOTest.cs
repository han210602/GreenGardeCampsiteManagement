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
                    new User { UserId = 1, FirstName = "John", LastName = "Doe", RoleId = 2, Email = "john.doe@example.com", Password = "password123" },
                    new User { UserId = 2, FirstName = "Jane", LastName = "Smith", RoleId = 2, Email = "jane.smith@example.com", Password = "password123" }
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
                        ActivityId = 1,
                        OrderDate = DateTime.UtcNow.AddDays(-5),
                        StatusOrder = true,
                        TotalAmount = 200.00m
                    },
                    new Order
                    {
                        OrderId = 2,
                        CustomerId = 1,
                        ActivityId = 2,
                        OrderDate = DateTime.UtcNow.AddDays(-3),
                        StatusOrder = false,
                        TotalAmount = 100.00m
                    },
                    new Order
                    {
                        OrderId = 3,
                        CustomerId = 1,
                        ActivityId = 2,
                        OrderDate = DateTime.UtcNow.AddDays(-4),
                        StatusOrder = true,
                        TotalAmount = 300.00m
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

        [Fact]
        public async Task GetCustomerOrders_ShouldReturnAllOrdersSortedByOrderDateDescending()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData();
            OrderDAO.InitializeContext(dbContext);

            // Act
            var result = OrderDAO.GetCustomerOrders(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal(2, result[0].ActivityId); // OrderId = 2 (most recent date)
            Assert.Equal(3, result[1].OrderId);   // OrderId = 3
            Assert.Equal(1, result[2].OrderId);   // OrderId = 1
        }

        [Fact]
        public async Task GetCustomerOrders_ShouldReturnOrdersWithStatusOrderTrue()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData();
            OrderDAO.InitializeContext(dbContext);

            // Act
            var result = OrderDAO.GetCustomerOrders(1, statusOrder: true);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, o => Assert.True(o.StatusOrder));
        }

        [Fact]
        public async Task GetCustomerOrders_ShouldReturnOrdersWithStatusOrderFalse()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData();
            OrderDAO.InitializeContext(dbContext);

            // Act
            var result = OrderDAO.GetCustomerOrders(1, statusOrder: false);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.False(result[0].StatusOrder);
        }

        [Fact]
        public async Task GetCustomerOrders_ShouldReturnOrdersWithSpecificActivityId()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData();
            OrderDAO.InitializeContext(dbContext);

            // Act
            var result = OrderDAO.GetCustomerOrders(1, activityId: 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, o => Assert.Equal(2, o.ActivityId));
        }

        [Fact]
        public async Task GetCustomerOrders_ShouldReturnOrdersWithStatusOrderTrueAndSpecificActivityId()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData();
            OrderDAO.InitializeContext(dbContext);

            // Act
            var result = OrderDAO.GetCustomerOrders(1, statusOrder: true, activityId: 2);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.True(result[0].StatusOrder);
            Assert.Equal(2, result[0].ActivityId);
        }

        [Fact]
        public async Task GetCustomerOrders_ShouldReturnEmptyListForNonExistentCustomer()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData();
            OrderDAO.InitializeContext(dbContext);

            // Act
            var result = OrderDAO.GetCustomerOrders(9999);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetCustomerOrders_ShouldReturnEmptyListForInvalidStatusAndActivityCombination()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData();
            OrderDAO.InitializeContext(dbContext);

            // Act
            var result = OrderDAO.GetCustomerOrders(1, statusOrder: true, activityId: 9999);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetCustomerOrders_ShouldReturnOrdersWithActivityId1()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData();
            OrderDAO.InitializeContext(dbContext);

            // Act
            var result = OrderDAO.GetCustomerOrders(1, activityId: 1);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result); // Only one order exists with ActivityId = 1
            Assert.All(result, o => Assert.Equal(1, o.ActivityId));
        }

        [Fact]
        public async Task GetCustomerOrders_ShouldReturnEmptyListForActivityId3()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData();
            OrderDAO.InitializeContext(dbContext);

            // Act
            var result = OrderDAO.GetCustomerOrders(1, activityId: 3);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result); // No orders exist with ActivityId = 3
        }

        [Fact]
        public async Task GetCustomerOrders_ShouldReturnEmptyList_WhenNoOrdersExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var emptyContext = new GreenGardenContext(options);
            emptyContext.Database.EnsureCreated();

            OrderDAO.InitializeContext(emptyContext);

            // Act
            var result = OrderDAO.GetCustomerOrders(1);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty(); // Expect an empty list
        }

        [Fact]
        public async Task GetCustomerOrders_ShouldThrowException_WhenDatabaseThrowsException()
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
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(OrderDAO.GetCustomerOrders(1)));
            exception.Message.Should().Be("Object reference not set to an instance of an object.");

        }

    }

}
