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

            TicketDAO.InitializeContext(dbContext); // Initialize the TicketDAO context with the in-memory db

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => Task.Run(() => TicketDAO.GetAllTickets()));
            exception.Message.Should().Be("Database query failed"); // Check that the exception message is correct
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

            TicketDAO.InitializeContext(dbContext); // Initialize the TicketDAO context with the in-memory db

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => Task.Run(() => TicketDAO.GetAllCustomerTickets()));
            exception.Message.Should().Be("Database query failed"); // Check that the exception message is correct
        }
    }
}
