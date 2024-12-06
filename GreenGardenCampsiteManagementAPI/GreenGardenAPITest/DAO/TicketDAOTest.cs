using BusinessObject.DTOs;
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
    public class TicketDAOTest
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
            if (!await databaseContext.Tickets.AnyAsync()) // Only add data if the table is empty
            {
                databaseContext.Tickets.AddRange(
                    new Ticket
                    {
                        TicketId = 1,
                        TicketName = "VIP Ticket",
                        Price = 500,
                        TicketCategoryId = 1,
                        TicketCategory = new TicketCategory { TicketCategoryName = "Category A" },
                        CreatedAt = DateTime.Now,
                        Status = true
                    },
                    new Ticket
                    {
                        TicketId = 2,
                        TicketName = "Regular Ticket",
                        Price = 200,
                        TicketCategoryId = 2,
                        TicketCategory = new TicketCategory { TicketCategoryName = "Category B" },
                        CreatedAt = DateTime.Now,
                        Status = false
                    },
                    new Ticket
                    {
                        TicketId = 3,
                        TicketName = "Standard Ticket",
                        Price = 150,
                        TicketCategoryId = 3,
                        TicketCategory = new TicketCategory { TicketCategoryName = "Category C" },
                        CreatedAt = DateTime.Now,
                        Status = true
                    }
                );
                await databaseContext.SaveChangesAsync();
            }

            return databaseContext;
        }

        // test for method GetAllTickets
        [Fact]
        public async Task GetAllTickets_ShouldReturnListOfTicketDTO()
        {
            // Arrange
            var dbContext = await GetDbContext(); // Get in-memory db context
            TicketDAO.InitializeContext(dbContext); // Initialize the TicketDAO context with the db context

            // Act
            var result = TicketDAO.GetAllTickets(); // Fetch the list of tickets

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3); // Ensure that we have 3 items in the list

            var firstTicket = result.First(); // Get the first ticket to assert values
            firstTicket.TicketId.Should().Be(1);
            firstTicket.TicketName.Should().Be("VIP Ticket");
            firstTicket.Price.Should().Be(500);
            firstTicket.TicketCategoryName.Should().Be("Category A");
        }

        [Fact]
        public async Task GetAllTickets_ShouldReturnEmptyList_WhenNoTicketsExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB for this test
                .Options;

            var emptyContext = new GreenGardenContext(options);
            emptyContext.Database.EnsureCreated(); // Ensure the empty database is created

            TicketDAO.InitializeContext(emptyContext); // Initialize the TicketDAO context with the empty db

            // Act
            var result = TicketDAO.GetAllTickets(); // Fetch the list of tickets from an empty database

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty(); // Expecting an empty list because there are no tickets in the DB
        }

        [Fact]
        public async Task GetAllTickets_ShouldThrowException_WhenDatabaseThrowsException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB for this test
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated(); // Ensure the database is created

            TicketDAO.InitializeContext(null); // Initialize the TicketDAO context with the in-memory db

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.Run(() => TicketDAO.GetAllTickets()));
            exception.Message.Should().Be("Object reference not set to an instance of an object.");
        }

        // test for method GetAllCustomerTickets
        [Fact]
        public async Task GetAllCustomerTickets_ShouldReturnOnlyActiveTickets()
        {
            // Arrange
            var dbContext = await GetDbContext();  // Get in-memory DB context
            TicketDAO.InitializeContext(dbContext);  // Initialize the TicketDAO with the in-memory DB context

            // Act
            var result = TicketDAO.GetAllCustomerTickets();  // Get tickets where Status = true

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);  // Only 2 tickets should have Status = true

            var firstTicket = result.First();
            firstTicket.TicketId.Should().Be(1);
            firstTicket.TicketName.Should().Be("VIP Ticket");
            firstTicket.Price.Should().Be(500);
            firstTicket.TicketCategoryName.Should().Be("Category A");

            var secondTicket = result.Last();
            secondTicket.TicketId.Should().Be(3);
            secondTicket.TicketName.Should().Be("Standard Ticket");
            secondTicket.Price.Should().Be(150);
            secondTicket.TicketCategoryName.Should().Be("Category C");
        }

        [Fact]
        public async Task GetAllCustomerTickets_ShouldReturnEmptyList_WhenNoActiveTicketsExist()
        // Test case for GetAllCustomerTickets when no tickets have Status = true
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())  // Unique DB for this test
                .Options;

            var emptyContext = new GreenGardenContext(options);
            emptyContext.Database.EnsureCreated();

            TicketDAO.InitializeContext(emptyContext);  // Initialize the TicketDAO with the in-memory DB context

            // Act
            var result = TicketDAO.GetAllCustomerTickets();  // Get tickets where Status = true

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();  // No tickets should have Status = true, hence empty list
        }

        [Fact]
        public async Task GetAllCustomerTickets_ShouldThrowException_WhenDatabaseThrowsException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB for this test
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated(); // Ensure the database is created

            TicketDAO.InitializeContext(null); // Initialize the TicketDAO context with the in-memory db

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.Run(() => TicketDAO.GetAllCustomerTickets()));
            exception.Message.Should().Be("Object reference not set to an instance of an object.");
        }

        // test for method GetAllCustomerTickets
        [Fact]
        public async Task GetTicketDetail_ShouldReturnTicket_WhenTicketExists()
        {
            // Arrange
            var dbContext = await GetDbContext(); // Get in-memory db context with mock data
            TicketDAO.InitializeContext(dbContext); // Initialize the TicketDAO context with the db context

            // Act
            var result = TicketDAO.GetTicketDetail(1); // Fetch the ticket details

            // Assert
            result.Should().NotBeNull(); // The result should not be null
            result.TicketId.Should().Be(1); // Ensure the ticket ID matches
            result.TicketName.Should().Be("VIP Ticket"); // Ensure the ticket name is correct
            result.Price.Should().Be(500); // Ensure the price is correct
            result.TicketCategoryName.Should().Be("Category A"); // Ensure the category name is correct
        }

        [Fact]
        public async Task GetTicketDetail_ShouldReturnNull_WhenTicketDoesNotExist()
        {
            // Arrange
            var dbContext = await GetDbContext(); // Get in-memory db context with mock data
            TicketDAO.InitializeContext(dbContext); // Initialize the TicketDAO context with the db context

            int invalidTicketId = 999; // A ticket ID that doesn't exist in the mock data

            // Act
            var result = TicketDAO.GetTicketDetail(invalidTicketId); // Try to fetch the non-existent ticket details

            // Assert
            result.Should().BeNull(); // The result should be null
        }

        [Fact]
        public async Task GetTicketDetail_ShouldThrowException_WhenDatabaseQueryFails()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB for this test
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated(); // Ensure the database is created

            TicketDAO.InitializeContext(null); // Initialize the TicketDAO context with the in-memory db

            // Simulate a database query failure by passing an invalid ticket ID (or an invalid query setup).
            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.Run(() => TicketDAO.GetTicketDetail(1)));
            exception.Message.Should().Be("Object reference not set to an instance of an object.");
        }

        // Test for method GetAllTicketCategories
        private async Task<GreenGardenContext> GetDbContext2() // Create a database in memory with mock data.
        {
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // unique name for each test
                .Options;

            var databaseContext = new GreenGardenContext(options);
            databaseContext.Database.EnsureCreated();

            // Clear the change tracker to ensure no duplicates
            databaseContext.ChangeTracker.Clear();
            if (!databaseContext.TicketCategories.Any())
            {
                databaseContext.TicketCategories.AddRange(
                    new TicketCategory { TicketCategoryId = 1, TicketCategoryName = "Category A", Description = "First category", CreatedAt = DateTime.Now },
                    new TicketCategory { TicketCategoryId = 2, TicketCategoryName = "Category B", Description = "Second category", CreatedAt = DateTime.Now }
                );
                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }
        [Fact]
        public async Task GetAllTicketCategories_ShouldReturnListOfTicketCategory_WhenDataExists()
        {
            // Arrange
            var dbContext = await GetDbContext2(); // Get in-memory db context with mock data
            TicketDAO.InitializeContext(dbContext); // Initialize the TicketDAO context with the db context

            // Act
            var result = TicketDAO.GetAllTicketCategories(); // Fetch the list of ticket categories

            // Assert
            result.Should().NotBeNull(); // The result should not be null
            result.Should().HaveCount(2); // Ensure we have 2 items in the list (or the number of mock data items)

            var firstCategory = result.First(); // Get the first category to assert values
            firstCategory.TicketCategoryId.Should().Be(1);
            firstCategory.TicketCategoryName.Should().Be("Category A");
            firstCategory.Description.Should().Be("First category");
            firstCategory.CreatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1)); // Ensure the date is recent (within a reasonable time span)
        }

        [Fact]
        public async Task GetAllTicketCategories_ShouldReturnEmptyList_WhenNoDataExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB for this test
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated(); // Get in-memory db context with no data
            TicketDAO.InitializeContext(dbContext); // Initialize the TicketDAO context with the db context

            // Act
            var result = TicketDAO.GetAllTicketCategories(); // Fetch the list of ticket categories (expecting an empty list)

            // Assert
            result.Should().NotBeNull(); // The result should not be null
            result.Should().BeEmpty(); // The result should be an empty list
        }

        [Fact]
        public async Task GetAllTicketCategories_ShouldThrowException_WhenDatabaseQueryFails()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB for this test
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated(); // Ensure the database is created

            TicketDAO.InitializeContext(null); // Initialize the TicketDAO context with the in-memory db

            // Simulate a database query failure by calling a non-existent method or causing a deliberate failure
            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.Run(() => TicketDAO.GetAllTicketCategories()));
            exception.Message.Should().Be("Object reference not set to an instance of an object.");
        }

        // Test for method ChangeTicketStatus
        [Fact]
        public async Task ChangeTicketStatus_ShouldToggleStatus_WhenValidTicketIdIsProvided()
        {
            // Arrange
            var dbContext = await GetDbContext(); // Get in-memory db context with mock data
            TicketDAO.InitializeContext(dbContext); // Initialize the TicketDAO context with the db context

            // Act - Change status of the first ticket (VIP Ticket)
            TicketDAO.ChangeTicketStatus(1);

            // Assert - Check if the status is toggled
            var updatedTicket = dbContext.Tickets.First(t => t.TicketId == 1);
            updatedTicket.Status.Should().BeFalse(); // The status should now be toggled to false

            // Act - Change status again to check if it's toggled back
            TicketDAO.ChangeTicketStatus(1);

            // Assert - Check if the status is toggled back
            updatedTicket = dbContext.Tickets.First(t => t.TicketId == 1);
            updatedTicket.Status.Should().BeTrue(); // The status should now be toggled back to true
        }

        [Fact]
        public async Task ChangeTicketStatus_ShouldThrowException_WhenTicketIdIsInvalid()
        {
            // Arrange
            var dbContext = await GetDbContext(); // Get in-memory db context with mock data
            TicketDAO.InitializeContext(dbContext); // Initialize the TicketDAO context with the db context

            // Act & Assert - Try to change status of a non-existing ticket (ticketId 999)
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.Run(() => TicketDAO.ChangeTicketStatus(999)));

            exception.Message.Should().Be("Food and Drink with ID 999 does not exist."); // Ensure the correct exception message is thrown
        }

        [Fact]
        public async Task ChangeTicketStatus_ShouldThrowException_WhenDatabaseSaveFails()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB for this test
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated(); // Ensure the database is created

            TicketDAO.InitializeContext(dbContext); // Initialize the TicketDAO context with the in-memory db

            // Act & Assert - Simulate a database error during SaveChanges
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.Run(() => TicketDAO.ChangeTicketStatus(1)));
            exception.Message.Should().Be("Food and Drink with ID 1 does not exist.");
        }

        // Test for method GetTicketsByCategoryIdAndSort
        // TC 1
        [Fact]
        public async Task GetTicketsWithNoFilter_ShouldReturnAllTickets()
        {
            // Arrange
            var dbContext = await GetDbContext(); // Get in-memory db context with mock data
            TicketDAO.InitializeContext(dbContext); // Initialize TicketDAO context

            // Act
            var result = TicketDAO.GetTicketsByCategoryIdAndSort(null, null);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2); // Ensure all 2 active tickets are returned
        }

        // TC 2
        [Fact]
        public async Task GetTicketsByCategoryId_ShouldReturnTicketsInCategory()
        {
            // Arrange
            var dbContext = await GetDbContext();
            TicketDAO.InitializeContext(dbContext);

            // Act
            var result = TicketDAO.GetTicketsByCategoryIdAndSort(1, null);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1); // Only 1 ticket in category ID = 1
            result.First().TicketCategoryId.Should().Be(1);
        }

        // TC 3
        [Fact]
        public async Task GetTicketsByCategoryIdAndSortByPriceDescending_ShouldReturnSortedTickets()
        {
            // Arrange
            var dbContext = await GetDbContext();
            TicketDAO.InitializeContext(dbContext);

            // Act
            var result = TicketDAO.GetTicketsByCategoryIdAndSort(1, 2);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result.First().TicketCategoryId.Should().Be(1);
            result.Should().BeInDescendingOrder(ticket => ticket.Price);
        }

        // TC 4
        [Fact]
        public async Task GetTicketsByCategoryIdAndSortByPriceAscending_ShouldReturnSortedTickets()
        {
            // Arrange
            var dbContext = await GetDbContext();
            TicketDAO.InitializeContext(dbContext);

            // Act
            var result = TicketDAO.GetTicketsByCategoryIdAndSort(1, 1);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result.First().TicketCategoryId.Should().Be(1);
            result.Should().BeInAscendingOrder(ticket => ticket.Price);
        }

        // TC 5
        [Fact]
        public async Task GetTicketsWithInvalidCategoryId_ShouldReturnEmptyList()
        {
            // Arrange
            var dbContext = await GetDbContext();
            TicketDAO.InitializeContext(dbContext);

            // Act
            var result = TicketDAO.GetTicketsByCategoryIdAndSort(99, null);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        // TC 6
        [Fact]
        public async Task GetTicketsSortedByPriceAscending_ShouldReturnSortedTickets()
        {
            // Arrange
            var dbContext = await GetDbContext();
            TicketDAO.InitializeContext(dbContext);

            // Act
            var result = TicketDAO.GetTicketsByCategoryIdAndSort(null, 1);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeInAscendingOrder(ticket => ticket.Price);
            Assert.Equal("Standard Ticket", result.FirstOrDefault()?.TicketName);
        }

        // TC 7
        [Fact]
        public async Task GetTicketsSortedByPriceDescending_ShouldReturnSortedTickets()
        {
            // Arrange
            var dbContext = await GetDbContext();
            TicketDAO.InitializeContext(dbContext);

            // Act
            var result = TicketDAO.GetTicketsByCategoryIdAndSort(null, 2);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeInDescendingOrder(ticket => ticket.Price);
        }
        
        // TC 8
        [Fact]
        public async Task GetTicketsWithInvalidSortCriteria_ShouldReturnUnsortedTickets()
        {
            // Arrange
            var dbContext = await GetDbContext();
            TicketDAO.InitializeContext(dbContext);

            // Act
            var result = TicketDAO.GetTicketsByCategoryIdAndSort(null, 3);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("VIP Ticket", result.FirstOrDefault()?.TicketName); // sắp xếp mặc định
            
        }

        // TC 9
        [Fact]
        public async Task GetTicketsWithInvalidCategoryAndSortCriteria_ShouldReturnEmptyList()
        {
            // Arrange
            var dbContext = await GetDbContext();
            TicketDAO.InitializeContext(dbContext);

            // Act
            var result = TicketDAO.GetTicketsByCategoryIdAndSort(99, 3);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        // TC 10
        [Fact]
        public async Task GetTicketsByCategoryIdAndSort_ShouldReturnEmptyList_WhenNoDataExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB for this test
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated(); // Get in-memory db context with no data
            TicketDAO.InitializeContext(dbContext); // Initialize the TicketDAO context with the db context

            // Act
            var result = TicketDAO.GetTicketsByCategoryIdAndSort(1, 1); // Fetch the list of ticket categories (expecting an empty list)

            // Assert
            result.Should().NotBeNull(); // The result should not be null
            result.Should().BeEmpty(); // The result should be an empty list
        }

        // TC 11
        [Fact]
        public async Task GetTicketsByCategoryIdAndSort_ShouldThrowException_WhenDatabaseQueryFails()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB for this test
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated(); // Ensure the database is created

            TicketDAO.InitializeContext(null); // Initialize the TicketDAO context with the in-memory db

            // Simulate a database query failure by calling a non-existent method or causing a deliberate failure
            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.Run(() => TicketDAO.GetTicketsByCategoryIdAndSort(1, 1)));
            exception.Message.Should().Be("Object reference not set to an instance of an object.");
        }

        // Test for method AddTicket
        [Fact]
        public async Task AddTicket_ShouldAddTicketSuccessfully_WhenDataIsValid()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);
            TicketDAO.InitializeContext(dbContext);

            var validTicket = new AddTicket
            {
                TicketId = 1,
                TicketName = "VIP Ticket",
                Price = 50.00m,
                TicketCategoryId = 2,
                ImgUrl = "http://example.com/ticket.png"
            };

            // Act
            TicketDAO.AddTicket(validTicket);

            // Assert
            var addedTicket = dbContext.Tickets.FirstOrDefault(t => t.TicketId == validTicket.TicketId);
            Assert.NotNull(addedTicket);
            Assert.Equal(validTicket.TicketName, addedTicket.TicketName);
            Assert.Equal(validTicket.Price, addedTicket.Price);
            Assert.Equal(validTicket.TicketCategoryId, addedTicket.TicketCategoryId);
            Assert.Equal(validTicket.ImgUrl, addedTicket.ImgUrl);
            Assert.True(addedTicket.Status);
            Assert.NotNull(addedTicket.CreatedAt); // Ensure CreatedAt is populated
        }

        //[Fact]
        //public void AddTicket_ShouldThrowException_WhenTicketNameIsNull()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<GreenGardenContext>()
        //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //        .Options;

        //    using var dbContext = new GreenGardenContext(options);
        //    TicketDAO.InitializeContext(dbContext);

        //    var invalidTicket = new AddTicket
        //    {
        //        TicketId = 2,
        //        TicketName = null!, // Invalid
        //        Price = 25.00m,
        //        TicketCategoryId = 1
        //    };

        //    // Act & Assert
        //    var exception = Assert.Throws<Exception>(() => TicketDAO.AddTicket(invalidTicket));
        //    Assert.Equal("The TicketName field is required.", exception.Message);
        //}

        //[Fact]
        //public void AddTicket_ShouldThrowException_WhenPriceIsZeroOrNegative()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<GreenGardenContext>()
        //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //        .Options;

        //    using var dbContext = new GreenGardenContext(options);
        //    TicketDAO.InitializeContext(dbContext);

        //    var invalidTicket = new AddTicket
        //    {
        //        TicketId = 3,
        //        TicketName = "Economy Ticket",
        //        Price = -10.00m, // Invalid
        //        TicketCategoryId = 1
        //    };

        //    // Act & Assert
        //    var exception = Assert.Throws<Exception>(() => TicketDAO.AddTicket(invalidTicket));
        //    Assert.Equal("Price must be a positive value.", exception.Message);
        //}

        [Fact]
        public void AddTicket_ShouldThrowException_WhenContextIsNull()
        {
            // Arrange
            TicketDAO.InitializeContext(null); // Nullify the context

            var validTicket = new AddTicket
            {
                TicketId = 4,
                TicketName = "Standard Ticket",
                Price = 30.00m,
                TicketCategoryId = 1
            };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => TicketDAO.AddTicket(validTicket));
            Assert.Equal("Object reference not set to an instance of an object.", exception.Message);
        }

        // Test for method UpdateTicket
        [Fact]
        public async Task UpdateTicket_ShouldUpdateTicketSuccessfully_WhenTicketExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);
            TicketDAO.InitializeContext(dbContext);

            // Add a ticket first for update
            var existingTicket = new Ticket
            {
                TicketId = 1,
                TicketName = "VIP Ticket",
                Price = 100.00m,
                TicketCategoryId = 1,
                ImgUrl = "http://example.com/vip-ticket.png",
                Status = true,
                CreatedAt = DateTime.Now
            };
            dbContext.Tickets.Add(existingTicket);
            dbContext.SaveChanges();

            var updatedTicketDto = new UpdateTicket
            {
                TicketId = 1,
                TicketName = "VIP Ticket Updated",
                Price = 120.00m,
                ImgUrl = "http://example.com/vip-ticket-updated.png",
                TicketCategoryId = 2
            };

            // Act
            TicketDAO.UpdateTicket(updatedTicketDto);

            // Assert
            var updatedTicket = dbContext.Tickets.FirstOrDefault(t => t.TicketId == 1);
            Assert.NotNull(updatedTicket);
            Assert.Equal("VIP Ticket Updated", updatedTicket.TicketName);
            Assert.Equal(120.00m, updatedTicket.Price);
            Assert.Equal("http://example.com/vip-ticket-updated.png", updatedTicket.ImgUrl);
            Assert.Equal(2, updatedTicket.TicketCategoryId);
        }

        [Fact]
        public void UpdateTicket_ShouldThrowException_WhenTicketDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);
            TicketDAO.InitializeContext(dbContext);

            var updateTicketDto = new UpdateTicket
            {
                TicketId = 999, // Non-existing ticket ID
                TicketName = "Non-Existing Ticket",
                Price = 50.00m,
                TicketCategoryId = 1
            };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => TicketDAO.UpdateTicket(updateTicketDto));
            Assert.Equal("Ticket with ID 999 does not exist.", exception.Message);
        }

        //[Fact]
        //public void UpdateTicket_ShouldThrowException_WhenPriceIsZeroOrNegative()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<GreenGardenContext>()
        //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //        .Options;

        //    using var dbContext = new GreenGardenContext(options);
        //    TicketDAO.InitializeContext(dbContext);

        //    var existingTicket = new Ticket
        //    {
        //        TicketId = 1,
        //        TicketName = "VIP Ticket",
        //        Price = 100.00m,
        //        TicketCategoryId = 1,
        //        ImgUrl = "http://example.com/vip-ticket.png",
        //        Status = true,
        //        CreatedAt = DateTime.Now
        //    };
        //    dbContext.Tickets.Add(existingTicket);
        //    dbContext.SaveChanges();

        //    var updateTicketDto = new UpdateTicket
        //    {
        //        TicketId = 1,
        //        TicketName = "VIP Ticket Updated",
        //        Price = -10.00m, // Invalid price
        //        ImgUrl = "http://example.com/vip-ticket-updated.png",
        //        TicketCategoryId = 2
        //    };

        //    // Act & Assert
        //    var exception = Assert.Throws<Exception>(() => TicketDAO.UpdateTicket(updateTicketDto));
        //    Assert.Equal("Price must be a positive value.", exception.Message);
        //}

        //[Fact]
        //public void UpdateTicket_ShouldThrowException_WhenTicketNameIsNull()
        //{
        //    // Arrange
        //    var options = new DbContextOptionsBuilder<GreenGardenContext>()
        //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //        .Options;

        //    using var dbContext = new GreenGardenContext(options);
        //    TicketDAO.InitializeContext(dbContext);

        //    var existingTicket = new Ticket
        //    {
        //        TicketId = 1,
        //        TicketName = "VIP Ticket",
        //        Price = 100.00m,
        //        TicketCategoryId = 1,
        //        ImgUrl = "http://example.com/vip-ticket.png",
        //        Status = true,
        //        CreatedAt = DateTime.Now
        //    };
        //    dbContext.Tickets.Add(existingTicket);
        //    dbContext.SaveChanges();

        //    var updateTicketDto = new UpdateTicket
        //    {
        //        TicketId = 1,
        //        TicketName = null!, // Invalid null name
        //        Price = 120.00m,
        //        ImgUrl = "http://example.com/vip-ticket-updated.png",
        //        TicketCategoryId = 2
        //    };

        //    // Act & Assert
        //    var exception = Assert.Throws<Exception>(() => TicketDAO.UpdateTicket(updateTicketDto));
        //    Assert.Equal("TicketName is required.", exception.Message);  // Assuming your validation enforces a required TicketName
        //}

        [Fact]
        public void UpdateTicket_ShouldThrowException_WhenContextIsNull()
        {
            // Arrange
            TicketDAO.InitializeContext(null); // Nullify the context

            var updatedTicketDto = new UpdateTicket
            {
                TicketId = 1,
                TicketName = "VIP Ticket Updated",
                Price = 120.00m,
                ImgUrl = "http://example.com/vip-ticket-updated.png",
                TicketCategoryId = 2
            };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => TicketDAO.UpdateTicket(updatedTicketDto));
            Assert.Equal("Object reference not set to an instance of an object.", exception.Message);
        }

    }
}
