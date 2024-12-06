using BusinessObject.DTOs;
using BusinessObject.Models;
using Castle.Core.Resource;
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
            result.Should().HaveCount(3); // Ensure there are 2 orders in the result

            var firstOrder = result.First();
            firstOrder.OrderId.Should().Be(1);
            firstOrder.CustomerId.Should().Be(1);
            //firstOrder.EmployeeId.Should().Be(1);
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

        //[Fact]
        //public async Task GetCustomerOrders_ShouldReturnAllOrdersSortedByOrderDateDescending()
        //{
        //    // Arrange
        //    var dbContext = await GetDbContextWithMockData();
        //    OrderDAO.InitializeContext(dbContext);

        //    // Act
        //    var result = OrderDAO.GetCustomerOrders(1);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal(3, result.Count);
        //    Assert.Equal(2, result[0].OrderId); // OrderId = 2 (most recent date) 
        //    Assert.Equal(3, result[1].OrderId);   // OrderId = 3
        //    Assert.Equal(1, result[2].OrderId);   // OrderId = 1
        //}

        [Fact] // Unit test for getAllOrder method
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

        // Unit test for GetAllOrderDepositAndUsing method
        [Fact]
        public async Task GetAllOrderDepositAndUsing_ShouldReturnCorrectOrders()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData(); // Create a mock context with data
            OrderDAO.InitializeContext(dbContext); // Initialize OrderDAO with the context

            // Act
            var result = OrderDAO.getAllOrderDepositAndUsing(); // Call the method

            // Assert
            Assert.NotNull(result); // Ensure result is not null
            Assert.Single(result); // Only one order matches the criteria in mock data

            var order = result.First(); // Get the first (and only) order
            Assert.Equal(2, order.ActivityId); // Ensure ActivityId = 2
            Assert.True(order.StatusOrder); // Ensure StatusOrder = true
            Assert.Equal(3, order.OrderId); // Check the OrderId of the matching order
            Assert.Equal("JohnDoe", order.CustomerName); // Check CustomerName (mock data concatenation)


        }

        [Fact]
        public async Task GetAllOrderDepositAndUsing_ShouldReturnEmptyList_WhenNoOrdersExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var emptyContext = new GreenGardenContext(options);
            emptyContext.Database.EnsureCreated();

            OrderDAO.InitializeContext(emptyContext);

            // Act
            var result = OrderDAO.getAllOrderDepositAndUsing();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty(); // Expect an empty list
        }

        [Fact]
        public async Task GetAllOrderDepositAndUsing_ShouldThrowException_WhenDatabaseThrowsException()
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
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(OrderDAO.getAllOrderDepositAndUsing()));
            exception.Message.Should().Be("Object reference not set to an instance of an object.");

        }

        // Unit test for EnterDeposit method
        private async Task<GreenGardenContext> GetDbContextWithMockData2()
        {
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique in-memory DB for testing
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
                    new User
                    {
                        UserId = 1,
                        FirstName = "John",
                        LastName = "Doe",
                        PhoneNumber = "123456789",
                        RoleId = 2,
                        Email = "john.doe@example.com",
                        Password = "password123"
                    },
                    new User
                    {
                        UserId = 2,
                        FirstName = "Jane",
                        LastName = "Smith",
                        PhoneNumber = "987654321",
                        RoleId = 3,
                        Email = "jane.smith@example.com",
                        Password = "password123"
                    }
                );

                dbContext.Activities.AddRange(
                    new Activity { ActivityId = 1, ActivityName = "Outdoor Event" },
                    new Activity { ActivityId = 2, ActivityName = "Indoor Event" }
                );

                dbContext.Orders.AddRange(
                    new Order
                    {
                        OrderId = 1,
                        CustomerId = 1,
                        EmployeeId = 2,
                        OrderDate = DateTime.UtcNow.AddDays(-5),
                        OrderUsageDate = DateTime.UtcNow.AddDays(2),
                        Deposit = 0,
                        TotalAmount = 200.00m,
                        AmountPayable = 0,
                        StatusOrder = false,
                        ActivityId = 1
                    },
                    new Order
                    {
                        OrderId = 2,
                        CustomerId = 1,
                        EmployeeId = 2,
                        OrderDate = DateTime.UtcNow.AddDays(-2),
                        OrderUsageDate = DateTime.UtcNow.AddDays(1),
                        Deposit = 70.00m,
                        TotalAmount = 300.00m,
                        AmountPayable = 230.00m,
                        StatusOrder = true,
                        ActivityId = 2
                    }
                );

                await dbContext.SaveChangesAsync();
            }

            return dbContext;
        }

        //[Fact]
        //public async Task EnterDeposit_ShouldUpdateOrder_WhenOrderExists()
        //{
        //    // Arrange
        //    var dbContext = await GetDbContextWithMockData2();
        //    OrderDAO.InitializeContext(dbContext); // Initialize the DAO with the context

        //    int orderId = 1; // An order that exists in the mock data
        //    decimal depositAmount = 100.00m; // New deposit amount

        //    // Act
        //    var result = OrderDAO.EnterDeposit(orderId, depositAmount); // Call the method

        //    // Assert
        //    Assert.True(result); // Ensure the method returns true

        //    var updatedOrder = await dbContext.Orders.FindAsync(orderId); // Fetch the updated order

        //    Assert.NotNull(updatedOrder); // Ensure the order exists
        //    Assert.Equal(depositAmount, updatedOrder.Deposit); // Check if deposit is updated
        //    Assert.True(updatedOrder.StatusOrder); // Ensure StatusOrder is set to true
        //    Assert.Equal(updatedOrder.TotalAmount - depositAmount, updatedOrder.AmountPayable); // Check AmountPayable
        //}

        [Fact]
        public async Task EnterDeposit_ShouldReturnFalse_WhenOrderDoesNotExist()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData2();
            OrderDAO.InitializeContext(dbContext);

            int nonExistentOrderId = 9999; // Order that doesn't exist in the mock data
            decimal depositAmount = 100.00m;

            // Act
            var result = OrderDAO.EnterDeposit(nonExistentOrderId, depositAmount); // Call the method

            // Assert
            Assert.False(result); // Ensure the method returns false
        }


        // Unit test for DeleteOrder method
        private async Task<GreenGardenContext> GetDbContextWithMockDataForDelete()
        {
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated();

            if (!await dbContext.Orders.AnyAsync())
            {
                dbContext.Orders.AddRange(
                    new Order { OrderId = 1, CustomerId = 1, TotalAmount = 100.00m },
                    new Order { OrderId = 2, CustomerId = 2, TotalAmount = 200.00m }
                );

                dbContext.OrderTicketDetails.AddRange(
                    new OrderTicketDetail { TicketId = 1, OrderId = 1 },
                    new OrderTicketDetail { TicketId = 2, OrderId = 1 }
                );

                dbContext.OrderFoodDetails.AddRange(
                    new OrderFoodDetail { ItemId = 1, OrderId = 1 },
                    new OrderFoodDetail { ItemId = 2, OrderId = 1 }
                );

                dbContext.OrderCampingGearDetails.AddRange(
                    new OrderCampingGearDetail { GearId = 1, OrderId = 1 }
                );

                dbContext.OrderFoodComboDetails.AddRange(
                    new OrderFoodComboDetail { ComboId = 1, OrderId = 1 }
                );

                await dbContext.SaveChangesAsync();
            }

            return dbContext;
        }

        //[Fact]
        //public async Task DeleteOrder_ShouldDeleteOrderAndRelatedDetails_WhenOrderExists()
        //{
        //    // Arrange
        //    var dbContext = await GetDbContextWithMockDataForDelete();
        //    OrderDAO.InitializeContext(dbContext);

        //    int orderIdToDelete = 1; // Existing order ID with related details.

        //    // Pre-Assert: Ensure the order and related details exist before deletion
        //    Assert.NotNull(dbContext.Orders.FirstOrDefault(o => o.OrderId == orderIdToDelete));
        //    Assert.NotEmpty(dbContext.OrderTicketDetails.Where(o => o.OrderId == orderIdToDelete));
        //    Assert.NotEmpty(dbContext.OrderFoodDetails.Where(o => o.OrderId == orderIdToDelete));
        //    Assert.NotEmpty(dbContext.OrderCampingGearDetails.Where(o => o.OrderId == orderIdToDelete));
        //    Assert.NotEmpty(dbContext.OrderFoodComboDetails.Where(o => o.OrderId == orderIdToDelete));

        //    // Act
        //    var result = OrderDAO.DeleteOrder(orderIdToDelete);

        //    // Assert: Check deletion result
        //    Assert.True(result);

        //    // Post-Assert: Verify order and related details are deleted
        //    Assert.Null(dbContext.Orders.FirstOrDefault(o => o.OrderId == orderIdToDelete));
        //    Assert.Empty(dbContext.OrderTicketDetails.Where(o => o.OrderId == orderIdToDelete));
        //    Assert.Empty(dbContext.OrderFoodDetails.Where(o => o.OrderId == orderIdToDelete));
        //    Assert.Empty(dbContext.OrderCampingGearDetails.Where(o => o.OrderId == orderIdToDelete));
        //    Assert.Empty(dbContext.OrderFoodComboDetails.Where(o => o.OrderId == orderIdToDelete));
        //}

        [Fact]
        public async Task DeleteOrder_ShouldReturnFalse_WhenOrderDoesNotExist()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockDataForDelete();
            OrderDAO.InitializeContext(dbContext);

            int nonExistentOrderId = 9999; // Non-existent order ID.

            // Act
            var result = OrderDAO.DeleteOrder(nonExistentOrderId);

            // Assert
            Assert.False(result); // Ensure the deletion returns false for non-existent order.
        }

        // Unit test for CreateUniqueOrder method
        private async Task<GreenGardenContext> GetDbContextWithMockDataForCreateUniqueOrderTests()
        {
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated();

            // Seed initial data
            if (!await dbContext.Tickets.AnyAsync())
            {
                dbContext.Roles.AddRange(
                    new Role { RoleId = 1, RoleName = "Admin" },
                    new Role { RoleId = 2, RoleName = "Customer" },
                    new Role { RoleId = 3, RoleName = "Employee" }
                );

                // Add Customers
                dbContext.Users.AddRange(
                    new User
                    {
                        UserId = 1,
                        FirstName = "Customer",
                        LastName = "One",
                        PhoneNumber = "123456789",
                        RoleId = 3
                    },
                    new User
                    {
                        UserId = 2,
                        FirstName = "Customer",
                        LastName = "Two",
                        PhoneNumber = "987654321",
                        RoleId = 3
                    }
                );

                // Add Tickets
                dbContext.Tickets.AddRange(
                    new Ticket { TicketId = 1, TicketName = "Standard Ticket", Price = 20.00m },
                    new Ticket { TicketId = 2, TicketName = "VIP Ticket", Price = 50.00m }
                );

                // Add Camping Gears
                dbContext.CampingGears.AddRange(
                    new CampingGear { GearId = 1, GearName = "Tent", RentalPrice = 30.00m },
                    new CampingGear { GearId = 2, GearName = "Sleeping Bag", RentalPrice = 15.00m }
                );

                // Add Food Items
                dbContext.FoodAndDrinks.AddRange(
                    new FoodAndDrink { ItemId = 1, ItemName = "Vegan Meal", Price = 10.00m },
                    new FoodAndDrink { ItemId = 2, ItemName = "Chicken Meal", Price = 15.00m }
                );

                // Add Food Combos
                dbContext.FoodCombos.AddRange(
                    new FoodCombo { ComboId = 1, ComboName = "Family Combo", Price = 25.00m },
                    new FoodCombo { ComboId = 2, ComboName = "Party Combo", Price = 40.00m }
                );

                await dbContext.SaveChangesAsync();
            }

            return dbContext;
        }

        [Fact]
        public async Task CreateUniqueOrder_ShouldCreateOrderWithAllDetails_WhenAllFieldsAreProvided()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData(); // Ensure this method sets up necessary test data
            OrderDAO.InitializeContext(dbContext);

            var orderRequest = new CreateUniqueOrderRequest
            {
                Order = new OrderAddDTO
                {
                    EmployeeId = 1,
                    CustomerName = "John Doe",
                    OrderUsageDate = DateTime.UtcNow.AddDays(5),
                    Deposit = 50.00m,
                    TotalAmount = 200.00m,
                    PhoneCustomer = "123456789"
                },
                OrderTicket = new List<OrderTicketAddlDTO>
        {
            new OrderTicketAddlDTO { TicketId = 1, Quantity = 2 },
            new OrderTicketAddlDTO { TicketId = 2, Quantity = 1 }
        },
                OrderCampingGear = new List<OrderCampingGearAddDTO>
        {
            new OrderCampingGearAddDTO { GearId = 1, Quantity = 3 }
        },
                OrderFood = new List<OrderFoodAddDTO>
        {
            new OrderFoodAddDTO { ItemId = 1, Quantity = 2, Description = "Vegan Meal" }
        },
                OrderFoodCombo = new List<OrderFoodComboAddDTO>
        {
            new OrderFoodComboAddDTO { ComboId = 1, Quantity = 1 }
        }
            };

            // Act
            var result = OrderDAO.CreateUniqueOrder(orderRequest);

            // Assert

            Assert.True(result); // Ensure the method returns true

            // Verify the order and associated details are added correctly
            var createdOrder = dbContext.Orders.FirstOrDefault(o => o.CustomerName == "John Doe");
            Assert.NotNull(createdOrder);
            Assert.Equal(50.00m, createdOrder.Deposit);
            Assert.Equal(150.00m, createdOrder.AmountPayable);

            var orderTickets = dbContext.OrderTicketDetails.Where(t => t.OrderId == createdOrder.OrderId).ToList();
            Assert.Equal(2, orderTickets.Count);
            Assert.Contains(orderTickets, t => t.TicketId == 1 && t.Quantity == 2);
            Assert.Contains(orderTickets, t => t.TicketId == 2 && t.Quantity == 1);

            var orderGears = dbContext.OrderCampingGearDetails.Where(g => g.OrderId == createdOrder.OrderId).ToList();
            Assert.Single(orderGears);
            Assert.Contains(orderGears, g => g.GearId == 1 && g.Quantity == 3);

            var orderFoods = dbContext.OrderFoodDetails.Where(f => f.OrderId == createdOrder.OrderId).ToList();
            Assert.Single(orderFoods);
            Assert.Contains(orderFoods, f => f.ItemId == 1 && f.Quantity == 2 && f.Description == "Vegan Meal");

            var orderCombos = dbContext.OrderFoodComboDetails.Where(c => c.OrderId == createdOrder.OrderId).ToList();
            Assert.Single(orderCombos);
            Assert.Contains(orderCombos, c => c.ComboId == 1 && c.Quantity == 1);
        }

        [Fact]
        public async Task CreateUniqueOrder_ShouldReturnFalse_WhenOrderTicketIsNull()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData();
            OrderDAO.InitializeContext(dbContext);

            var orderRequest = new CreateUniqueOrderRequest
            {
                Order = new OrderAddDTO
                {
                    EmployeeId = 1,
                    CustomerName = "John Doe",
                    OrderUsageDate = DateTime.UtcNow.AddDays(5),
                    Deposit = 50.00m,
                    TotalAmount = 200.00m,
                    PhoneCustomer = "123456789"
                },
                OrderTicket = null, // Ticket is null
                OrderCampingGear = null,
                OrderFood = null,
                OrderFoodCombo = null
            };

            // Act
            var result = OrderDAO.CreateUniqueOrder(orderRequest);

            // Assert
            Assert.False(result);
        }

        //[Fact]
        //public async Task CreateUniqueOrder_ShouldCreateOrder_WhenOnlyOrderTicketIsProvided()
        //{
        //    // Arrange
        //    var dbContext = await GetDbContextWithMockData();
        //    OrderDAO.InitializeContext(dbContext);

        //    var orderRequest = new CreateUniqueOrderRequest
        //    {
        //        Order = new OrderAddDTO
        //        {
        //            EmployeeId = 1,
        //            CustomerName = "John Doe",
        //            OrderUsageDate = DateTime.UtcNow.AddDays(5),
        //            Deposit = 50.00m,
        //            TotalAmount = 200.00m,
        //            PhoneCustomer = "123456789"
        //        },
        //        OrderTicket = new List<OrderTicketAddlDTO>
        //{
        //    new OrderTicketAddlDTO { TicketId = 1, Quantity = 2 }
        //},
        //        OrderCampingGear = null,
        //        OrderFood = null,
        //        OrderFoodCombo = null
        //    };

        //    // Act
        //    var result = OrderDAO.CreateUniqueOrder(orderRequest);

        //    // Assert
        //    Assert.True(result);

        //    var createdOrder = dbContext.Orders.FirstOrDefault(o => o.CustomerName == "John Doe");
        //    Assert.NotNull(createdOrder);

        //    var orderTickets = dbContext.OrderTicketDetails.Where(t => t.OrderId == createdOrder.OrderId).ToList();
        //    Assert.Single(orderTickets);
        //    Assert.Contains(orderTickets, t => t.TicketId == 1 && t.Quantity == 2);
        //}

        [Fact]
        public async Task CreateUniqueOrder_ShouldCreateOrder_WhenOnlyOrderCampingGearIsNull()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData();
            OrderDAO.InitializeContext(dbContext);

            var orderRequest = new CreateUniqueOrderRequest
            {
                Order = new OrderAddDTO
                {
                    EmployeeId = 1,
                    CustomerName = "John Doe",
                    OrderUsageDate = DateTime.UtcNow.AddDays(5),
                    Deposit = 50.00m,
                    TotalAmount = 200.00m,
                    PhoneCustomer = "123456789"
                },
                OrderTicket = new List<OrderTicketAddlDTO>
        {
            new OrderTicketAddlDTO { TicketId = 1, Quantity = 2 }
        },
                OrderCampingGear = null, // Camping gear is null
                OrderFood = new List<OrderFoodAddDTO>
        {
            new OrderFoodAddDTO { ItemId = 1, Quantity = 2, Description = "Vegan Meal" }
        },
                OrderFoodCombo = new List<OrderFoodComboAddDTO>
        {
            new OrderFoodComboAddDTO { ComboId = 1, Quantity = 1 }
        }
            };

            // Act
            var result = OrderDAO.CreateUniqueOrder(orderRequest);

            // Assert
            Assert.True(result);

            var createdOrder = dbContext.Orders.FirstOrDefault(o => o.CustomerName == "John Doe");
            Assert.NotNull(createdOrder);

            var orderTickets = dbContext.OrderTicketDetails.Where(t => t.OrderId == createdOrder.OrderId).ToList();
            Assert.Single(orderTickets);
            Assert.Contains(orderTickets, t => t.TicketId == 1 && t.Quantity == 2);

            var orderFoods = dbContext.OrderFoodDetails.Where(f => f.OrderId == createdOrder.OrderId).ToList();
            Assert.Single(orderFoods);
            Assert.Contains(orderFoods, f => f.ItemId == 1 && f.Quantity == 2 && f.Description == "Vegan Meal");

            var orderCombos = dbContext.OrderFoodComboDetails.Where(c => c.OrderId == createdOrder.OrderId).ToList();
            Assert.Single(orderCombos);
            Assert.Contains(orderCombos, c => c.ComboId == 1 && c.Quantity == 1);
        }

        [Fact]
        public async Task CreateUniqueOrder_ShouldCreateOrder_WhenOnlyOrderFoodIsNull()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData();
            OrderDAO.InitializeContext(dbContext);

            var orderRequest = new CreateUniqueOrderRequest
            {
                Order = new OrderAddDTO
                {
                    EmployeeId = 1,
                    CustomerName = "John Doe",
                    OrderUsageDate = DateTime.UtcNow.AddDays(5),
                    Deposit = 50.00m,
                    TotalAmount = 200.00m,
                    PhoneCustomer = "123456789"
                },
                OrderTicket = new List<OrderTicketAddlDTO>
        {
            new OrderTicketAddlDTO { TicketId = 1, Quantity = 2 }
        },
                OrderCampingGear = new List<OrderCampingGearAddDTO>
        {
            new OrderCampingGearAddDTO { GearId = 1, Quantity = 3 }
        },
                OrderFood = null, // Food is null
                OrderFoodCombo = new List<OrderFoodComboAddDTO>
        {
            new OrderFoodComboAddDTO { ComboId = 1, Quantity = 1 }
        }
            };

            // Act
            var result = OrderDAO.CreateUniqueOrder(orderRequest);

            // Assert
            Assert.True(result);

            var createdOrder = dbContext.Orders.FirstOrDefault(o => o.CustomerName == "John Doe");
            Assert.NotNull(createdOrder);

            var orderTickets = dbContext.OrderTicketDetails.Where(t => t.OrderId == createdOrder.OrderId).ToList();
            Assert.Single(orderTickets);
            Assert.Contains(orderTickets, t => t.TicketId == 1 && t.Quantity == 2);

            var orderGears = dbContext.OrderCampingGearDetails.Where(g => g.OrderId == createdOrder.OrderId).ToList();
            Assert.Single(orderGears);
            Assert.Contains(orderGears, g => g.GearId == 1 && g.Quantity == 3);

            var orderCombos = dbContext.OrderFoodComboDetails.Where(c => c.OrderId == createdOrder.OrderId).ToList();
            Assert.Single(orderCombos);
            Assert.Contains(orderCombos, c => c.ComboId == 1 && c.Quantity == 1);
        }

        [Fact]
        public async Task CreateUniqueOrder_ShouldCreateOrder_WhenOnlyOrderFoodComboIsNull()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData();
            OrderDAO.InitializeContext(dbContext);

            var orderRequest = new CreateUniqueOrderRequest
            {
                Order = new OrderAddDTO
                {
                    EmployeeId = 1,
                    CustomerName = "John Doe",
                    OrderUsageDate = DateTime.UtcNow.AddDays(5),
                    Deposit = 50.00m,
                    TotalAmount = 200.00m,
                    PhoneCustomer = "123456789"
                },
                OrderTicket = new List<OrderTicketAddlDTO>
        {
            new OrderTicketAddlDTO { TicketId = 1, Quantity = 2 }
        },
                OrderCampingGear = new List<OrderCampingGearAddDTO>
        {
            new OrderCampingGearAddDTO { GearId = 1, Quantity = 3 }
        },
                OrderFood = new List<OrderFoodAddDTO>
        {
            new OrderFoodAddDTO { ItemId = 1, Quantity = 2, Description = "Vegan Meal" }
        },
                OrderFoodCombo = null // Food combo is null
            };

            // Act
            var result = OrderDAO.CreateUniqueOrder(orderRequest);

            // Assert
            Assert.True(result);

            var createdOrder = dbContext.Orders.FirstOrDefault(o => o.CustomerName == "John Doe");
            Assert.NotNull(createdOrder);

            var orderTickets = dbContext.OrderTicketDetails.Where(t => t.OrderId == createdOrder.OrderId).ToList();
            Assert.Single(orderTickets);
            Assert.Contains(orderTickets, t => t.TicketId == 1 && t.Quantity == 2);

            var orderGears = dbContext.OrderCampingGearDetails.Where(g => g.OrderId == createdOrder.OrderId).ToList();
            Assert.Single(orderGears);
            Assert.Contains(orderGears, g => g.GearId == 1 && g.Quantity == 3);

            var orderFoods = dbContext.OrderFoodDetails.Where(f => f.OrderId == createdOrder.OrderId).ToList();
            Assert.Single(orderFoods);
            Assert.Contains(orderFoods, f => f.ItemId == 1 && f.Quantity == 2 && f.Description == "Vegan Meal");
        }

        // Unit test for GetOrderDetail method
        [Fact]
        public async Task GetOrderDetail_ShouldReturnOrderDetails_WhenOrderIdExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new GreenGardenContext(options))
            {
                // Seed mock data
                dbContext.Roles.Add(new Role { RoleId = 3, RoleName = "Employee" });
                dbContext.Users.Add(new User
                {
                    UserId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "customer1@example.com",  // Add Email
                    Password = "hashed_password1",  // Add Password
                    RoleId = 3
                });

                dbContext.Tickets.AddRange(
                    new Ticket { TicketId = 1, TicketName = "VIP Ticket", Price = 50.00m }
                );

                dbContext.CampingGears.Add(new CampingGear { GearId = 1, GearName = "Tent", RentalPrice = 30.00m });

                dbContext.FoodAndDrinks.Add(new FoodAndDrink { ItemId = 1, ItemName = "Vegan Meal", Price = 15.00m });

                dbContext.FoodCombos.Add(new FoodCombo { ComboId = 1, ComboName = "Family Combo", Price = 25.00m });

                var order = new Order
                {
                    OrderId = 100,
                    EmployeeId = 1,
                    CustomerName = "Jane Doe",
                    OrderDate = DateTime.UtcNow,
                    OrderUsageDate = DateTime.UtcNow.AddDays(5),
                    Deposit = 50.00m,
                    TotalAmount = 200.00m,
                    AmountPayable = 150.00m,
                    StatusOrder = true,
                    ActivityId = 1,
                    PhoneCustomer = "123456789"
                };
                dbContext.Orders.Add(order);

                dbContext.OrderTicketDetails.Add(new OrderTicketDetail
                {
                    OrderId = 100,
                    TicketId = 1,
                    Quantity = 2
                });

                dbContext.OrderCampingGearDetails.Add(new OrderCampingGearDetail
                {
                    OrderId = 100,
                    GearId = 1,
                    Quantity = 3
                });

                dbContext.OrderFoodDetails.Add(new OrderFoodDetail
                {
                    OrderId = 100,
                    ItemId = 1,
                    Quantity = 2
                });

                dbContext.OrderFoodComboDetails.Add(new OrderFoodComboDetail
                {
                    OrderId = 100,
                    ComboId = 1,
                    Quantity = 1
                });

                await dbContext.SaveChangesAsync();

                // Check if the data is saved correctly
                var orderInDb = await dbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == 100);
                Assert.NotNull(orderInDb); // Verify that the order exists in the DB

                var ticketDetailsInDb = await dbContext.OrderTicketDetails.FirstOrDefaultAsync(t => t.OrderId == 100);
                Assert.NotNull(ticketDetailsInDb); // Verify ticket details exist

                var campingGearDetailsInDb = await dbContext.OrderCampingGearDetails.FirstOrDefaultAsync(g => g.OrderId == 100);
                Assert.NotNull(campingGearDetailsInDb); // Verify camping gear details exist

                var foodDetailsInDb = await dbContext.OrderFoodDetails.FirstOrDefaultAsync(f => f.OrderId == 100);
                Assert.NotNull(foodDetailsInDb); // Verify food details exist

                var foodComboDetailsInDb = await dbContext.OrderFoodComboDetails.FirstOrDefaultAsync(c => c.OrderId == 100);
                Assert.NotNull(foodComboDetailsInDb); // Verify food combo details exist



                OrderDAO.InitializeContext(dbContext);

                // Act
                var result = OrderDAO.GetOrderDetail(100);

                // Assert
                Assert.NotNull(result);  // Ensure the result is not null
                Assert.Equal(100, result.OrderId);  // Assert Order ID
                Assert.Equal("JohnDoe", result.EmployeeName);  // Correct employee name
                Assert.Equal("Jane Doe", result.CustomerName);  // Correct customer name
                Assert.Equal(50.00m, result.Deposit);  // Assert Deposit
                Assert.Equal(150.00m, result.AmountPayable);  // Assert Amount Payable
                Assert.Equal(1, result.OrderTicketDetails.Count);  // Check the number of tickets
                Assert.Equal(1, result.OrderCampingGearDetails.Count);  // Check the number of camping gear
                Assert.Equal(1, result.OrderFoodDetails.Count);  // Check the number of food items
                Assert.Equal(1, result.OrderFoodComboDetails.Count);  // Check the number of food combos

                // Verify ticket details
                var ticket = result.OrderTicketDetails.First();
                Assert.Equal(1, ticket.TicketId);
                Assert.Equal("VIP Ticket", ticket.Name);
                Assert.Equal(2, ticket.Quantity);
                Assert.Equal(50.00m, ticket.Price);

                // Verify camping gear details
                var campingGear = result.OrderCampingGearDetails.First();
                Assert.Equal(1, campingGear.GearId);
                Assert.Equal("Tent", campingGear.Name);
                Assert.Equal(3, campingGear.Quantity);
                Assert.Equal(30.00m, campingGear.Price);

                // Verify food details
                var food = result.OrderFoodDetails.First();
                Assert.Equal(1, food.ItemId);
                Assert.Equal("Vegan Meal", food.Name);
                Assert.Equal(2, food.Quantity);
                Assert.Equal(15.00m, food.Price);

                // Verify food combo details
                var combo = result.OrderFoodComboDetails.First();
                Assert.Equal(1, combo.ComboId);
                Assert.Equal("Family Combo", combo.Name);
                Assert.Equal(1, combo.Quantity);
                Assert.Equal(25.00m, combo.Price);
            }
        }

        [Fact]
        public void GetOrderDetail_ShouldReturnNull_WhenOrderIdDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new GreenGardenContext(options))
            {
                dbContext.Database.EnsureCreated();
            }

            OrderDAO.InitializeContext(new GreenGardenContext(options));

            // Act
            var result = OrderDAO.GetOrderDetail(999); // Non-existent ID

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetOrderDetail_ShouldThrowException_WhenDatabaseThrowsException()
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
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(OrderDAO.GetOrderDetail(1)));
            exception.Message.Should().Be("Object reference not set to an instance of an object.");

        }

        // Unit test for GetCustomerOrderDetail method
        //[Fact]
        //public async Task GetCustomerOrderDetail_ShouldReturnCorrectOrderDetails_WhenOrderIdExists()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<GreenGardenContext>()
        //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //        .Options;

        //    using (var dbContext = new GreenGardenContext(options))
        //    {
        //        // Seed mock data
        //        dbContext.Roles.Add(new Role { RoleId = 3, RoleName = "Employee" });
        //        dbContext.Users.Add(new User
        //        {
        //            UserId = 1,
        //            FirstName = "John",
        //            LastName = "Doe",
        //            Email = "customer1@example.com",  // Add Email
        //            Password = "hashed_password1",  // Add Password
        //            RoleId = 3
        //        });

        //        dbContext.Tickets.Add(new Ticket { TicketId = 1, TicketName = "VIP Ticket", Price = 50.00m });

        //        dbContext.CampingGears.Add(new CampingGear { GearId = 1, GearName = "Tent", RentalPrice = 30.00m });

        //        dbContext.FoodAndDrinks.Add(new FoodAndDrink { ItemId = 1, ItemName = "Vegan Meal", Price = 15.00m });

        //        dbContext.FoodCombos.Add(new FoodCombo { ComboId = 1, ComboName = "Family Combo", Price = 25.00m });

        //        var order = new Order
        //        {
        //            OrderId = 100,
        //            CustomerName = "Jane Doe",
        //            OrderDate = DateTime.UtcNow,
        //            OrderUsageDate = DateTime.UtcNow.AddDays(5),
        //            Deposit = 50.00m,
        //            TotalAmount = 200.00m,
        //            AmountPayable = 150.00m,
        //            StatusOrder = true,
        //            ActivityId = 1,
        //            PhoneCustomer = "123456789"
        //        };
        //        dbContext.Orders.Add(order);

        //        dbContext.OrderTicketDetails.Add(new OrderTicketDetail
        //        {
        //            OrderId = 100,
        //            TicketId = 1,
        //            Quantity = 2
        //        });

        //        dbContext.OrderCampingGearDetails.Add(new OrderCampingGearDetail
        //        {
        //            OrderId = 100,
        //            GearId = 1,
        //            Quantity = 3
        //        });

        //        dbContext.OrderFoodDetails.Add(new OrderFoodDetail
        //        {
        //            OrderId = 100,
        //            ItemId = 1,
        //            Quantity = 2
        //        });

        //        dbContext.OrderFoodComboDetails.Add(new OrderFoodComboDetail
        //        {
        //            OrderId = 100,
        //            ComboId = 1,
        //            Quantity = 1
        //        });

        //        dbContext.OrderComboDetails.Add(new OrderComboDetail
        //        {
        //            OrderId = 100,
        //            ComboId = 1,
        //            Quantity = 1
        //        });

        //        await dbContext.SaveChangesAsync();

        //    }

        //    // Act
        //    CustomerOrderDetailDTO result;
        //    using (var dbContext = new GreenGardenContext(options))
        //    {
        //        OrderDAO.InitializeContext(dbContext);  // Assuming InitializeContext is necessary

        //        result = OrderDAO.GetCustomerOrderDetail(100); // Get order details by ID
        //    }

        //    // Assert
        //    Assert.NotNull(result);  // Ensure the result is not null
        //    Assert.Equal(100, result.OrderId);  // Assert Order ID
        //    //Assert.Equal("Jane Doe", result.CustomerName);  // Assert Customer Name
        //    Assert.Equal("123456789", result.PhoneCustomer);  // Assert Phone Number
        //    Assert.Equal(50.00m, result.Deposit);  // Assert Deposit
        //    Assert.Equal(200.00m, result.TotalAmount);  // Assert Total Amount
        //    Assert.Equal(150.00m, result.AmountPayable);  // Assert Amount Payable
        //    Assert.True(result.StatusOrder.HasValue);  // Assert StatusOrder is not null
        //    Assert.Equal(1, result.ActivityId);  // Assert ActivityId
        //    //Assert.Equal("Activity Name", result.ActivityName);  // Assert ActivityName (assuming it's set in the mock)

        //    // Verify related data
        //    Assert.Single(result.OrderTicketDetails);  // Ensure there is one ticket detail
        //    var ticketDetail = result.OrderTicketDetails.First();
        //    Assert.Equal(1, ticketDetail.TicketId);
        //    Assert.Equal("VIP Ticket", ticketDetail.Name);
        //    Assert.Equal(2, ticketDetail.Quantity);
        //    Assert.Equal(50.00m, ticketDetail.Price);

        //    Assert.Single(result.OrderCampingGearDetails);  // Ensure there is one camping gear detail
        //    var gearDetail = result.OrderCampingGearDetails.First();
        //    Assert.Equal(1, gearDetail.GearId);
        //    Assert.Equal("Tent", gearDetail.Name);
        //    Assert.Equal(3, gearDetail.Quantity);
        //    Assert.Equal(30.00m, gearDetail.Price);

        //    Assert.Single(result.OrderFoodDetails);  // Ensure there is one food detail
        //    var foodDetail = result.OrderFoodDetails.First();
        //    Assert.Equal(1, foodDetail.ItemId);
        //    Assert.Equal("Vegan Meal", foodDetail.Name);
        //    Assert.Equal(2, foodDetail.Quantity);
        //    Assert.Equal(15.00m, foodDetail.Price);

        //    Assert.Single(result.OrderFoodComboDetails);  // Ensure there is one food combo detail
        //    var foodComboDetail = result.OrderFoodComboDetails.First();
        //    Assert.Equal(1, foodComboDetail.ComboId);
        //    Assert.Equal("Family Combo", foodComboDetail.Name);
        //    Assert.Equal(1, foodComboDetail.Quantity);
        //    Assert.Equal(25.00m, foodComboDetail.Price);

        //    Assert.Single(result.OrderComboDetails);  // Ensure there is one combo detail
        //    var comboDetail = result.OrderComboDetails.First();
        //    Assert.Equal(1, comboDetail.ComboId);
        //    Assert.Equal("Family Combo", comboDetail.Name);
        //    Assert.Equal(1, comboDetail.Quantity);
        //    Assert.Equal(25.00m, comboDetail.Price);
        //}

        [Fact]
        public void GetCustomerOrderDetail_ShouldReturnNull_WhenOrderIdDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new GreenGardenContext(options))
            {
                dbContext.Database.EnsureCreated();
            }

            OrderDAO.InitializeContext(new GreenGardenContext(options));

            // Act
            var result = OrderDAO.GetCustomerOrderDetail(999); // Non-existent ID

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetCustomerOrderDetail_ShouldThrowException_WhenDatabaseThrowsException()
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
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(OrderDAO.GetCustomerOrderDetail(1)));
            exception.Message.Should().Be("Object reference not set to an instance of an object.");

        }

        // Test for UpdateActivityOrder method
        [Fact]
        public async Task UpdateActivityOrder_ShouldUpdateActivityId_WhenOrderExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new GreenGardenContext(options))
            {
                // Seed mock data
                dbContext.Orders.Add(new Order
                {
                    OrderId = 1,
                    ActivityId = 100 // Original ActivityId
                });

                await dbContext.SaveChangesAsync();
            }

            bool result;

            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext); // Initialize _context for OrderDAO

                // Act
                result = OrderDAO.UpdateActivityOrder(1, 200); // Update the ActivityId to 200
            }

            // Assert
            Assert.True(result); // Assert that the method returned true

            using (var dbContext = new GreenGardenContext(options))
            {
                var updatedOrder = dbContext.Orders.FirstOrDefault(o => o.OrderId == 1);
                Assert.NotNull(updatedOrder); // Assert that the order exists
                Assert.Equal(200, updatedOrder.ActivityId); // Assert that the ActivityId was updated
            }
        }

        [Fact]
        public async Task UpdateActivityOrder_ShouldReturnFalse_WhenOrderDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new GreenGardenContext(options))
            {
                await dbContext.SaveChangesAsync(); // Ensure the database is empty
            }

            bool result;

            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext); // Initialize _context for OrderDAO

                // Act
                result = OrderDAO.UpdateActivityOrder(999, 200); // Try updating a non-existing order
            }

            // Assert
            Assert.False(result); // Assert that the method returned false
        }

        // Test for GetAllOrderOnline method
        [Fact]
        public async Task GetAllOrderOnline_ShouldReturnCorrectOrders_WhenActivityIdIsOne()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new GreenGardenContext(options))
            {
                // Seed mock data
                dbContext.Roles.Add(new Role { RoleId = 2, RoleName = "Customer" });
                dbContext.Roles.Add(new Role { RoleId = 3, RoleName = "Employee" });

                dbContext.Users.AddRange(
                    new User { UserId = 1, FirstName = "John", LastName = "Doe", RoleId = 3, Email = "john@example.com", Password = "hashed_password" },
                    new User { UserId = 2, FirstName = "John", LastName = "John", RoleId = 3, Email = "john2@example.com", Password = "hashed_password" },
                    new User { UserId = 3, FirstName = "Jane", LastName = "Smith", RoleId = 2, Email = "jane@example.com", Password = "hashed_password" }
                );

                dbContext.Activities.Add(new Activity { ActivityId = 1, ActivityName = "Online Order" });



                dbContext.Orders.AddRange(
                    new Order
                    {
                        OrderId = 1,
                        CustomerId = 1,
                        EmployeeId = 1,
                        ActivityId = 1,
                        PhoneCustomer = "123456789",
                        OrderDate = DateTime.UtcNow.AddDays(-1),
                        OrderUsageDate = DateTime.UtcNow.AddDays(5),
                        Deposit = 50.00m,
                        TotalAmount = 200.00m,
                        AmountPayable = 150.00m,
                        StatusOrder = true
                    },
                    new Order
                    {
                        OrderId = 2,
                        CustomerId = 1,
                        EmployeeId = 2,
                        ActivityId = 2, // Not an online order
                        PhoneCustomer = "987654321",
                        OrderDate = DateTime.UtcNow.AddDays(-2),
                        OrderUsageDate = DateTime.UtcNow.AddDays(3),
                        Deposit = 40.00m,
                        TotalAmount = 180.00m,
                        AmountPayable = 140.00m,
                        StatusOrder = false
                    }
                );

                await dbContext.SaveChangesAsync();
            }

            List<OrderDTO> result;

            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext); // Assuming InitializeContext is necessary

                // Act
                result = OrderDAO.getAllOrderOnline(); // Get all online orders
            }

            // Assert
            Assert.NotNull(result); // Ensure result is not null
            Assert.Single(result); // Only one order has ActivityId = 1
            var order = result.First();

            Assert.Equal(1, order.OrderId); // Assert the correct order is returned
            Assert.Equal(1, order.CustomerId);
            Assert.Equal(1, order.EmployeeId);
            Assert.Equal("123456789", order.PhoneCustomer); // Phone number
            Assert.Equal(50.00m, order.Deposit); // Deposit
            Assert.Equal(200.00m, order.TotalAmount); // Total amount
            Assert.Equal(150.00m, order.AmountPayable); // Amount payable
            Assert.True(order.StatusOrder); // Status of the order
            Assert.Equal(1, order.ActivityId); // ActivityId
            Assert.Equal("Online Order", order.ActivityName); // Activity name
        }

        [Fact]
        public async Task GetAllOrderOnline_ShouldReturnEmptyList_WhenNoOrdersExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var emptyContext = new GreenGardenContext(options);
            emptyContext.Database.EnsureCreated();

            OrderDAO.InitializeContext(emptyContext);

            // Act
            var result = OrderDAO.getAllOrderOnline();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty(); // Expect an empty list
        }

        [Fact]
        public async Task GetAllOrderOnline_ShouldThrowException_WhenDatabaseThrowsException()
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
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(OrderDAO.getAllOrderOnline()));
            exception.Message.Should().Be("Object reference not set to an instance of an object.");

        }

        // Test for CancelDeposit method
        [Fact]
        public async Task CancelDeposit_ShouldUpdateOrder_WhenOrderIdExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new GreenGardenContext(options))
            {
                // Seed mock data
                dbContext.Orders.Add(new Order
                {
                    OrderId = 1,
                    CustomerId = 1,
                    EmployeeId = 1,
                    OrderDate = DateTime.UtcNow.AddDays(-1),
                    OrderUsageDate = DateTime.UtcNow.AddDays(5),
                    Deposit = 50.00m,
                    TotalAmount = 200.00m,
                    AmountPayable = 150.00m,
                    StatusOrder = true
                });

                await dbContext.SaveChangesAsync();
            }

            bool result;
            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext); // Assuming InitializeContext is necessary

                // Act
                result = OrderDAO.CancelDeposit(1); // Cancel the deposit for OrderId = 1
            }

            // Assert
            Assert.True(result); // Ensure the method returns true

            using (var dbContext = new GreenGardenContext(options))
            {
                var updatedOrder = dbContext.Orders.FirstOrDefault(o => o.OrderId == 1);

                Assert.NotNull(updatedOrder); // Ensure the order still exists
                Assert.False(updatedOrder.StatusOrder); // Ensure the order status is updated to false
                Assert.Equal(0, updatedOrder.Deposit); // Ensure the deposit is set to 0
                Assert.Equal(200.00m, updatedOrder.AmountPayable); // Ensure the AmountPayable matches TotalAmount
            }
        }

        [Fact]
        public async Task CancelDeposit_ShouldReturnFalse_WhenOrderIdDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new GreenGardenContext(options))
            {
                // Seed mock data
                dbContext.Orders.Add(new Order
                {
                    OrderId = 1,
                    CustomerId = 1,
                    EmployeeId = 1,
                    OrderDate = DateTime.UtcNow.AddDays(-1),
                    OrderUsageDate = DateTime.UtcNow.AddDays(5),
                    Deposit = 50.00m,
                    TotalAmount = 200.00m,
                    AmountPayable = 150.00m,
                    StatusOrder = true
                });

                await dbContext.SaveChangesAsync();
            }
            bool result;

            using (var dbContext = new GreenGardenContext(options))
            {
                // Act
                OrderDAO.InitializeContext(dbContext);

                result = OrderDAO.CancelDeposit(999); // Try to cancel deposit for a non-existent OrderId
            }

            // Assert
            Assert.False(result); // Ensure the method returns false
        }

        [Fact]
        public async Task CancelDeposit_ShouldThrowException_WhenDatabaseThrowsException()
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
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(OrderDAO.CancelDeposit(1)));
            exception.Message.Should().Be("Object reference not set to an instance of an object.");

        }

        // Test for GetListOrderGearByUsageDate method
        [Fact]
        public async Task GetListOrderGearByUsageDate_ShouldReturnCorrectGearList_WhenOrdersExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var usageDate = new DateTime(2024, 11, 25); // Test usage date

            using (var dbContext = new GreenGardenContext(options))
            {
                // Seed mock data
                dbContext.Orders.AddRange(
                    new Order
                    {
                        OrderId = 1,
                        OrderUsageDate = usageDate,
                        ActivityId = 1 // Valid ActivityId
                    },
                    new Order
                    {
                        OrderId = 2,
                        OrderUsageDate = usageDate.AddDays(1), // Different date
                        ActivityId = 1 // Valid ActivityId
                    },
                    new Order
                    {
                        OrderId = 3,
                        OrderUsageDate = usageDate,
                        ActivityId = 1002 // Excluded ActivityId
                    }
                );

                dbContext.OrderCampingGearDetails.AddRange(
                    new OrderCampingGearDetail
                    {
                        OrderId = 1,
                        GearId = 101,
                        Quantity = 5
                    },
                    new OrderCampingGearDetail
                    {
                        OrderId = 2,
                        GearId = 102,
                        Quantity = 3
                    },
                    new OrderCampingGearDetail
                    {
                        OrderId = 3,
                        GearId = 103,
                        Quantity = 2
                    }
                );

                await dbContext.SaveChangesAsync();
            }

            List<OrderCampingGearByUsageDateDTO> result;
            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext); // Assuming InitializeContext is necessary

                // Act
                result = OrderDAO.GetListOrderGearByUsageDate(usageDate);
            }

            // Assert
            Assert.NotNull(result);
            Assert.Single(result); // Only one valid order on the specified date
            var gear = result.First();
            Assert.Equal(101, gear.GearId); // Verify GearId
            Assert.Equal(5, gear.Quantity); // Verify Quantity
        }

        [Fact]
        public async Task GetListOrderGearByUsageDate_ShouldReturnEmptyList_WhenNoMatchingOrdersExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var usageDate = new DateTime(2024, 11, 25); // Test usage date

            using (var dbContext = new GreenGardenContext(options))
            {
                // Seed mock data with orders on a different date
                dbContext.Orders.Add(new Order
                {
                    OrderId = 1,
                    OrderUsageDate = usageDate.AddDays(1), // Different date
                    ActivityId = 1 // Valid ActivityId
                });

                dbContext.OrderCampingGearDetails.Add(new OrderCampingGearDetail
                {
                    OrderId = 1,
                    GearId = 101,
                    Quantity = 5
                });

                await dbContext.SaveChangesAsync();
            }

            List<OrderCampingGearByUsageDateDTO> result;
            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext); // Assuming InitializeContext is necessary

                // Act
                result = OrderDAO.GetListOrderGearByUsageDate(usageDate);
            }

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result); // No orders match the usage date
        }

        [Fact]
        public async Task GetListOrderGearByUsageDate_ShouldThrowException_WhenDatabaseThrowsException()
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
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(OrderDAO.GetListOrderGearByUsageDate(new DateTime(2024, 11, 25))));
            exception.Message.Should().Be("Object reference not set to an instance of an object.");

        }

        // Test for CreateComboOrder method
        [Fact]
        public async Task CreateComboOrder_ShouldReturnTrue_WhenOrderIsValid()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var request = new CreateComboOrderRequest
            {
                Order = new OrderAddDTO
                {
                    EmployeeId = 1,
                    CustomerName = "Jane Doe",
                    OrderUsageDate = DateTime.UtcNow.AddDays(2),
                    Deposit = 50.00m,
                    TotalAmount = 200.00m,
                    PhoneCustomer = "123456789"
                },
                OrderCombo = new List<OrderComboAddDTO>
        {
            new OrderComboAddDTO { ComboId = 1, Quantity = 2 }
        },
                OrderCampingGear = new List<OrderCampingGearAddDTO>
        {
            new OrderCampingGearAddDTO { GearId = 1, Quantity = 1 }
        },
                OrderFood = new List<OrderFoodAddDTO>
        {
            new OrderFoodAddDTO { ItemId = 1, Quantity = 3, Description = "No onions" }
        },
                OrderFoodCombo = new List<OrderFoodComboAddDTO>
        {
            new OrderFoodComboAddDTO { ComboId = 2, Quantity = 1 }
        }
            };

            bool result;

            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext); // Assuming InitializeContext is necessary

                // Act
                result = OrderDAO.CreateComboOrder(request);
            }

            // Assert
            Assert.True(result); // Ensure the method returns true

            // Validate data in the database
            using (var dbContext = new GreenGardenContext(options))
            {
                var order = dbContext.Orders.FirstOrDefault();
                Assert.NotNull(order);
                Assert.Equal("Jane Doe", order.CustomerName);
                Assert.Equal(50.00m, order.Deposit);
                Assert.Equal(200.00m, order.TotalAmount);
                Assert.Equal(150.00m, order.AmountPayable);
                Assert.True(order.StatusOrder);

                var orderCombo = dbContext.OrderComboDetails.ToList();
                Assert.Single(orderCombo);
                Assert.Equal(1, orderCombo.First().ComboId);
                Assert.Equal(2, orderCombo.First().Quantity);

                var orderCampingGear = dbContext.OrderCampingGearDetails.ToList();
                Assert.Single(orderCampingGear);
                Assert.Equal(1, orderCampingGear.First().GearId);
                Assert.Equal(1, orderCampingGear.First().Quantity);

                var orderFood = dbContext.OrderFoodDetails.ToList();
                Assert.Single(orderFood);
                Assert.Equal(1, orderFood.First().ItemId);
                Assert.Equal(3, orderFood.First().Quantity);
                Assert.Equal("No onions", orderFood.First().Description);

                var orderFoodCombo = dbContext.OrderFoodComboDetails.ToList();
                Assert.Single(orderFoodCombo);
                Assert.Equal(2, orderFoodCombo.First().ComboId);
                Assert.Equal(1, orderFoodCombo.First().Quantity);
            }
        }

        [Fact]
        public async Task CreateComboOrder_ShouldReturnFalse_WhenOrderComboIsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var request = new CreateComboOrderRequest
            {
                Order = new OrderAddDTO
                {
                    EmployeeId = 1,
                    CustomerName = "Jane Doe",
                    OrderUsageDate = DateTime.UtcNow.AddDays(2),
                    Deposit = 50.00m,
                    TotalAmount = 200.00m,
                    PhoneCustomer = "123456789"
                },
                OrderCombo = null, // Invalid case
                OrderCampingGear = null,
                OrderFood = null,
                OrderFoodCombo = null
            };

            bool result;

            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext); // Assuming InitializeContext is necessary

                // Act
                result = OrderDAO.CreateComboOrder(request);
            }

            // Assert
            Assert.False(result); // Ensure the method returns false
        }

        [Fact]
        public async Task CreateComboOrder_ShouldWork_WhenOrderCampingGearIsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var request = new CreateComboOrderRequest
            {
                Order = new OrderAddDTO
                {
                    EmployeeId = 1,
                    CustomerName = "Jane Doe",
                    OrderUsageDate = DateTime.UtcNow.AddDays(2),
                    Deposit = 50.00m,
                    TotalAmount = 200.00m,
                    PhoneCustomer = "123456789"
                },
                OrderCombo = new List<OrderComboAddDTO>
        {
            new OrderComboAddDTO { ComboId = 1, Quantity = 2 }
        },
                OrderCampingGear = null, // Optional field is null
                OrderFood = new List<OrderFoodAddDTO>
        {
            new OrderFoodAddDTO { ItemId = 1, Quantity = 2, Description = "Extra spicy" }
        },
                OrderFoodCombo = new List<OrderFoodComboAddDTO>
        {
            new OrderFoodComboAddDTO { ComboId = 2, Quantity = 1 }
        }
            };

            bool result;

            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext); // Initialize context if necessary

                // Act
                result = OrderDAO.CreateComboOrder(request);
            }

            // Assert
            Assert.True(result); // Ensure the method returns true

            using (var dbContext = new GreenGardenContext(options))
            {
                var order = dbContext.Orders.FirstOrDefault();
                Assert.NotNull(order);

                var orderCampingGear = dbContext.OrderCampingGearDetails.ToList();
                Assert.Empty(orderCampingGear); // Ensure no camping gear was added
            }
        }

        [Fact]
        public async Task CreateComboOrder_ShouldWork_WhenOrderFoodIsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var request = new CreateComboOrderRequest
            {
                Order = new OrderAddDTO
                {
                    EmployeeId = 1,
                    CustomerName = "John Doe",
                    OrderUsageDate = DateTime.UtcNow.AddDays(1),
                    Deposit = 30.00m,
                    TotalAmount = 100.00m,
                    PhoneCustomer = "987654321"
                },
                OrderCombo = new List<OrderComboAddDTO>
        {
            new OrderComboAddDTO { ComboId = 1, Quantity = 1 }
        },
                OrderCampingGear = new List<OrderCampingGearAddDTO>
        {
            new OrderCampingGearAddDTO { GearId = 1, Quantity = 1 }
        },
                OrderFood = null, // Optional field is null
                OrderFoodCombo = new List<OrderFoodComboAddDTO>
        {
            new OrderFoodComboAddDTO { ComboId = 3, Quantity = 2 }
        }
            };

            bool result;

            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext);

                // Act
                result = OrderDAO.CreateComboOrder(request);
            }

            // Assert
            Assert.True(result); // Ensure the method returns true

            using (var dbContext = new GreenGardenContext(options))
            {
                var order = dbContext.Orders.FirstOrDefault();
                Assert.NotNull(order);

                var orderFood = dbContext.OrderFoodDetails.ToList();
                Assert.Empty(orderFood); // Ensure no food was added
            }
        }

        [Fact]
        public async Task CreateComboOrder_ShouldWork_WhenOrderFoodComboIsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var request = new CreateComboOrderRequest
            {
                Order = new OrderAddDTO
                {
                    EmployeeId = 2,
                    CustomerName = "Alice Smith",
                    OrderUsageDate = DateTime.UtcNow.AddDays(3),
                    Deposit = 75.00m,
                    TotalAmount = 300.00m,
                    PhoneCustomer = "567890123"
                },
                OrderCombo = new List<OrderComboAddDTO>
        {
            new OrderComboAddDTO { ComboId = 2, Quantity = 3 }
        },
                OrderCampingGear = new List<OrderCampingGearAddDTO>
        {
            new OrderCampingGearAddDTO { GearId = 2, Quantity = 2 }
        },
                OrderFood = new List<OrderFoodAddDTO>
        {
            new OrderFoodAddDTO { ItemId = 2, Quantity = 1, Description = "Gluten-free" }
        },
                OrderFoodCombo = null // Optional field is null
            };

            bool result;

            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext);

                // Act
                result = OrderDAO.CreateComboOrder(request);
            }

            // Assert
            Assert.True(result); // Ensure the method returns true

            using (var dbContext = new GreenGardenContext(options))
            {
                var order = dbContext.Orders.FirstOrDefault();
                Assert.NotNull(order);

                var orderFoodCombo = dbContext.OrderFoodComboDetails.ToList();
                Assert.Empty(orderFoodCombo); // Ensure no food combo was added
            }
        }

        //[Fact]
        //public async Task CreateComboOrder_ShouldWork_WhenOnlyComboIsProvided()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<GreenGardenContext>()
        //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //        .Options;

        //    var request = new CreateComboOrderRequest
        //    {
        //        Order = new OrderAddDTO
        //        {
        //            EmployeeId = 1,
        //            CustomerName = "John Doe",
        //            OrderUsageDate = DateTime.UtcNow.AddDays(1),
        //            Deposit = 100.00m,
        //            TotalAmount = 300.00m,
        //            PhoneCustomer = "123456789"
        //        },
        //        OrderCombo = new List<OrderComboAddDTO>
        //{
        //    new OrderComboAddDTO { ComboId = 1, Quantity = 2 }
        //},
        //        OrderCampingGear = null, // Optional field is null
        //        OrderFood = null,        // Optional field is null
        //        OrderFoodCombo = null    // Optional field is null
        //    };

        //    bool result;

        //    using (var dbContext = new GreenGardenContext(options))
        //    {
        //        OrderDAO.InitializeContext(dbContext);

        //        // Act
        //        result = OrderDAO.CreateComboOrder(request);
        //    }

        //    // Assert
        //    Assert.True(result); // Ensure the method returns true

        //    using (var dbContext = new GreenGardenContext(options))
        //    {
        //        // Verify that the order was created
        //        var order = dbContext.Orders.FirstOrDefault();
        //        Assert.NotNull(order);
        //        Assert.Equal(1, order.EmployeeId);
        //        Assert.Equal("John Doe", order.CustomerName);
        //        Assert.Equal(300.00m, order.TotalAmount);
        //        Assert.Equal(100.00m, order.Deposit);
        //        Assert.Equal(200.00m, order.AmountPayable);
        //        Assert.Equal(1, order.ActivityId); // Activity ID based on deposit condition

        //        // Verify that only combo details were added
        //        var orderCombos = dbContext.OrderComboDetails.ToList();
        //        Assert.Single(orderCombos); // Ensure only one combo detail was added
        //        var combo = orderCombos.First();
        //        Assert.Equal(1, combo.ComboId);
        //        Assert.Equal(2, combo.Quantity);

        //        // Verify no camping gear details were added
        //        var campingGear = dbContext.OrderCampingGearDetails.ToList();
        //        Assert.Empty(campingGear);

        //        // Verify no food details were added
        //        var foodDetails = dbContext.OrderFoodDetails.ToList();
        //        Assert.Empty(foodDetails);

        //        // Verify no food combo details were added
        //        var foodCombos = dbContext.OrderFoodComboDetails.ToList();
        //        Assert.Empty(foodCombos);
        //    }
        //}

        // Test for UpdateTicket method
        [Fact]
        public async Task UpdateTicket_ShouldUpdateTicketsSuccessfully_WhenValidTicketsAreProvided()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var tickets = new List<OrderTicketAddlDTO>
        {
            new OrderTicketAddlDTO { OrderId = 1, TicketId = 2, Quantity = 3 },
            new OrderTicketAddlDTO { OrderId = 1, TicketId = 3, Quantity = 1 }
        };

            using (var dbContext = new GreenGardenContext(options))
            {
                dbContext.Orders.Add(new Order { OrderId = 1 });
                dbContext.OrderTicketDetails.Add(new OrderTicketDetail { OrderId = 1, TicketId = 1, Quantity = 2 });
                await dbContext.SaveChangesAsync();
            }

            // Act
            bool result;
            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext); // Initialize context (if required)
                result = OrderDAO.UpdateTicket(tickets); // Call the method
            }

            // Assert
            Assert.True(result); // Ensure the method returns true

            using (var dbContext = new GreenGardenContext(options))
            {
                var updatedTickets = dbContext.OrderTicketDetails.Where(o => o.OrderId == 1).ToList();
                Assert.Equal(2, updatedTickets.Count); // Ensure there are 2 tickets now
                Assert.Contains(updatedTickets, t => t.TicketId == 2 && t.Quantity == 3);
                Assert.Contains(updatedTickets, t => t.TicketId == 3 && t.Quantity == 1);
            }
        }

        [Fact]
        public async Task UpdateTicket_ShouldReplaceExistingTickets_WhenValidTicketsAreProvided()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var tickets = new List<OrderTicketAddlDTO>
        {
            new OrderTicketAddlDTO { OrderId = 1, TicketId = 2, Quantity = 5 }
        };

            using (var dbContext = new GreenGardenContext(options))
            {
                dbContext.Orders.Add(new Order { OrderId = 1 });
                dbContext.OrderTicketDetails.Add(new OrderTicketDetail { OrderId = 1, TicketId = 1, Quantity = 2 });
                await dbContext.SaveChangesAsync();
            }

            // Act
            bool result;
            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext); // Initialize context (if required)
                result = OrderDAO.UpdateTicket(tickets); // Call the method
            }

            // Assert
            Assert.True(result); // Ensure the method returns true

            using (var dbContext = new GreenGardenContext(options))
            {
                var updatedTickets = dbContext.OrderTicketDetails.Where(o => o.OrderId == 1).ToList();
                Assert.Single(updatedTickets); // Ensure only one ticket exists
                Assert.Equal(2, updatedTickets[0].TicketId); // TicketId should be 2
                Assert.Equal(5, updatedTickets[0].Quantity); // Quantity should be 5
            }
        }

        //[Fact]
        //public async Task UpdateTicket_ShouldReturnTrue_WhenNoTicketsAreProvided()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<GreenGardenContext>()
        //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //        .Options;

        //    var tickets = new List<OrderTicketAddlDTO>(); // Empty ticket list

        //    using (var dbContext = new GreenGardenContext(options))
        //    {
        //        dbContext.Orders.Add(new Order { OrderId = 1 });
        //        await dbContext.SaveChangesAsync();
        //    }

        //    // Act
        //    bool result;
        //    using (var dbContext = new GreenGardenContext(options))
        //    {
        //        OrderDAO.InitializeContext(dbContext); // Initialize context (if required)
        //        result = OrderDAO.UpdateTicket(tickets); // Call the method
        //    }

        //    // Assert
        //    Assert.True(result); // Method should return true even when no tickets are provided
        //}

        //[Fact]
        //public async Task UpdateTicket_ShouldCallDeleteOrder_WhenTicketIdIsZero()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<GreenGardenContext>()
        //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //        .Options;

        //    var tickets = new List<OrderTicketAddlDTO>
        //{
        //    new OrderTicketAddlDTO { OrderId = 1, TicketId = 0, Quantity = 0 } // TicketId is 0
        //};

        //    using (var dbContext = new GreenGardenContext(options))
        //    {
        //        dbContext.Orders.Add(new Order { OrderId = 1 });
        //        dbContext.OrderTicketDetails.Add(new OrderTicketDetail { OrderId = 1, TicketId = 1, Quantity = 2 });
        //        await dbContext.SaveChangesAsync();
        //    }

        //    // Act
        //    bool result;
        //    using (var dbContext = new GreenGardenContext(options))
        //    {

        //        OrderDAO.InitializeContext(dbContext);
        //        result = OrderDAO.UpdateTicket(tickets); // Call the method

        //    }

        //    // Assert
        //    Assert.True(result); // Ensure the method returns true

        //    using (var dbContext = new GreenGardenContext(options))
        //    {
        //        var existingTickets = dbContext.OrderTicketDetails.Where(o => o.OrderId == 1).ToList();
        //        Assert.Empty(existingTickets); // There should be no tickets left
        //    }
        //}

        [Fact]
        public async Task UpdateTicket_ShouldReturnFalse_WhenTicketIdIsInvalid()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var tickets = new List<OrderTicketAddlDTO>
        {
            new OrderTicketAddlDTO { OrderId = 1, TicketId = 999, Quantity = 2 } // Invalid TicketId
        };

            using (var dbContext = new GreenGardenContext(options))
            {
                dbContext.Orders.Add(new Order { OrderId = 1 });
                dbContext.OrderTicketDetails.Add(new OrderTicketDetail { OrderId = 1, TicketId = 1, Quantity = 2 });
                await dbContext.SaveChangesAsync();
            }

            // Act & Assert
            using (var dbContext = new GreenGardenContext(options))
            {
                Assert.Throws<Exception>(() => OrderDAO.UpdateTicket(tickets)); // Expecting an exception due to invalid TicketId
            }
        }

        // Test for UpdateGear method
        [Fact]
        public async Task UpdateGear_ShouldReturnTrue_WhenValidGearIsProvided()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var gears = new List<OrderCampingGearAddDTO>
        {
            new OrderCampingGearAddDTO { OrderId = 1, GearId = 101, Quantity = 2 }
        };

            using (var dbContext = new GreenGardenContext(options))
            {
                dbContext.Orders.Add(new Order { OrderId = 1 });
                dbContext.OrderCampingGearDetails.Add(new OrderCampingGearDetail { OrderId = 1, GearId = 100, Quantity = 1 });
                await dbContext.SaveChangesAsync();
            }

            // Act
            bool result;
            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext);
                result = OrderDAO.UpdateGear(gears); // Call the method
            }

            // Assert
            Assert.True(result); // Ensure the method returns true

            using (var dbContext = new GreenGardenContext(options))
            {
                var existingGears = dbContext.OrderCampingGearDetails.Where(o => o.OrderId == 1).ToList();
                Assert.Single(existingGears); // Only one gear should be present after the update
                Assert.Equal(101, existingGears[0].GearId); // GearId should be 101
                Assert.Equal(2, existingGears[0].Quantity); // Quantity should be 2
            }
        }

        [Fact]
        public async Task UpdateGear_ShouldRemoveAllGears_WhenGearIdIsZero()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var gears = new List<OrderCampingGearAddDTO>
        {
            new OrderCampingGearAddDTO { OrderId = 1, GearId = 0, Quantity = 0 } // GearId is 0 to remove all gear
        };

            using (var dbContext = new GreenGardenContext(options))
            {
                dbContext.Orders.Add(new Order { OrderId = 1 });
                dbContext.OrderCampingGearDetails.Add(new OrderCampingGearDetail { OrderId = 1, GearId = 100, Quantity = 1 });
                await dbContext.SaveChangesAsync();
            }

            // Act
            bool result;
            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext);
                result = OrderDAO.UpdateGear(gears); // Call the method
            }

            // Assert
            Assert.True(result); // Ensure the method returns true

            using (var dbContext = new GreenGardenContext(options))
            {
                var existingGears = dbContext.OrderCampingGearDetails.Where(o => o.OrderId == 1).ToList();
                Assert.Empty(existingGears); // No gears should be left
            }
        }

        [Fact]
        public async Task UpdateGear_ShouldThrowException_WhenExceptionIsThrown()
        {
            var gears = new List<OrderCampingGearAddDTO>
        {
            new OrderCampingGearAddDTO { OrderId = 1, GearId = 101, Quantity = 2 }
        };
            OrderDAO.InitializeContext(null);
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.Run(() => OrderDAO.UpdateGear(gears)));
            exception.Message.Should().Be("Object reference not set to an instance of an object.");
        }

        // Test for UpdateFood method
        [Fact]
        public async Task UpdateFood_ShouldReturnTrue_WhenValidFoodIsProvided()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var foods = new List<OrderFoodAddDTO>
        {
            new OrderFoodAddDTO { OrderId = 1, ItemId = 101, Quantity = 2 }
        };

            using (var dbContext = new GreenGardenContext(options))
            {
                dbContext.Orders.Add(new Order { OrderId = 1 });
                dbContext.OrderFoodDetails.Add(new OrderFoodDetail { OrderId = 1, ItemId = 100, Quantity = 1 });
                await dbContext.SaveChangesAsync();
            }

            // Act
            bool result;
            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext);
                result = OrderDAO.UpdateFood(foods); // Call the method
            }

            // Assert
            Assert.True(result); // Ensure the method returns true

            using (var dbContext = new GreenGardenContext(options))
            {
                var existingFoods = dbContext.OrderFoodDetails.Where(o => o.OrderId == 1).ToList();
                Assert.Single(existingFoods); // Only one food item should be present after the update
                Assert.Equal(101, existingFoods[0].ItemId); // ItemId should be 101
                Assert.Equal(2, existingFoods[0].Quantity); // Quantity should be 2
            }
        }

        [Fact]
        public async Task UpdateFood_ShouldRemoveAllFoodDetails_WhenItemIdIsZero()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var foods = new List<OrderFoodAddDTO>
        {
            new OrderFoodAddDTO { OrderId = 1, ItemId = 0, Quantity = 0 } // ItemId is 0 to remove all food
        };

            using (var dbContext = new GreenGardenContext(options))
            {
                dbContext.Orders.Add(new Order { OrderId = 1 });
                dbContext.OrderFoodDetails.Add(new OrderFoodDetail { OrderId = 1, ItemId = 100, Quantity = 1 });
                await dbContext.SaveChangesAsync();
            }

            // Act
            bool result;
            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext);
                result = OrderDAO.UpdateFood(foods); // Call the method
            }

            // Assert
            Assert.True(result); // Ensure the method returns true

            using (var dbContext = new GreenGardenContext(options))
            {
                var existingFoods = dbContext.OrderFoodDetails.Where(o => o.OrderId == 1).ToList();
                Assert.Empty(existingFoods); // No food items should remain
            }
        }

        [Fact]
        public async Task UpdateFood_ShouldThrowException_WhenExceptionIsThrown()
        {
            var foods = new List<OrderFoodAddDTO>
        {
            new OrderFoodAddDTO { OrderId = 1, ItemId = 101, Quantity = 2 }
        };

            // Simulating an exception by passing null context
            OrderDAO.InitializeContext(null);

            var exception = await Assert.ThrowsAsync<Exception>(() => Task.Run(() => OrderDAO.UpdateFood(foods)));
            Assert.Equal("Object reference not set to an instance of an object.", exception.Message);
        }

        // Test for UpdateCombo method
        [Fact]
        public async Task UpdateCombo_ShouldUpdateCombosSuccessfully_WhenValidCombosAreProvided()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var combos = new List<OrderComboAddDTO>
        {
            new OrderComboAddDTO { OrderId = 1, ComboId = 2, Quantity = 3 },
            new OrderComboAddDTO { OrderId = 1, ComboId = 3, Quantity = 1 }
        };

            using (var dbContext = new GreenGardenContext(options))
            {
                dbContext.Orders.Add(new Order { OrderId = 1 });
                dbContext.OrderComboDetails.Add(new OrderComboDetail { OrderId = 1, ComboId = 1, Quantity = 2 });
                await dbContext.SaveChangesAsync();
            }

            // Act
            bool result;
            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext);
                result = OrderDAO.UpdateCombo(combos); // Call the method
            }

            // Assert
            Assert.True(result); // Ensure the method returns true

            using (var dbContext = new GreenGardenContext(options))
            {
                var updatedCombos = dbContext.OrderComboDetails.Where(o => o.OrderId == 1).ToList();
                Assert.Equal(2, updatedCombos.Count); // Ensure there are 2 combos now
                Assert.Contains(updatedCombos, c => c.ComboId == 2 && c.Quantity == 3);
                Assert.Contains(updatedCombos, c => c.ComboId == 3 && c.Quantity == 1);
            }
        }

        [Fact]
        public async Task UpdateCombo_ShouldReplaceExistingCombos_WhenValidCombosAreProvided()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var combos = new List<OrderComboAddDTO>
        {
            new OrderComboAddDTO { OrderId = 1, ComboId = 2, Quantity = 5 }
        };

            using (var dbContext = new GreenGardenContext(options))
            {
                dbContext.Orders.Add(new Order { OrderId = 1 });
                dbContext.OrderComboDetails.Add(new OrderComboDetail { OrderId = 1, ComboId = 1, Quantity = 2 });
                await dbContext.SaveChangesAsync();
            }

            // Act
            bool result;
            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext);
                result = OrderDAO.UpdateCombo(combos); // Call the method
            }

            // Assert
            Assert.True(result); // Ensure the method returns true

            using (var dbContext = new GreenGardenContext(options))
            {
                var updatedCombos = dbContext.OrderComboDetails.Where(o => o.OrderId == 1).ToList();
                Assert.Single(updatedCombos); // Ensure only one combo exists
                Assert.Equal(2, updatedCombos[0].ComboId); // ComboId should be 2
                Assert.Equal(5, updatedCombos[0].Quantity); // Quantity should be 5
            }
        }

        //[Fact]
        //public async Task UpdateCombo_ShouldCallDeleteOrder_WhenComboIdIsZero()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<GreenGardenContext>()
        //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //        .Options;

        //    var combos = new List<OrderComboAddDTO>
        //{
        //    new OrderComboAddDTO { OrderId = 1, ComboId = 0, Quantity = 0 } // ComboId is 0
        //};

        //    using (var dbContext = new GreenGardenContext(options))
        //    {
        //        dbContext.Orders.Add(new Order { OrderId = 1 });
        //        dbContext.OrderComboDetails.Add(new OrderComboDetail { OrderId = 1, ComboId = 1, Quantity = 2 });
        //        await dbContext.SaveChangesAsync();
        //    }

        //    // Act
        //    bool result;
        //    using (var dbContext = new GreenGardenContext(options))
        //    {
        //        OrderDAO.InitializeContext(dbContext);
        //        result = OrderDAO.UpdateCombo(combos); // Call the method
        //    }

        //    // Assert
        //    Assert.True(result); // Ensure the method returns true

        //    using (var dbContext = new GreenGardenContext(options))
        //    {
        //        var existingCombos = dbContext.OrderComboDetails.Where(o => o.OrderId == 1).ToList();
        //        Assert.Empty(existingCombos); // There should be no combos left
        //    }
        //}

        //[Fact]
        //public async Task UpdateCombo_ShouldReturnFalse_WhenComboIdIsInvalid()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<GreenGardenContext>()
        //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //        .Options;

        //    var combos = new List<OrderComboAddDTO>
        //{
        //    new OrderComboAddDTO { OrderId = 1, ComboId = 999, Quantity = 2 } // Invalid ComboId
        //};

        //    using (var dbContext = new GreenGardenContext(options))
        //    {
        //        dbContext.Orders.Add(new Order { OrderId = 1 });
        //        dbContext.OrderComboDetails.Add(new OrderComboDetail { OrderId = 1, ComboId = 1, Quantity = 2 });
        //        await dbContext.SaveChangesAsync();
        //    }

        //    // Act & Assert
        //    using (var dbContext = new GreenGardenContext(options))
        //    {
        //        Assert.Throws<Exception>(() => OrderDAO.UpdateCombo(combos)); // Expecting an exception due to invalid ComboId
        //    }
        //}

        [Fact]
        public async Task UpdateCombo_ShouldThrowException_WhenExceptionOccurs()
        {
            var combos = new List<OrderComboAddDTO>
        {
            new OrderComboAddDTO { OrderId = 1, ComboId = 101, Quantity = 2 }
        };

            // Simulating an exception by passing null context
            OrderDAO.InitializeContext(null);

            var exception = await Assert.ThrowsAsync<Exception>(() => Task.Run(() => OrderDAO.UpdateCombo(combos)));
            Assert.Equal("Object reference not set to an instance of an object.", exception.Message);
        }

        // Test for UpdateFoodCombo method
        [Fact]
        public async Task UpdateFoodCombo_ShouldUpdateFoodCombosSuccessfully_WhenValidFoodCombosAreProvided()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var foodCombos = new List<OrderFoodComboAddDTO>
        {
            new OrderFoodComboAddDTO { OrderId = 1, ComboId = 101, Quantity = 2 },
            new OrderFoodComboAddDTO { OrderId = 1, ComboId = 102, Quantity = 1 }
        };

            using (var dbContext = new GreenGardenContext(options))
            {
                dbContext.Orders.Add(new Order { OrderId = 1 });
                dbContext.OrderFoodComboDetails.Add(new OrderFoodComboDetail { OrderId = 1, ComboId = 100, Quantity = 3 });
                await dbContext.SaveChangesAsync();
            }

            // Act
            bool result;
            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext);
                result = OrderDAO.UpdateFoodCombo(foodCombos); // Call the method
            }

            // Assert
            Assert.True(result); // Ensure the method returns true

            using (var dbContext = new GreenGardenContext(options))
            {
                var updatedFoodCombos = dbContext.OrderFoodComboDetails.Where(o => o.OrderId == 1).ToList();
                Assert.Equal(2, updatedFoodCombos.Count); // Ensure two food combos are present
                Assert.Contains(updatedFoodCombos, c => c.ComboId == 101 && c.Quantity == 2);
                Assert.Contains(updatedFoodCombos, c => c.ComboId == 102 && c.Quantity == 1);
            }
        }

        [Fact]
        public async Task UpdateFoodCombo_ShouldRemoveAllFoodCombos_WhenComboIdIsZero()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var foodCombos = new List<OrderFoodComboAddDTO>
        {
            new OrderFoodComboAddDTO { OrderId = 1, ComboId = 0, Quantity = 0 } // ComboId is 0 to remove all food combos
        };

            using (var dbContext = new GreenGardenContext(options))
            {
                dbContext.Orders.Add(new Order { OrderId = 1 });
                dbContext.OrderFoodComboDetails.Add(new OrderFoodComboDetail { OrderId = 1, ComboId = 100, Quantity = 3 });
                await dbContext.SaveChangesAsync();
            }

            // Act
            bool result;
            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext);
                result = OrderDAO.UpdateFoodCombo(foodCombos); // Call the method
            }

            // Assert
            Assert.True(result); // Ensure the method returns true

            using (var dbContext = new GreenGardenContext(options))
            {
                var existingFoodCombos = dbContext.OrderFoodComboDetails.Where(o => o.OrderId == 1).ToList();
                Assert.Empty(existingFoodCombos); // Ensure no food combos remain
            }
        }

        [Fact]
        public async Task UpdateFoodCombo_ShouldThrowException_WhenExceptionIsThrown()
        {
            var foodCombos = new List<OrderFoodComboAddDTO>
        {
            new OrderFoodComboAddDTO { OrderId = 1, ComboId = 101, Quantity = 2 }
        };

            // Simulating an exception by passing null context
            OrderDAO.InitializeContext(null);

            var exception = await Assert.ThrowsAsync<Exception>(() => Task.Run(() => OrderDAO.UpdateFoodCombo(foodCombos)));
            Assert.Equal("Object reference not set to an instance of an object.", exception.Message);
        }

        // Test for UpdateOrder method
        [Fact]
        public async Task UpdateOrder_ShouldUpdateOrderSuccessfully_WhenValidOrderIsProvided()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new GreenGardenContext(options))
            {
                dbContext.Orders.Add(new Order
                {
                    OrderId = 1,
                    OrderUsageDate = DateTime.Parse("2024-01-01"),
                    TotalAmount = 100m,
                    AmountPayable = 80m,
                    Deposit = 20m,
                    OrderCheckoutDate = null
                });
                await dbContext.SaveChangesAsync();
            }

            var updatedOrder = new UpdateOrderDTO
            {
                OrderId = 1,
                OrderUsageDate = DateTime.Parse("2024-02-01"),
                TotalAmount = 200m,
                OrderCheckoutDate = DateTime.Parse("2024-02-02")
            };

            // Act
            bool result;
            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext);
                result = OrderDAO.UpdateOrder(updatedOrder);
            }

            // Assert
            Assert.True(result); // Ensure the method returns true

            using (var dbContext = new GreenGardenContext(options))
            {
                var order = dbContext.Orders.FirstOrDefault(o => o.OrderId == 1);
                Assert.NotNull(order);
                Assert.Equal(updatedOrder.OrderUsageDate, order.OrderUsageDate);
                Assert.Equal(updatedOrder.TotalAmount, order.TotalAmount);
                Assert.Equal(updatedOrder.TotalAmount - 20m, order.AmountPayable); // TotalAmount - Deposit
                Assert.Equal(updatedOrder.OrderCheckoutDate, order.OrderCheckoutDate);
            }
        }

        [Fact]
        public async Task UpdateOrder_ShouldReturnFalse_WhenOrderDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var nonExistentOrder = new UpdateOrderDTO
            {
                OrderId = 999,
                OrderUsageDate = DateTime.Parse("2024-02-01"),
                TotalAmount = 200m,
                OrderCheckoutDate = DateTime.Parse("2024-02-02")
            };

            // Act
            bool result;
            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext);
                result = OrderDAO.UpdateOrder(nonExistentOrder);
            }

            // Assert
            Assert.False(result); // Ensure the method returns false
        }

        //[Fact]
        //public async Task UpdateOrder_ShouldThrowException_WhenOrderCheckoutDateIsEarlierThanOrderUsageDate()   
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<GreenGardenContext>()
        //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //        .Options;

        //    using (var dbContext = new GreenGardenContext(options))
        //    {
        //        dbContext.Orders.Add(new Order
        //        {
        //            OrderId = 1,
        //            OrderUsageDate = DateTime.Parse("2024-02-01"),
        //            TotalAmount = 100m,
        //            AmountPayable = 80m,
        //            Deposit = 20m,
        //            OrderCheckoutDate = null
        //        });
        //        await dbContext.SaveChangesAsync();
        //    }

        //    var invalidOrder = new UpdateOrderDTO
        //    {
        //        OrderId = 1,
        //        OrderUsageDate = DateTime.Parse("2024-02-01"),
        //        OrderCheckoutDate = DateTime.Parse("2024-01-05"), // Invalid: Checkout date earlier than usage date
        //        TotalAmount = 150m
        //    };

        //    // Act & Assert
        //    using (var dbContext = new GreenGardenContext(options))
        //    {
        //        OrderDAO.InitializeContext(dbContext);
        //        var exception = Assert.Throws<Exception>(() => OrderDAO.UpdateOrder(invalidOrder));
        //        Assert.Equal("OrderCheckoutDate cannot be earlier than OrderUsageDate.", exception.Message);
        //    }
        //}

        [Fact]
        public async Task UpdateOrder_ShouldUpdateSuccessfully_WhenOrderCheckoutDateEqualsOrderUsageDate()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new GreenGardenContext(options))
            {
                dbContext.Orders.Add(new Order
                {
                    OrderId = 1,
                    OrderUsageDate = DateTime.Parse("2024-02-01"),
                    TotalAmount = 100m,
                    AmountPayable = 80m,
                    Deposit = 20m,
                    OrderCheckoutDate = null
                });
                await dbContext.SaveChangesAsync();
            }

            var validOrder = new UpdateOrderDTO
            {
                OrderId = 1,
                OrderUsageDate = DateTime.Parse("2024-02-01"),
                OrderCheckoutDate = DateTime.Parse("2024-02-01"), // Valid: Checkout date equals usage date
                TotalAmount = 150m
            };

            // Act
            bool result;
            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext);
                result = OrderDAO.UpdateOrder(validOrder);
            }

            // Assert
            Assert.True(result);

            using (var dbContext = new GreenGardenContext(options))
            {
                var order = dbContext.Orders.FirstOrDefault(o => o.OrderId == 1);
                Assert.NotNull(order);
                Assert.Equal(validOrder.OrderCheckoutDate, order.OrderCheckoutDate);
                Assert.Equal(validOrder.OrderUsageDate, order.OrderUsageDate);
            }
        }

        [Fact]
        public async Task UpdateOrder_ShouldThrowException_WhenDatabaseContextIsNull()
        {
            // Arrange
            var updatedOrder = new UpdateOrderDTO
            {
                OrderId = 1,
                OrderUsageDate = DateTime.Parse("2024-02-01"),
                TotalAmount = 200m,
                OrderCheckoutDate = DateTime.Parse("2024-02-02")
            };

            // Simulate a null context
            OrderDAO.InitializeContext(null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.Run(() => OrderDAO.UpdateOrder(updatedOrder)));
            Assert.Equal("Object reference not set to an instance of an object.", exception.Message);
        }

        //[Fact]
        //public async Task UpdateOrder_ShouldReturnFalse_NullFields()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<GreenGardenContext>()
        //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //        .Options;

        //    using (var dbContext = new GreenGardenContext(options))
        //    {
        //        dbContext.Orders.Add(new Order
        //        {
        //            OrderId = 1,
        //            OrderUsageDate = DateTime.Parse("2024-01-01"),
        //            TotalAmount = 100m,
        //            AmountPayable = 80m,
        //            Deposit = 20m,
        //            OrderCheckoutDate = null
        //        });
        //        await dbContext.SaveChangesAsync();
        //    }

        //    var updatedOrder = new UpdateOrderDTO
        //    {
        //        OrderId = 1,
        //        OrderUsageDate = null, // Optional field left null
        //        TotalAmount = 150m,
        //        OrderCheckoutDate = null // Optional field left null
        //    };

        //    // Act
        //    bool result;
        //    using (var dbContext = new GreenGardenContext(options))
        //    {
        //        OrderDAO.InitializeContext(dbContext);
        //        result = OrderDAO.UpdateOrder(updatedOrder);
        //    }

        //    // Assert
        //    Assert.False(result); // Ensure the method returns true

            
        //}

        //[Fact]
        //public async Task UpdateOrder_ShouldReturnFalse_NullOrderUsageDateFields()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<GreenGardenContext>()
        //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //        .Options;

        //    using (var dbContext = new GreenGardenContext(options))
        //    {
        //        dbContext.Orders.Add(new Order
        //        {
        //            OrderId = 1,
        //            OrderUsageDate = DateTime.Parse("2024-01-01"),
        //            TotalAmount = 100m,
        //            AmountPayable = 80m,
        //            Deposit = 20m,
        //            OrderCheckoutDate = null
        //        });
        //        await dbContext.SaveChangesAsync();
        //    }

        //    var updatedOrder = new UpdateOrderDTO
        //    {
        //        OrderId = 1,
        //        OrderUsageDate = null, // Optional field left null
        //        TotalAmount = 150m,
        //        OrderCheckoutDate = DateTime.Parse("2024-01-01") 
        //    };

        //    // Act
        //    bool result;
        //    using (var dbContext = new GreenGardenContext(options))
        //    {
        //        OrderDAO.InitializeContext(dbContext);
        //        result = OrderDAO.UpdateOrder(updatedOrder);
        //    }

        //    // Assert
        //    Assert.False(result); // Ensure the method returns true


        //}

        // Test for UpdateOrder method
        [Fact]
        public async Task CheckOut_ShouldReturnTrue_WhenValidOrderRequestIsProvided()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var orderRequest = new CheckOut
            {
                Order = new CustomerOrderAddDTO
                {
                    CustomerId = 1,
                    CustomerName = "John Doe",
                    OrderUsageDate = DateTime.Parse("2024-01-01"),
                    TotalAmount = 100m,
                    PhoneCustomer = "1234567890"
                },
                OrderTicket = new List<OrderTicketAddlDTO>
        {
            new OrderTicketAddlDTO { TicketId = 1, Quantity = 2 },
            new OrderTicketAddlDTO { TicketId = 2, Quantity = 3 }
        },
                OrderCampingGear = new List<OrderCampingGearAddDTO>
        {
            new OrderCampingGearAddDTO { GearId = 1, Quantity = 1 },
            new OrderCampingGearAddDTO { GearId = 2, Quantity = 2 }
        },
                OrderFood = new List<OrderFoodAddDTO>
        {
            new OrderFoodAddDTO { ItemId = 1, Quantity = 1, Description = "Burger" }
        },
                OrderFoodCombo = new List<OrderFoodComboAddDTO>
        {
            new OrderFoodComboAddDTO { ComboId = 1, Quantity = 2 }
        }
            };

            // Act
            bool result;
            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext); // Initialize context
                result = OrderDAO.CheckOut(orderRequest); // Call the method
            }

            // Assert
            Assert.True(result); // Ensure the method returns true

            using (var dbContext = new GreenGardenContext(options))
            {
                var order = dbContext.Orders.FirstOrDefault(o => o.CustomerName == "John Doe");
                Assert.NotNull(order); // Ensure the order is added
                Assert.Equal(100m, order.TotalAmount); // Verify total amount

                var tickets = dbContext.OrderTicketDetails.Where(t => t.OrderId == order.OrderId).ToList();
                Assert.Equal(2, tickets.Count); // Ensure tickets are added
                Assert.Contains(tickets, t => t.TicketId == 1 && t.Quantity == 2);

                var gears = dbContext.OrderCampingGearDetails.Where(g => g.OrderId == order.OrderId).ToList();
                Assert.Equal(2, gears.Count); // Ensure camping gears are added

                var foods = dbContext.OrderFoodDetails.Where(f => f.OrderId == order.OrderId).ToList();
                Assert.Single(foods); // Ensure foods are added

                var combos = dbContext.OrderFoodComboDetails.Where(c => c.OrderId == order.OrderId).ToList();
                Assert.Single(combos); // Ensure food combos are added
            }
        }

        [Fact]
        public async Task CheckOut_ShouldReturnFalse_WhenOrderTicketIsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var orderRequest = new CheckOut
            {
                Order = new CustomerOrderAddDTO
                {
                    CustomerId = 1,
                    CustomerName = "John Doe",
                    OrderUsageDate = DateTime.Parse("2024-01-01"),
                    TotalAmount = 100m,
                    PhoneCustomer = "1234567890"
                },
                OrderTicket = null, // No tickets
                OrderCampingGear = null,
                OrderFood = null,
                OrderFoodCombo = null
            };

            // Act
            bool result;
            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext);
                result = OrderDAO.CheckOut(orderRequest); // Call the method
            }

            // Assert
            Assert.False(result); // Ensure the method returns false
        }

        //[Fact]
        //public async Task CheckOut_ShouldHandlePartialOrderDetailsCorrectly()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<GreenGardenContext>()
        //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //        .Options;

        //    var orderRequest = new CheckOut
        //    {
        //        Order = new CustomerOrderAddDTO
        //        {
        //            CustomerId = 1,
        //            CustomerName = "John Doe",
        //            OrderUsageDate = DateTime.Parse("2024-01-01"),
        //            TotalAmount = 50m,
        //            PhoneCustomer = "1234567890"
        //        },
        //        OrderTicket = new List<OrderTicketAddlDTO>
        //{
        //    new OrderTicketAddlDTO { TicketId = 1, Quantity = 2 }
        //},
        //        OrderCampingGear = null, // No camping gears
        //        OrderFood = null, // No food
        //        OrderFoodCombo = null // No food combos
        //    };

        //    // Act
        //    bool result;
        //    using (var dbContext = new GreenGardenContext(options))
        //    {
        //        OrderDAO.InitializeContext(dbContext);
        //        result = OrderDAO.CheckOut(orderRequest); // Call the method
        //    }

        //    // Assert
        //    Assert.True(result); // Ensure the method returns true

        //    using (var dbContext = new GreenGardenContext(options))
        //    {
        //        var order = dbContext.Orders.FirstOrDefault(o => o.CustomerName == "John Doe");
        //        Assert.NotNull(order); // Ensure the order is added
        //        Assert.Equal(50m, order.TotalAmount); // Verify total amount

        //        var tickets = dbContext.OrderTicketDetails.Where(t => t.OrderId == order.OrderId).ToList();
        //        Assert.Single(tickets); // Ensure tickets are added
        //        Assert.Contains(tickets, t => t.TicketId == 1 && t.Quantity == 2);

        //        var gears = dbContext.OrderCampingGearDetails.Where(g => g.OrderId == order.OrderId).ToList();
        //        Assert.Empty(gears); // No camping gears

        //        var foods = dbContext.OrderFoodDetails.Where(f => f.OrderId == order.OrderId).ToList();
        //        Assert.Empty(foods); // No food

        //        var combos = dbContext.OrderFoodComboDetails.Where(c => c.OrderId == order.OrderId).ToList();
        //        Assert.Empty(combos); // No food combos
        //    }
        //}

        [Fact]
        public async Task CheckOut_ShouldThrowException_WhenDatabaseFails()
        {
            // Arrange
            var orderRequest = new CheckOut
            {
                Order = new CustomerOrderAddDTO
                {
                    CustomerId = 1,
                    CustomerName = "John Doe",
                    OrderUsageDate = DateTime.Parse("2024-01-01"),
                    TotalAmount = 100m,
                    PhoneCustomer = "1234567890"
                },
                OrderTicket = new List<OrderTicketAddlDTO>
        {
            new OrderTicketAddlDTO { TicketId = 1, Quantity = 2 }
        },
                OrderCampingGear = null,
                OrderFood = null,
                OrderFoodCombo = null
            };

            // Simulating an exception by passing null context
            OrderDAO.InitializeContext(null);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => OrderDAO.CheckOut(orderRequest));
            Assert.NotNull(exception);
            Assert.Contains("Object reference not set to an instance of an object", exception.Message);
        }

        // Test for CheckoutComboOrder method
        [Fact]
        public async Task CheckoutComboOrder_ShouldReturnTrue_WhenValidOrderRequestIsProvided()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var orderRequest = new CheckoutCombo
            {
                Order = new CustomerOrderAddDTO
                {
                    CustomerId = 1,
                    CustomerName = "John Doe",
                    OrderUsageDate = DateTime.Parse("2024-01-01"),
                    TotalAmount = 100m,
                    PhoneCustomer = "1234567890"
                },
                OrderCombo = new List<OrderComboAddDTO>
        {
            new OrderComboAddDTO { ComboId = 1, Quantity = 2 },
            new OrderComboAddDTO { ComboId = 2, Quantity = 3 }
        },
                OrderCampingGear = new List<OrderCampingGearAddDTO>
        {
            new OrderCampingGearAddDTO { GearId = 1, Quantity = 1 },
            new OrderCampingGearAddDTO { GearId = 2, Quantity = 2 }
        },
                OrderFood = new List<OrderFoodAddDTO>
        {
            new OrderFoodAddDTO { ItemId = 1, Quantity = 1, Description = "Burger" }
        },
                OrderFoodCombo = new List<OrderFoodComboAddDTO>
        {
            new OrderFoodComboAddDTO { ComboId = 1, Quantity = 2 }
        }
            };

            // Act
            bool result;
            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext);
                result = OrderDAO.CheckoutComboOrder(orderRequest);
            }

            // Assert
            Assert.True(result);

            using (var dbContext = new GreenGardenContext(options))
            {
                var order = dbContext.Orders.FirstOrDefault(o => o.CustomerName == "John Doe");
                Assert.NotNull(order);
                Assert.Equal(100m, order.TotalAmount);

                var combos = dbContext.OrderComboDetails.Where(c => c.OrderId == order.OrderId).ToList();
                Assert.Equal(2, combos.Count);
                Assert.Contains(combos, c => c.ComboId == 1 && c.Quantity == 2);

                var gears = dbContext.OrderCampingGearDetails.Where(g => g.OrderId == order.OrderId).ToList();
                Assert.Equal(2, gears.Count);

                var foods = dbContext.OrderFoodDetails.Where(f => f.OrderId == order.OrderId).ToList();
                Assert.Single(foods);

                var foodCombos = dbContext.OrderFoodComboDetails.Where(fc => fc.OrderId == order.OrderId).ToList();
                Assert.Single(foodCombos);
            }
        }

        [Fact]
        public async Task CheckoutComboOrder_ShouldReturnFalse_WhenOrderComboIsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var orderRequest = new CheckoutCombo
            {
                Order = new CustomerOrderAddDTO
                {
                    CustomerId = 1,
                    CustomerName = "John Doe",
                    OrderUsageDate = DateTime.Parse("2024-01-01"),
                    TotalAmount = 100m,
                    PhoneCustomer = "1234567890"
                },
                OrderCombo = null,
                OrderCampingGear = null,
                OrderFood = null,
                OrderFoodCombo = null
            };

            // Act
            bool result;
            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext);
                result = OrderDAO.CheckoutComboOrder(orderRequest);
            }

            // Assert
            Assert.False(result);
        }

        //[Fact]
        //public async Task CheckoutComboOrder_ShouldHandlePartialOrderDetailsCorrectly()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<GreenGardenContext>()
        //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //        .Options;

        //    var orderRequest = new CheckoutCombo
        //    {
        //        Order = new CustomerOrderAddDTO
        //        {
        //            CustomerId = 1,
        //            CustomerName = "John Doe",
        //            OrderUsageDate = DateTime.Parse("2024-01-01"),
        //            TotalAmount = 50m,
        //            PhoneCustomer = "1234567890"
        //        },
        //        OrderCombo = new List<OrderComboAddDTO>
        //{
        //    new OrderComboAddDTO { ComboId = 1, Quantity = 2 }
        //},
        //        OrderCampingGear = null,
        //        OrderFood = null,
        //        OrderFoodCombo = null
        //    };

        //    // Act
        //    bool result;
        //    using (var dbContext = new GreenGardenContext(options))
        //    {
        //        OrderDAO.InitializeContext(dbContext);
        //        result = OrderDAO.CheckoutComboOrder(orderRequest);
        //    }

        //    // Assert
        //    Assert.True(result);

        //    using (var dbContext = new GreenGardenContext(options))
        //    {
        //        var order = dbContext.Orders.FirstOrDefault(o => o.CustomerName == "John Doe");
        //        Assert.NotNull(order);
        //        Assert.Equal(50m, order.TotalAmount);

        //        var combos = dbContext.OrderComboDetails.Where(c => c.OrderId == order.OrderId).ToList();
        //        Assert.Single(combos);
        //        Assert.Contains(combos, c => c.ComboId == 1 && c.Quantity == 2);

        //        var gears = dbContext.OrderCampingGearDetails.Where(g => g.OrderId == order.OrderId).ToList();
        //        Assert.Empty(gears);

        //        var foods = dbContext.OrderFoodDetails.Where(f => f.OrderId == order.OrderId).ToList();
        //        Assert.Empty(foods);

        //        var foodCombos = dbContext.OrderFoodComboDetails.Where(fc => fc.OrderId == order.OrderId).ToList();
        //        Assert.Empty(foodCombos);
        //    }
        //}

        [Fact]
        public async Task CheckoutComboOrder_ShouldThrowException_WhenDatabaseFails()
        {
            // Arrange
            var orderRequest = new CheckoutCombo
            {
                Order = new CustomerOrderAddDTO
                {
                    CustomerId = 1,
                    CustomerName = "John Doe",
                    OrderUsageDate = DateTime.Parse("2024-01-01"),
                    TotalAmount = 100m,
                    PhoneCustomer = "1234567890"
                },
                OrderCombo = new List<OrderComboAddDTO>
        {
            new OrderComboAddDTO { ComboId = 1, Quantity = 2 }
        },
                OrderCampingGear = null,
                OrderFood = null,
                OrderFoodCombo = null
            };

            // Simulating an exception by passing null context
            OrderDAO.InitializeContext(null);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => OrderDAO.CheckoutComboOrder(orderRequest));
            Assert.NotNull(exception);
            Assert.Contains("Object reference not set to an instance of an object", exception.Message);
        }

        // Test for CheckoutComboOrder method
        [Fact]
        public void UpdateActivity_ShouldReturnTrue_WhenOrderExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            int orderId = 1;

            using (var dbContext = new GreenGardenContext(options))
            {
                dbContext.Orders.Add(new Order
                {
                    OrderId = orderId,
                    ActivityId = 1001 // Original ActivityId
                });
                dbContext.SaveChanges();
            }

            // Act
            bool result;
            using (var dbContext = new GreenGardenContext(options))
            {
                OrderDAO.InitializeContext(dbContext);
                result = OrderDAO.UpdateActivity(orderId);
            }

            // Assert
            Assert.True(result);

            using (var dbContext = new GreenGardenContext(options))
            {
                var order = dbContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
                Assert.NotNull(order);
                Assert.Equal(1002, order.ActivityId); // Ensure ActivityId is updated
            }
        }

        [Fact]
        public void UpdateActivity_ShouldReturnFalse_WhenOrderDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);
            OrderDAO.InitializeContext(dbContext); // Initialize context for the DAO

            int nonExistentOrderId = 999;

            // Act
            bool result = OrderDAO.UpdateActivity(nonExistentOrderId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void UpdateActivity_ShouldThrowException_WhenDatabaseFails()
        {
            // Arrange
            int orderId = 1;

            // Simulating a database failure by passing null context
            OrderDAO.InitializeContext(null);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => OrderDAO.UpdateActivity(orderId));
            Assert.NotNull(exception);
            Assert.Contains("Lỗi khi cập nhật ActivityId", exception.Message);
        }

    }

}


