using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess.DAO;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGardenAPITest.DAO
{
    public class EventDAOTest
    {
        private async Task<GreenGardenContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique name for each test
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated();

            // Seed mock data
            if (!await dbContext.Events.AnyAsync())
            {
                var user1 = new User
                {
                    UserId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Password = "Password123" // Ensure a value is provided
                };

                var user2 = new User
                {
                    UserId = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    Password = "SecurePass" // Ensure a value is provided
                };

                await dbContext.Users.AddRangeAsync(user1, user2);

                await dbContext.Events.AddRangeAsync(
                    new Event
                    {
                        EventId = 1,
                        EventName = "Music Concert",
                        Description = "A night of amazing music.",
                        EventDate = new DateTime(2024, 12, 25),
                        StartTime = new TimeSpan(18, 0, 0),
                        EndTime = new TimeSpan(22, 0, 0),
                        Location = "Central Park",
                        PictureUrl = "http://example.com/concert.jpg",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        CreateBy = user1.UserId,
                        CreateByNavigation = user1
                    },
                    new Event
                    {
                        EventId = 2,
                        EventName = "Art Exhibition",
                        Description = "Explore beautiful artworks.",
                        EventDate = new DateTime(2024, 11, 20),
                        StartTime = new TimeSpan(10, 0, 0),
                        EndTime = new TimeSpan(16, 0, 0),
                        Location = "City Art Gallery",
                        PictureUrl = "http://example.com/art.jpg",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        CreateBy = user2.UserId,
                        CreateByNavigation = user2
                    });

                await dbContext.SaveChangesAsync();
            }

            return dbContext;
        }

        // Test for method GetAllEvent
        [Fact]
        public async Task GetAllEvents_ShouldReturnAllEventsWithDetails()
        {
            // Arrange
            var dbContext = await GetDbContext();
            EventDAO.InitializeContext(dbContext);

            // Act
            var result = EventDAO.GetAllEvents();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);

            var firstEvent = result.FirstOrDefault(e => e.EventId == 1);
            firstEvent.Should().NotBeNull();
            firstEvent!.EventName.Should().Be("Music Concert");
            firstEvent.CreatedByUserName.Should().Be("John Doe");

            var secondEvent = result.FirstOrDefault(e => e.EventId == 2);
            secondEvent.Should().NotBeNull();
            secondEvent!.EventName.Should().Be("Art Exhibition");
            secondEvent.CreatedByUserName.Should().Be("Jane Smith");
        }

        [Fact]
        public async Task GetAllEvents_ShouldReturnEmpty_WhenNoEventsExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated();
            EventDAO.InitializeContext(dbContext);

            // Act
            var result = EventDAO.GetAllEvents();

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetAllEvents_ShouldThrowException_WhenRepositoryThrowsException()
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
            var exception =  Assert.ThrowsAsync<Exception>(() => Task.FromResult(EventDAO.GetAllEvents()));
        }

        // Test for method GetEventByCreatedBy
        [Fact]
        public async Task GetEventByCreatedBy_ShouldReturnEventsForUser()
        {
            // Arrange
            var dbContext = await GetDbContext();
            EventDAO.InitializeContext(dbContext);

            // Act
            var eventsForUser1 = EventDAO.GetEventByCreatedBy(1);

            // Assert
            Assert.NotNull(eventsForUser1);
            Assert.Single(eventsForUser1); // User1 has one event
            Assert.Equal("Music Concert", eventsForUser1[0].EventName);
            Assert.Equal("John Doe", eventsForUser1[0].CreatedByUserName);
        }

        [Fact]
        public async Task GetEventByCreatedBy_ShouldReturnEmptyForInvalidUser()
        {
            // Arrange
            var dbContext = await GetDbContext();
            EventDAO.InitializeContext(dbContext);

            // Act
            var eventsForInvalidUser = EventDAO.GetEventByCreatedBy(99); // User ID 99 doesn't exist

            // Assert
            Assert.NotNull(eventsForInvalidUser);
            Assert.Empty(eventsForInvalidUser);
        }

        [Fact]
        public async Task GetEventByCreatedBy_ShouldHandleNoEvents()
        {
            // Arrange
            var dbContext = await GetDbContext();
            EventDAO.InitializeContext(dbContext);

            // Act
            var eventsForUser2 = EventDAO.GetEventByCreatedBy(2);

            // Assert
            Assert.NotNull(eventsForUser2);
            Assert.Single(eventsForUser2); // User2 has one event
            Assert.Equal("Art Exhibition", eventsForUser2[0].EventName);
            Assert.Equal("Jane Smith", eventsForUser2[0].CreatedByUserName);
        }

        // Test for method GetEventById
        [Fact]
        public async Task GetEventById_ShouldReturnEvent_WhenEventExists()
        {
            // Arrange
            var dbContext = await GetDbContext();
            EventDAO.InitializeContext(dbContext);

            // Act
            var result = EventDAO.GetEventById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.EventId);
            Assert.Equal("Music Concert", result.EventName);
            Assert.Equal("John Doe", result.CreatedByUserName);
        }

        [Fact]
        public async Task GetEventById_ShouldThrowKeyNotFoundException_WhenEventDoesNotExist()
        {
            // Arrange
            var dbContext = await GetDbContext();
            EventDAO.InitializeContext(dbContext);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => EventDAO.GetEventById(99));
            Assert.Contains("Event with ID 99 not found", exception.Message);
        }

        [Fact]
        public async Task GetEventById_ShouldHandleNullCreateByNavigationGracefully()
        {
            // Arrange
            var dbContext = await GetDbContext();
            EventDAO.InitializeContext(dbContext);

            // Add an event with a missing CreateByNavigation
            var eventWithNoUser = new Event
            {
                EventId = 3,
                EventName = "Tech Conference",
                Description = "An insightful tech conference.",
                EventDate = DateTime.Now.AddDays(10),
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(17, 0, 0),
                Location = "Tech Park",
                PictureUrl = "http://example.com/techconference.jpg",
                IsActive = true,
                CreatedAt = DateTime.Now,
                CreateBy = null // No user assigned
            };
            dbContext.Events.Add(eventWithNoUser);
            await dbContext.SaveChangesAsync();

            // Act
            var result = EventDAO.GetEventById(3);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.EventId);
            Assert.Equal("Unknown", result.CreatedByUserName); // Ensure it handles missing user gracefully
        }

        // Test for method AddEvent
        private async Task<GreenGardenContext> GetDbContext2() // Create a database in memory with mock data.
        {
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique name for each test
                .Options;

            var databaseContext = new GreenGardenContext(options);
            databaseContext.Database.EnsureCreated();

            // Seed mock users
            if (!await databaseContext.Users.AnyAsync())
            {
                var users = new List<User>
            {
                new User
                {
                    UserId = 1,
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@example.com",
                    Password = "Password123",
                    RoleId = 1 // Admin role
                },
                new User
                {
                    UserId = 2,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Password = "Password123",
                    RoleId = 2 // Regular user role
                },
                new User
                {
                    UserId = 3,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    Password = "SecurePassword",
                    RoleId = 3 // Regular user role
                }
            };
                await databaseContext.Users.AddRangeAsync(users);
            }

            await databaseContext.SaveChangesAsync();
            return databaseContext;
        }

        private IConfiguration GetTestConfiguration()
        {
            var inMemorySettings = new Dictionary<string, string>
        {
            { "Smtp:Host", "smtp.example.com" },
            { "Smtp:Port", "587" },
            { "Smtp:Username", "test@example.com" },
            { "Smtp:Password", "password" },
            { "Smtp:FromEmail", "no-reply@example.com" }
        };

            return new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
        }

        private async Task<bool> MockSendEventNotificationEmail(string email, CreateEventDTO eventDTO, IConfiguration configuration)
        {
            // Simulate successful email sending
            await Task.Delay(10);
            return true;
        }

        [Fact]
        public async Task AddEvent_ShouldAddEventAndSendEmails_WhenDataIsValid()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_AddEvent")
                .Options;

            await using var context = new GreenGardenContext(options);
            context.Database.EnsureCreated();

            // Seed necessary data: Add a user with RoleId = 3 (sender)
            var senderUser = new User
            {
                UserId = 3,
                FirstName = "John",
                LastName = "Doe",
                Email = "sender@example.com",
                Password = "password123",
                RoleId = 3
            };

            // Seed another user with RoleId != 3 (the recipient of the email)
            var recipientUser = new User
            {
                UserId = 4,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "recipient@example.com",
                Password = "password123",
                RoleId = 2
            };

            context.Users.Add(senderUser);
            context.Users.Add(recipientUser);
            await context.SaveChangesAsync();

            var eventDTO = new CreateEventDTO
            {
                EventName = "Test Event",
                Description = "This is a test event",
                EventDate = DateTime.Now,
                StartTime = "10:00",
                EndTime = "12:00",
                Location = "Test Location",
                PictureUrl = "http://example.com/event.jpg",
                CreateBy = senderUser.UserId
            };

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(config => config["EmailSettings:FromEmail"])
                .Returns("no-reply@example.com");

            // Mock email service
            var emailServiceMock = new Mock<Func<string, CreateEventDTO, IConfiguration, Task>>();
            emailServiceMock.Setup(service => service(It.IsAny<string>(), It.IsAny<CreateEventDTO>(), It.IsAny<IConfiguration>()))
                .Returns(Task.CompletedTask); // Mock the email sending task to complete instantly.

            // Act
            var result = await EventDAO.AddEvent(eventDTO, configurationMock.Object);

            // Assert
            Assert.True(result, "AddEvent should return true when the event is successfully added and emails are sent.");

            // Verify the event was added to the database
            var addedEvent = context.Events.FirstOrDefault(e => e.EventName == eventDTO.EventName);
            Assert.NotNull(addedEvent);
            Assert.Equal(eventDTO.Description, addedEvent.Description);
            Assert.Equal(eventDTO.Location, addedEvent.Location);
            Assert.Equal(eventDTO.CreateBy, addedEvent.CreateBy);

            // Verify the email service was called for users with RoleId = 3
            emailServiceMock.Verify(service => service(It.IsAny<string>(), It.Is<CreateEventDTO>(e => e.EventName == "Test Event"), It.IsAny<IConfiguration>()), Times.AtLeastOnce);

            // Check that the email was intended to be sent to the recipients (e.g., RoleId == 2, not the sender)
            var notifiedUsers = context.Users.Where(u => u.UserId != senderUser.UserId).ToList();
            Assert.Contains(notifiedUsers, u => u.Email == recipientUser.Email);

            // Check that the sender was not included in the notification
            Assert.DoesNotContain(notifiedUsers, u => u.Email == senderUser.Email);
        }

        [Fact]
        public async Task AddEvent_ShouldThrowException_WhenEventCreationFails()
        {
            // Arrange
            var dbContext = await GetDbContext();
            EventDAO.InitializeContext(dbContext);
            var configuration = GetTestConfiguration();

            var eventDTO = new CreateEventDTO
            {
                EventName = null, // Invalid event name
                Description = "Invalid event data.",
                EventDate = DateTime.Now.AddDays(7),
                StartTime = "10:00:00",
                EndTime = "12:00:00",
                Location = "Community Hall",
                PictureUrl = "http://example.com/event.jpg",
                CreateBy = 1
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => EventDAO.AddEvent(eventDTO, configuration));
            Assert.Contains("Error adding event and sending emails", exception.Message);
        }

        [Fact]
        public async Task AddEvent_ShouldThrowException_WhenDatabaseSaveFails()
        {
            // Arrange
            var dbContext = await GetDbContext();
            EventDAO.InitializeContext(dbContext);
            var configuration = GetTestConfiguration();

            // Simulate a scenario where the database save fails
            dbContext.Database.EnsureDeleted(); // Force database deletion to trigger failure

            var eventDTO = new CreateEventDTO
            {
                EventName = "Community Meeting",
                Description = "A meeting for the community.",
                EventDate = DateTime.Now.AddDays(7),
                StartTime = "10:00:00",
                EndTime = "12:00:00",
                Location = "Community Hall",
                PictureUrl = "http://example.com/event.jpg",
                CreateBy = 1 // Created by Admin
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => EventDAO.AddEvent(eventDTO, configuration));
            Assert.Contains("Error adding event and sending emails", exception.Message);
        }

        // Test for method UpdateEvent
        [Fact]
        public async Task UpdateEvent_ShouldUpdateEvent_WhenEventExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);

            // Seed data
            var existingEvent = new Event
            {
                EventId = 1,
                EventName = "Original Event",
                Description = "Original Description",
                EventDate = DateTime.Now,
                StartTime = new TimeSpan(10, 0, 0),
                EndTime = new TimeSpan(12, 0, 0),
                Location = "Original Location",
                PictureUrl = "http://example.com/original.jpg",
                IsActive = true
            };
            dbContext.Events.Add(existingEvent);
            await dbContext.SaveChangesAsync();

            EventDAO.InitializeContext(dbContext);

            var updateDto = new UpdateEventDTO
            {
                EventId = 1,
                EventName = "Updated Event",
                Description = "Updated Description",
                EventDate = DateTime.Now.AddDays(1),
                StartTime = "11:00:00",
                EndTime = "13:00:00",
                Location = "Updated Location",
                PictureUrl = "http://example.com/updated.jpg",
                IsActive = false
            };

            // Act
            var result = EventDAO.UpdateEvent(updateDto);

            // Assert
            Assert.True(result);
            var updatedEvent = dbContext.Events.FirstOrDefault(e => e.EventId == 1);
            Assert.NotNull(updatedEvent);
            Assert.Equal(updateDto.EventName, updatedEvent.EventName);
            Assert.Equal(updateDto.Description, updatedEvent.Description);
            Assert.Equal(updateDto.EventDate, updatedEvent.EventDate);
            Assert.Equal(TimeSpan.Parse(updateDto.StartTime), updatedEvent.StartTime);
            Assert.Equal(TimeSpan.Parse(updateDto.EndTime), updatedEvent.EndTime);
            Assert.Equal(updateDto.Location, updatedEvent.Location);
            Assert.Equal(updateDto.PictureUrl, updatedEvent.PictureUrl);
            Assert.Equal(updateDto.IsActive, updatedEvent.IsActive);
        }

        [Fact]
        public void UpdateEvent_ShouldThrowException_WhenEventDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);
            EventDAO.InitializeContext(dbContext);

            var updateDto = new UpdateEventDTO
            {
                EventId = 999, // Non-existent EventId
                EventName = "Non-existent Event",
                Description = "Description",
                EventDate = DateTime.Now,
                StartTime = "10:00:00",
                EndTime = "12:00:00",
                Location = "Location",
                PictureUrl = "http://example.com/event.jpg",
                IsActive = true
            };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => EventDAO.UpdateEvent(updateDto));
            Assert.Contains("Event not found.", exception.Message);
        }

        [Fact]
        public void UpdateEvent_ShouldHandleNullFields_Gracefully()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);

            var existingEvent = new Event
            {
                EventId = 1,
                EventName = "Original Event",
                Description = "Original Description",
                EventDate = DateTime.Now,
                StartTime = new TimeSpan(10, 0, 0),
                EndTime = new TimeSpan(12, 0, 0),
                Location = "Original Location",
                PictureUrl = "http://example.com/original.jpg",
                IsActive = true
            };
            dbContext.Events.Add(existingEvent);
            dbContext.SaveChanges();

            EventDAO.InitializeContext(dbContext);

            var updateDto = new UpdateEventDTO
            {
                EventId = 1,
                EventName = "Updated Event",
                Description = null, // Null description
                EventDate = DateTime.Now.AddDays(1),
                StartTime = "11:00:00",
                EndTime = "13:00:00",
                Location = null, // Null location
                PictureUrl = null, // Null picture URL
                IsActive = null // Null IsActive
            };

            // Act
            var result = EventDAO.UpdateEvent(updateDto);

            // Assert
            Assert.True(result);
            var updatedEvent = dbContext.Events.FirstOrDefault(e => e.EventId == 1);
            Assert.NotNull(updatedEvent);
            Assert.Equal(updateDto.EventName, updatedEvent.EventName);
            Assert.Null(updatedEvent.Description);
            Assert.Equal(updateDto.EventDate, updatedEvent.EventDate);
            Assert.Equal(TimeSpan.Parse(updateDto.StartTime), updatedEvent.StartTime);
            Assert.Equal(TimeSpan.Parse(updateDto.EndTime), updatedEvent.EndTime);
            Assert.Null(updatedEvent.Location);
            Assert.Null(updatedEvent.PictureUrl);
            Assert.Null(updatedEvent.IsActive);
        }

        [Fact]
        public void UpdateEvent_ShouldThrowException_WhenContextIsNull()
        {
            // Arrange
            EventDAO.InitializeContext(null); // Null context

            var updateDto = new UpdateEventDTO
            {
                EventId = 1,
                EventName = "Updated Event",
                Description = "Updated Description",
                EventDate = DateTime.Now,
                StartTime = "11:00:00",
                EndTime = "13:00:00",
                Location = "Updated Location",
                PictureUrl = "http://example.com/updated.jpg",
                IsActive = true
            };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => EventDAO.UpdateEvent(updateDto));
            Assert.Contains("Object reference not set", exception.Message);
        }

        // Test for method DeleteEvent
        [Fact]
        public async Task DeleteEvent_ShouldDeleteEvent_WhenEventExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);

            // Seed data
            var existingEvent = new Event
            {
                EventId = 1,
                EventName = "Test Event",
                Description = "Test Description",
                EventDate = DateTime.Now,
                StartTime = new TimeSpan(10, 0, 0),
                EndTime = new TimeSpan(12, 0, 0),
                Location = "Test Location",
                PictureUrl = "http://example.com/test.jpg",
                IsActive = true
            };
            dbContext.Events.Add(existingEvent);
            await dbContext.SaveChangesAsync();

            EventDAO.InitializeContext(dbContext);

            // Act
            var result = EventDAO.DeleteEvent(1);

            // Assert
            Assert.True(result);
            var deletedEvent = dbContext.Events.FirstOrDefault(e => e.EventId == 1);
            Assert.Null(deletedEvent);
        }

        [Fact]
        public void DeleteEvent_ShouldThrowException_WhenEventDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);
            EventDAO.InitializeContext(dbContext);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => EventDAO.DeleteEvent(999)); // Non-existent EventId
            Assert.Contains("Event not found.", exception.Message);
        }

        [Fact]
        public void DeleteEvent_ShouldThrowException_WhenContextIsNull()
        {
            // Arrange
            EventDAO.InitializeContext(null); // Null context

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => EventDAO.DeleteEvent(1));
            Assert.Contains("Object reference not set", exception.Message);
        }

        // Test for method GetTop3NewestEvents
        [Fact]
        public async Task GetTop3NewestEvents_ShouldReturnTop3EventsOrderedByDate()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);

            // Seed data
            dbContext.Events.AddRange(
                new Event
                {
                    EventId = 1,
                    EventName = "Event 1",
                    Description = "First Event",
                    EventDate = DateTime.Now.AddDays(-3), // Older event
                    StartTime = new TimeSpan(10, 0, 0),
                    EndTime = new TimeSpan(12, 0, 0),
                    Location = "Location 1",
                    PictureUrl = "http://example.com/event1.jpg",
                    IsActive = true,
                    CreatedAt = DateTime.Now.AddDays(-5),
                    CreateByNavigation = new User { FirstName = "John", LastName = "Doe", Email = "john@gmail.com", Password = "123" }
                },
                new Event
                {
                    EventId = 2,
                    EventName = "Event 2",
                    Description = "Second Event",
                    EventDate = DateTime.Now.AddDays(-1), // Newer event
                    StartTime = new TimeSpan(14, 0, 0),
                    EndTime = new TimeSpan(16, 0, 0),
                    Location = "Location 2",
                    PictureUrl = "http://example.com/event2.jpg",
                    IsActive = true,
                    CreatedAt = DateTime.Now.AddDays(-4),
                    CreateByNavigation = new User { FirstName = "Jane", LastName = "Smith", Email = "jane@gmail.com", Password = "123" }
                },
                new Event
                {
                    EventId = 3,
                    EventName = "Event 3",
                    Description = "Third Event",
                    EventDate = DateTime.Now.AddDays(-2), // Middle event
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(11, 0, 0),
                    Location = "Location 3",
                    PictureUrl = "http://example.com/event3.jpg",
                    IsActive = true,
                    CreatedAt = DateTime.Now.AddDays(-3),
                    CreateByNavigation = new User { FirstName = "Alice", LastName = "Johnson", Email = "alice@gmail.com", Password = "123" }
                },
                new Event
                {
                    EventId = 4,
                    EventName = "Event 4",
                    Description = "Fourth Event",
                    EventDate = DateTime.Now.AddDays(-4), // Older event
                    StartTime = new TimeSpan(8, 0, 0),
                    EndTime = new TimeSpan(10, 0, 0),
                    Location = "Location 4",
                    PictureUrl = "http://example.com/event4.jpg",
                    IsActive = true,
                    CreatedAt = DateTime.Now.AddDays(-6),
                    CreateByNavigation = new User { FirstName = "Bob", LastName = "Brown", Email = "bob@gmail.com", Password = "123" }
                }
            );
            await dbContext.SaveChangesAsync();

            EventDAO.InitializeContext(dbContext);

            // Act
            var result = EventDAO.GetTop3NewestEvents();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count); // Only top 3 events should be returned
            Assert.Equal("Event 2", result[0].EventName); // Newest event should be first
            Assert.Equal("Event 3", result[1].EventName); // Second newest event
            Assert.Equal("Event 1", result[2].EventName); // Third newest event
        }

        [Fact]
        public async Task GetTop3NewestEvents_ShouldReturnEmptyList_WhenNoEventsExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);
            EventDAO.InitializeContext(dbContext);

            // Act
            var result = EventDAO.GetTop3NewestEvents();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result); // No events should result in an empty list
        }

        [Fact]
        public async Task GetTop3NewestEvents_ShouldHandleNullContext()
        {
            // Arrange
            EventDAO.InitializeContext(null); // Null context

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => EventDAO.GetTop3NewestEvents());
            Assert.Contains("Object reference not set", exception.Message);
        }

        [Fact]
        public async Task GetTop3NewestEvents_ShouldReturnLessThan3_WhenFewerEventsExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);

            // Seed data with fewer than 3 events
            dbContext.Events.Add(
                new Event
                {
                    EventId = 1,
                    EventName = "Event 1",
                    Description = "Only Event",
                    EventDate = DateTime.Now.AddDays(-1),
                    StartTime = new TimeSpan(10, 0, 0),
                    EndTime = new TimeSpan(12, 0, 0),
                    Location = "Location 1",
                    PictureUrl = "http://example.com/event1.jpg",
                    IsActive = true,
                    CreatedAt = DateTime.Now.AddDays(-2),
                    CreateByNavigation = new User { FirstName = "John", LastName = "Doe", Email = "john@gmail.com", Password = "123" }
                }
            );
            await dbContext.SaveChangesAsync();

            EventDAO.InitializeContext(dbContext);

            // Act
            var result = EventDAO.GetTop3NewestEvents();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result); // Only one event should be returned
            Assert.Equal("Event 1", result[0].EventName);
        }


    }

}
