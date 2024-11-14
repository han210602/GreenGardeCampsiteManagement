using BusinessObject.Models;
using DataAccess.DAO;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Activity = BusinessObject.Models.Activity;

namespace GreenGardenAPITest.DAO
{
    public class ActivityDAOTest
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
            if (!await databaseContext.Activities.AnyAsync()) // Only add data if the table is empty
            {
                databaseContext.Activities.AddRange(
                    new Activity
                    {
                        ActivityId = 1,
                        ActivityName = "Activity 1",
                    },
                    new Activity
                     {
                         ActivityId = 2,
                         ActivityName = "Activity 2",
                     },
                    new Activity
                      {
                         ActivityId = 3,
                         ActivityName = "Activity 3",
                      }

                    );
               
                await databaseContext.SaveChangesAsync();
            }

            return databaseContext;
        }

        // test for method getAllActivity
        [Fact]
        public async Task GetAllActivity_ShouldReturnListOfActivity()
        {
            // Arrange
            var dbContext = await GetDbContext(); // Get in-memory db context
            ActivityDAO.InitializeContext(dbContext); // Initialize the ActivityDAO context with the db context

            // Act
            var result = ActivityDAO.getAllActivity(); // Fetch the list of tickets

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3); // Ensure that we have 3 items in the list

            var firstTicket = result.First(); // Get the first ticket to assert values
            firstTicket.ActivityId.Should().Be(1);
            firstTicket.ActivityName.Should().Be("Activity 1");
            
        }

        [Fact]
        public async Task GetAllActivity_ShouldReturnEmptyList_WhenNoActivityExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB for this test
                .Options;

            var emptyContext = new GreenGardenContext(options);
            emptyContext.Database.EnsureCreated(); // Ensure the empty database is created

            ActivityDAO.InitializeContext(emptyContext); // Initialize the TicketDAO context with the empty db

            // Act
            var result = ActivityDAO.getAllActivity(); // Fetch the list of tickets from an empty database

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty(); // Expecting an empty list because there are no tickets in the DB
        }

       
        [Fact]
        public async Task GetAllActivity_ShouldThrowException_WhenRepositoryThrowsException()
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
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(ActivityDAO.getAllActivity()));
            
        }
    }
}
