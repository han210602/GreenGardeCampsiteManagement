using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess.DAO;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GreenGardenAPITest.DAO
{
    public class ComboDAOTest
    {
        // Helper method to create an in-memory database with mock data
        private async Task<GreenGardenContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // unique name for each test
                .Options;

            var databaseContext = new GreenGardenContext(options);
            databaseContext.Database.EnsureCreated();

            // Clear the change tracker to ensure no duplicates
            databaseContext.ChangeTracker.Clear();

            // Seed data for Combos with the updated properties
            if (!await databaseContext.Combos.AnyAsync()) // Only add data if the table is empty
            {
                databaseContext.Combos.AddRange(
                    new Combo
                    {
                        ComboId = 1,
                        ComboName = "Combo A",
                        Description = "Description for Combo A",
                        Price = 1000,
                        ImgUrl = "http://example.com/comboA.jpg",
                        CreatedAt = DateTime.Now,
                        Status = true
                    },
                    new Combo
                    {
                        ComboId = 2,
                        ComboName = "Combo B",
                        Description = "Description for Combo B",
                        Price = 1500,
                        ImgUrl = "http://example.com/comboB.jpg",
                        CreatedAt = DateTime.Now,
                        Status = true
                    },
                    new Combo
                    {
                        ComboId = 3,
                        ComboName = "Combo C",
                        Description = "Description for Combo C",
                        Price = 2000,
                        ImgUrl = "http://example.com/comboC.jpg",
                        CreatedAt = DateTime.Now,
                        Status = false // Combo C is inactive
                    }
                );
                await databaseContext.SaveChangesAsync();
            }

            return databaseContext;
        }

        // Test for GetListCombo method ----------------------------------------------------------------
        [Fact]
        public async Task GetListCombo_ShouldReturnListOfCombo()
        {
            // Arrange
            var dbContext = await GetDbContext(); // Get in-memory db context
            ComboDAO.InitializeContext(dbContext); // Initialize the ComboDAO context with the db context

            // Act
            var result = ComboDAO.GetListCombo(); // Fetch the list of combos

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3); // Ensure that we have 3 items in the list

            var firstCombo = result.First(); // Get the first combo to assert values
            firstCombo.ComboId.Should().Be(1);
            firstCombo.ComboName.Should().Be("Combo A");
            firstCombo.Description.Should().Be("Description for Combo A");
            firstCombo.Price.Should().Be(1000);
            firstCombo.ImgUrl.Should().Be("http://example.com/comboA.jpg");

        }

        [Fact]
        public async Task GetListCombo_ShouldReturnEmptyList_WhenNoCombosExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var emptyContext = new GreenGardenContext(options);
            emptyContext.Database.EnsureCreated();

            ComboDAO.InitializeContext(emptyContext);

            // Act
            var result = ComboDAO.GetListCombo();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty(); // Ensure the list is empty
        }

        [Fact]
        public async Task GetListCombo_ShouldThrowException_WhenRepositoryThrowsException()
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
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(ComboDAO.GetListCombo()));

        }

        // Test for GetListCustomerCombo method ----------------------------------------------------------------
        [Fact]
        public async Task GetListCustomerCombo_ShouldReturnListOfCombo()
        {
            // Arrange
            var dbContext = await GetDbContext(); // Get in-memory db context
            ComboDAO.InitializeContext(dbContext); // Initialize the ComboDAO context with the db context

            // Act
            var result = ComboDAO.GetListCustomerCombo(); // Fetch the list of customer combos

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2); // Ensure that we have 2 active combos (Combo C should not appear as it's inactive)

            var firstCombo = result.First(); // Get the first combo to assert values
            firstCombo.ComboId.Should().Be(1);
            firstCombo.ComboName.Should().Be("Combo A");
            firstCombo.Description.Should().Be("Description for Combo A");
            firstCombo.Price.Should().Be(1000);
            firstCombo.ImgUrl.Should().Be("http://example.com/comboA.jpg");

            var secondCombo = result.Skip(1).First(); // Get the second combo to assert values
            secondCombo.ComboId.Should().Be(2);
            secondCombo.ComboName.Should().Be("Combo B");
            secondCombo.Description.Should().Be("Description for Combo B");
            secondCombo.Price.Should().Be(1500);
            secondCombo.ImgUrl.Should().Be("http://example.com/comboB.jpg");
        }

        [Fact]
        public async Task GetListCustomerCombo_ShouldReturnEmptyList_WhenNoActiveCombosExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var emptyContext = new GreenGardenContext(options);
            emptyContext.Database.EnsureCreated();

            ComboDAO.InitializeContext(emptyContext);

            // Act
            var result = ComboDAO.GetListCustomerCombo(); // Fetch the list of customer combos

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty(); // Ensure the list is empty when no active combos exist
        }

        [Fact]
        public async Task GetListCustomerCombo_ShouldThrowException_WhenRepositoryThrowsException()
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
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(ComboDAO.GetListCustomerCombo()));

        }

        // Test for ChangeComboStatus method ----------------------------------------------------------------
        [Fact]
        public async Task ChangeComboStatus_ShouldUpdateStatus_WhenComboExists()
        {
            // Arrange
            var dbContext = await GetDbContext(); // Get in-memory db context
            ComboDAO.InitializeContext(dbContext); // Initialize the ComboDAO context with the db context

            var comboId = 1; // Existing comboId
            var newStatus = new ChangeComboStatus { Status = false }; // New status to be updated

            // Act
            ComboDAO.ChangeComboStatus(comboId); // Call the method to change status

            // Assert
            var updatedCombo = dbContext.Combos.FirstOrDefault(c => c.ComboId == comboId);
            updatedCombo.Should().NotBeNull();
            updatedCombo.Status.Should().Be(false); // Ensure the status was updated correctly
        }

        [Fact]
        public async Task ChangeComboStatus_ShouldThrowException_WhenComboDoesNotExist()
        {
            // Arrange
            var dbContext = await GetDbContext(); // Get in-memory db context
            ComboDAO.InitializeContext(dbContext); // Initialize the ComboDAO context with the db context

            var comboId = 999; // Non-existing comboId
            var newStatus = new ChangeComboStatus { Status = true };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.Run(() => ComboDAO.ChangeComboStatus(comboId)));
            exception.Message.Should().Be($"Combo with ID {comboId} does not exist.");
        }

        [Fact]
        public async Task ChangeComboStatus_ShouldThrowException_WhenContextIsNull()
        {
            // Arrange
            ComboDAO.InitializeContext(null); // Khởi tạo context là null

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() =>
                Task.Run(() => ComboDAO.ChangeComboStatus(1))
            );

            exception.Message.Should().NotBeNullOrEmpty(); // Đảm bảo exception có thông điệp
            exception.Message.Should().Be("Object reference not set to an instance of an object.");
        }


        //----------------------------------------------------------------

        private async Task<GreenGardenContext> GetDbContextForDetail()
        {
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // unique name for each test
                .Options;

            var databaseContext = new GreenGardenContext(options);
            databaseContext.Database.EnsureCreated();

            // Clear the change tracker to ensure no duplicates
            databaseContext.ChangeTracker.Clear();

            // Seed data for Combos
            if (!await databaseContext.Combos.AnyAsync()) // Only add data if the table is empty
            {
                databaseContext.Combos.AddRange(
                    new Combo
                    {
                        ComboId = 1,
                        ComboName = "Combo A",
                        Description = "Description for Combo A",
                        Price = 1000,
                        ImgUrl = "http://example.com/comboA.jpg",
                        CreatedAt = DateTime.Now,
                        Status = true,
                        ComboTicketDetails = new[]
                        {
                            new ComboTicketDetail { TicketId = 1, Quantity = 2, Description = "Ticket for Combo A" }
                        },
                        ComboCampingGearDetails = new[]
                        {
                            new ComboCampingGearDetail { GearId = 1, Quantity = 1 }
                        },
                        ComboFootDetails = new[]
                        {
                            new ComboFootDetail { ItemId = 1, Quantity = 1 }
                        }
                    },
                    new Combo
                    {
                        ComboId = 2,
                        ComboName = "Combo B",
                        Description = "Description for Combo B",
                        Price = 1500,
                        ImgUrl = "http://example.com/comboB.jpg",
                        CreatedAt = DateTime.Now,
                        Status = false,
                        ComboTicketDetails = new[]
                        {
                            new ComboTicketDetail { TicketId = 2, Quantity = 1, Description = "Ticket for Combo B" }
                        },
                        ComboCampingGearDetails = new[]
                        {
                            new ComboCampingGearDetail { GearId = 2, Quantity = 3 }
                        },
                        ComboFootDetails = new[]
                        {
                            new ComboFootDetail { ItemId = 2, Quantity = 2 }
                        }
                    }
                );
                await databaseContext.SaveChangesAsync();
            }

            return databaseContext;
        }

        // Test for GetComboDetail method ----------------------------------------------------------------
        [Fact]
        public async Task GetComboDetail_ShouldReturnComboDetail_WhenComboExists()
        {
            // Arrange
            var dbContext = await GetDbContextForDetail();
            ComboDAO.InitializeContext(dbContext);

            var comboId = 1; // Existing ComboId

            // Act
            var result = ComboDAO.GetComboDetail(comboId);

            // Assert
            result.Should().NotBeNull();
            result.ComboId.Should().Be(comboId);
            result.ComboName.Should().Be("Combo A");
            
        }

        [Fact]
        public async Task GetComboDetail_ShouldReturnNull_WhenComboDoesNotExist()
        {
            // Arrange
            var dbContext = await GetDbContext();
            ComboDAO.InitializeContext(dbContext);

            var comboId = 999; // Non-existing ComboId

            // Act
            var result = ComboDAO.GetComboDetail(comboId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetComboDetail_ShouldThrowException_WhenDatabaseFails()
        {
            // Arrange
            var dbContext = await GetDbContext();
            ComboDAO.InitializeContext(dbContext);

            // Simulate an error scenario by making the context null
            ComboDAO.InitializeContext(null);

            var comboId = 1;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.Run(() => ComboDAO.GetComboDetail(comboId)));
            exception.Message.Should().Contain("Object reference not set to an instance of an object.");
        }

        //----------------------------------------------------------------

        private async Task<GreenGardenContext> GetDbContextForAddCombo()
        {
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // unique name for each test
                .Options;

            var databaseContext = new GreenGardenContext(options);
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }

        // Test case for AddNewCombo method ----------------------------------------------------------------
        [Fact]
        public async Task AddNewCombo_ShouldAddComboWithAllDetails()
        {
            // Arrange
            var dbContext = await GetDbContextForAddCombo();
            ComboDAO.InitializeContext(dbContext);

            var newCombo = new AddCombo
            {
                ComboName = "Combo A",
                Description = "Description for Combo A",
                Price = 1000,
                ImgUrl = "http://example.com/comboA.jpg",
                ComboCampingGearDetails = new List<ComboCampingGearDetailDTO>
        {
            new ComboCampingGearDetailDTO { GearId = 1, Quantity = 2 }
        },
                ComboFootDetails = new List<ComboFootDetailDTO>
        {
            new ComboFootDetailDTO { ItemId = 1, Quantity = 3 }
        },
                ComboTicketDetails = new List<ComboTicketDetailDTO>
        {
            new ComboTicketDetailDTO { TicketId = 1, Quantity = 5, Description = "Ticket for Combo A" }
        }
            };

            // Act
            ComboDAO.AddNewCombo(newCombo);

            // Assert
            var addedCombo = dbContext.Combos
                .Include(c => c.ComboCampingGearDetails)
                .Include(c => c.ComboFootDetails)
                .Include(c => c.ComboTicketDetails)
                .FirstOrDefault(c => c.ComboName == "Combo A");

            addedCombo.Should().NotBeNull();
            addedCombo!.Description.Should().Be("Description for Combo A");
            addedCombo.Price.Should().Be(1000);
            addedCombo.ImgUrl.Should().Be("http://example.com/comboA.jpg");

            // Verify ComboCampingGearDetails
            addedCombo.ComboCampingGearDetails.Should().HaveCount(1);
            var campingGearDetail = addedCombo.ComboCampingGearDetails.First();
            campingGearDetail.GearId.Should().Be(1);
            campingGearDetail.Quantity.Should().Be(2);

            // Verify ComboFootDetails
            addedCombo.ComboFootDetails.Should().HaveCount(1);
            var footDetail = addedCombo.ComboFootDetails.First();
            footDetail.ItemId.Should().Be(1);
            footDetail.Quantity.Should().Be(3);

            // Verify ComboTicketDetails
            addedCombo.ComboTicketDetails.Should().HaveCount(1);
            var ticketDetail = addedCombo.ComboTicketDetails.First();
            ticketDetail.TicketId.Should().Be(1);
            ticketDetail.Quantity.Should().Be(5);
            ticketDetail.Description.Should().Be("Ticket for Combo A");
        }

        [Fact]
        public async Task AddNewCombo_ShouldAddComboWithoutCampingGearDetails()
        {
            // Arrange
            var dbContext = await GetDbContextForAddCombo();
            ComboDAO.InitializeContext(dbContext);

            var newCombo = new AddCombo
            {
                ComboName = "Combo B",
                Description = "Description for Combo B",
                Price = 1500,
                ImgUrl = "http://example.com/comboB.jpg",
                ComboCampingGearDetails = null, // No camping gear details
                ComboFootDetails = new List<ComboFootDetailDTO>
            {
                new ComboFootDetailDTO { ItemId = 1, Quantity = 2 }
            },
                ComboTicketDetails = new List<ComboTicketDetailDTO>
            {
                new ComboTicketDetailDTO { TicketId = 2, Quantity = 3, Description = "Ticket for Combo B" }
            }
            };

            // Act
            ComboDAO.AddNewCombo(newCombo);

            // Assert
            var addedCombo = dbContext.Combos
                .Include(c => c.ComboCampingGearDetails)
                .Include(c => c.ComboFootDetails)
                .Include(c => c.ComboTicketDetails)
                .FirstOrDefault(c => c.ComboName == "Combo B");

            addedCombo.Should().NotBeNull();
            addedCombo.ComboName.Should().Be("Combo B");
            addedCombo.ComboCampingGearDetails.Should().BeEmpty();
            addedCombo.ComboFootDetails.Should().NotBeEmpty();
            addedCombo.ComboTicketDetails.Should().NotBeEmpty();
        }

        [Fact]
        public async Task AddNewCombo_ShouldAddComboWithoutFoodDetails()
        {
            // Arrange
            var dbContext = await GetDbContextForAddCombo();
            ComboDAO.InitializeContext(dbContext);

            var newCombo = new AddCombo
            {
                ComboName = "Combo C",
                Description = "Description for Combo C",
                Price = 1200,
                ImgUrl = "http://example.com/comboC.jpg",
                ComboCampingGearDetails = new List<ComboCampingGearDetailDTO>
            {
                new ComboCampingGearDetailDTO { GearId = 2, Quantity = 1 }
            },
                ComboFootDetails = null, // No foot details
                ComboTicketDetails = new List<ComboTicketDetailDTO>
            {
                new ComboTicketDetailDTO { TicketId = 3, Quantity = 2, Description = "Ticket for Combo C" }
            }
            };

            // Act
            ComboDAO.AddNewCombo(newCombo);

            // Assert
            var addedCombo = dbContext.Combos
                .Include(c => c.ComboCampingGearDetails)
                .Include(c => c.ComboFootDetails)
                .Include(c => c.ComboTicketDetails)
                .FirstOrDefault(c => c.ComboName == "Combo C");

            addedCombo.Should().NotBeNull();
            addedCombo.ComboName.Should().Be("Combo C");
            addedCombo.ComboFootDetails.Should().BeEmpty();
        }

        [Fact]
        public async Task AddNewCombo_ShouldAddComboWithoutTicketDetails()
        {
            // Arrange
            var dbContext = await GetDbContextForAddCombo();
            ComboDAO.InitializeContext(dbContext);

            var newCombo = new AddCombo
            {
                ComboName = "Combo D",
                Description = "Description for Combo D",
                Price = 1400,
                ImgUrl = "http://example.com/comboD.jpg",
                ComboCampingGearDetails = new List<ComboCampingGearDetailDTO>
            {
                new ComboCampingGearDetailDTO { GearId = 3, Quantity = 2 }
            },
                ComboFootDetails = new List<ComboFootDetailDTO>
            {
                new ComboFootDetailDTO { ItemId = 2, Quantity = 1 }
            },
                ComboTicketDetails = null // No ticket details
            };

            // Act
            ComboDAO.AddNewCombo(newCombo);

            // Assert
            var addedCombo = dbContext.Combos
                .Include(c => c.ComboCampingGearDetails)
                .Include(c => c.ComboFootDetails)
                .Include(c => c.ComboTicketDetails)
                .FirstOrDefault(c => c.ComboName == "Combo D");

            addedCombo.Should().NotBeNull();
            addedCombo.ComboName.Should().Be("Combo D");
            addedCombo.ComboTicketDetails.Should().BeEmpty();
        }

        //[Fact]
        //public async Task AddNewCombo_ShouldThrowException_WhenRequiredFieldsAreMissing()
        //{
        //    // Arrange
        //    var dbContext = await GetDbContextForAddCombo();
        //    ComboDAO.InitializeContext(dbContext);

        //    var invalidCombo = new AddCombo
        //    {
        //        ComboName = "", // Missing ComboName
        //        Description = "Missing ComboName",
        //        Price = 1000,
        //        ImgUrl = "http://example.com/comboF.jpg"
        //    };

        //    // Act & Assert
        //    var exception = await Assert.ThrowsAsync<Exception>(() => Task.Run(() => ComboDAO.AddNewCombo(invalidCombo)));
        //    exception.Message.Should().Be("Error adding new combo: The ComboName cannot be empty.");
        //}

        //[Fact]
        //public async Task AddNewCombo_ShouldThrowException_WhenPriceIsNegative()
        //{
        //    // Arrange
        //    var dbContext = await GetDbContextForAddCombo();
        //    ComboDAO.InitializeContext(dbContext);

        //    var invalidCombo = new AddCombo
        //    {
        //        ComboName = "Combo G",
        //        Description = "Description for Combo G",
        //        Price = -100, // Invalid negative price
        //        ImgUrl = "http://example.com/comboG.jpg"
        //    };

        //    // Act & Assert
        //    var exception = await Assert.ThrowsAsync<Exception>(() => Task.Run(() => ComboDAO.AddNewCombo(invalidCombo)));
        //    exception.Message.Should().Be("Error adding new combo: Price cannot be negative.");
        //}

        [Fact]
        public async Task AddNewCombo_ShouldThrowException_WhenDatabaseFails()
        {
           
            // Khởi tạo context với ComboDAO
            ComboDAO.InitializeContext(null);

            var newCombo = new AddCombo
            {
                ComboName = "Combo H",
                Description = "Description for Combo H",
                Price = 2000,
                ImgUrl = "http://example.com/comboH.jpg",
                ComboCampingGearDetails = new List<ComboCampingGearDetailDTO>
        {
            new ComboCampingGearDetailDTO { GearId = 1, Quantity = 2 }
        },
                ComboFootDetails = new List<ComboFootDetailDTO>
        {
            new ComboFootDetailDTO { ItemId = 1, Quantity = 3 }
        },
                ComboTicketDetails = new List<ComboTicketDetailDTO>
        {
            new ComboTicketDetailDTO { TicketId = 1, Quantity = 5, Description = "Ticket for Combo A" }
        }
            };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => ComboDAO.AddNewCombo(newCombo));
            Assert.Contains("Object reference not set", exception.Message);


        }

        // Test case for UpdateCombo method ----------------------------------------------------------------
        [Fact]
        public async Task UpdateCombo_ShouldUpdateComboWithNewDetails()
        {
            // Arrange
            var dbContext = await GetDbContext();
            ComboDAO.InitializeContext(dbContext);

            // Existing ComboId
            var comboId = 1;

            var updatedCombo = new AddCombo
            {
                ComboId = comboId,
                ComboName = "Updated Combo Name",
                Description = "Updated Description",
                Price = 1500,
                ImgUrl = "http://example.com/updatedcombo.jpg",
                ComboCampingGearDetails = new List<ComboCampingGearDetailDTO>
        {
            new ComboCampingGearDetailDTO { GearId = 1, Quantity = 5 }, // Update quantity
            new ComboCampingGearDetailDTO { GearId = 2, Quantity = 3 }  // Add new detail
        },
                ComboFootDetails = new List<ComboFootDetailDTO>
        {
            new ComboFootDetailDTO { ItemId = 1, Quantity = 10 } // Update quantity
        },
                ComboTicketDetails = new List<ComboTicketDetailDTO>
        {
            new ComboTicketDetailDTO { TicketId = 1, Quantity = 8, Description = "Updated Ticket Description" },
            new ComboTicketDetailDTO { TicketId = 2, Quantity = 4, Description = "New Ticket Detail" }
        }
            };

            // Act
            ComboDAO.UpdateCombo(updatedCombo);

            // Assert
            var updatedComboFromDb = dbContext.Combos
                .Include(c => c.ComboCampingGearDetails)
                .Include(c => c.ComboFootDetails)
                .Include(c => c.ComboTicketDetails)
                .FirstOrDefault(c => c.ComboId == comboId);

            updatedComboFromDb.Should().NotBeNull();
            updatedComboFromDb!.ComboName.Should().Be("Updated Combo Name");
            updatedComboFromDb.Description.Should().Be("Updated Description");
            updatedComboFromDb.Price.Should().Be(1500);
            updatedComboFromDb.ImgUrl.Should().Be("http://example.com/updatedcombo.jpg");

            // Assert ComboCampingGearDetails
            updatedComboFromDb.ComboCampingGearDetails.Should().HaveCount(2);
            var updatedCampingGear = updatedComboFromDb.ComboCampingGearDetails.First(c => c.GearId == 1);
            updatedCampingGear.Quantity.Should().Be(5);
            var newCampingGear = updatedComboFromDb.ComboCampingGearDetails.First(c => c.GearId == 2);
            newCampingGear.Quantity.Should().Be(3);

            // Assert ComboFootDetails
            updatedComboFromDb.ComboFootDetails.Should().HaveCount(1);
            var updatedFootDetail = updatedComboFromDb.ComboFootDetails.First();
            updatedFootDetail.ItemId.Should().Be(1);
            updatedFootDetail.Quantity.Should().Be(10);

            // Assert ComboTicketDetails
            updatedComboFromDb.ComboTicketDetails.Should().HaveCount(2);
            var updatedTicket = updatedComboFromDb.ComboTicketDetails.First(t => t.TicketId == 1);
            updatedTicket.Quantity.Should().Be(8);
            updatedTicket.Description.Should().Be("Updated Ticket Description");

            var newTicket = updatedComboFromDb.ComboTicketDetails.First(t => t.TicketId == 2);
            newTicket.Quantity.Should().Be(4);
            newTicket.Description.Should().Be("New Ticket Detail");
        }

        //[Fact]
        //public async Task UpdateCombo_ShouldUpdateComboWithoutCampingGearDetails()
        //{
        //    // Arrange
        //    var dbContext = await GetDbContext();
        //    ComboDAO.InitializeContext(dbContext);

        //    var comboId = 1;

        //    // Create initial combo with camping gear details
        //    var existingCombo = new Combo
        //    {
        //        ComboId = comboId,
        //        ComboName = "Original Combo Name",
        //        Description = "Original Description",
        //        Price = 1000,
        //        ImgUrl = "http://example.com/originalcombo.jpg",
        //        ComboCampingGearDetails = new List<ComboCampingGearDetail>
        //{
        //    new ComboCampingGearDetail { GearId = 1, Quantity = 3 }
        //},
        //        ComboFootDetails = new List<ComboFootDetail>
        //{
        //    new ComboFootDetail { ItemId = 1, Quantity = 5 }
        //},
        //        ComboTicketDetails = new List<ComboTicketDetail>
        //{
        //    new ComboTicketDetail { TicketId = 1, Quantity = 5, Description = "Original Ticket" }
        //}
        //    };
        //    dbContext.Combos.Add(existingCombo);
        //    dbContext.SaveChanges();

        //    // Updated combo without camping gear details
        //    var updatedCombo = new AddCombo
        //    {
        //        ComboId = comboId,
        //        ComboName = "Updated Combo Name",
        //        Description = "Updated Description",
        //        Price = 1200,
        //        ImgUrl = "http://example.com/updatedcombo.jpg",
        //        ComboFootDetails = new List<ComboFootDetailDTO>
        //{
        //    new ComboFootDetailDTO { ItemId = 1, Quantity = 10 }
        //},
        //        ComboTicketDetails = new List<ComboTicketDetailDTO>
        //{
        //    new ComboTicketDetailDTO { TicketId = 1, Quantity = 6, Description = "Updated Ticket" }
        //}
        //    };

        //    // Act
        //    ComboDAO.UpdateCombo(updatedCombo);

        //    // Assert
        //    var updatedComboFromDb = dbContext.Combos
        //        .Include(c => c.ComboCampingGearDetails)
        //        .Include(c => c.ComboFootDetails)
        //        .Include(c => c.ComboTicketDetails)
        //        .FirstOrDefault(c => c.ComboId == comboId);

        //    updatedComboFromDb.Should().NotBeNull();
        //    updatedComboFromDb.ComboName.Should().Be("Updated Combo Name");
        //    updatedComboFromDb.Description.Should().Be("Updated Description");
        //    updatedComboFromDb.Price.Should().Be(1200);
        //    updatedComboFromDb.ImgUrl.Should().Be("http://example.com/updatedcombo.jpg");

        //    // Assert that the camping gear details remain unchanged
        //    updatedComboFromDb.ComboCampingGearDetails.Should().HaveCount(1);
        //    updatedComboFromDb.ComboCampingGearDetails.First().Quantity.Should().Be(3);
        //}

        //[Fact]
        //public async Task UpdateCombo_ShouldUpdateComboWithoutFoodDetails()
        //{
        //    // Arrange
        //    var dbContext = await GetDbContext();
        //    ComboDAO.InitializeContext(dbContext);

        //    var comboId = 1;

        //    // Create initial combo with food details
        //    var existingCombo = new Combo
        //    {
        //        ComboId = comboId,
        //        ComboName = "Original Combo Name",
        //        Description = "Original Description",
        //        Price = 1000,
        //        ImgUrl = "http://example.com/originalcombo.jpg",
        //        ComboCampingGearDetails = new List<ComboCampingGearDetail>
        //{
        //    new ComboCampingGearDetail { GearId = 1, Quantity = 3 }
        //},
        //        ComboFootDetails = new List<ComboFootDetail>
        //{
        //    new ComboFootDetail { ItemId = 1, Quantity = 5 }
        //},
        //        ComboTicketDetails = new List<ComboTicketDetail>
        //{
        //    new ComboTicketDetail { TicketId = 1, Quantity = 5, Description = "Original Ticket" }
        //}
        //    };
        //    dbContext.Combos.Add(existingCombo);
        //    dbContext.SaveChanges();

        //    // Updated combo without food details
        //    var updatedCombo = new AddCombo
        //    {
        //        ComboId = comboId,
        //        ComboName = "Updated Combo Name",
        //        Description = "Updated Description",
        //        Price = 1200,
        //        ImgUrl = "http://example.com/updatedcombo.jpg",
        //        ComboCampingGearDetails = new List<ComboCampingGearDetailDTO>
        //{
        //    new ComboCampingGearDetailDTO { GearId = 1, Quantity = 4 }
        //},
        //        ComboTicketDetails = new List<ComboTicketDetailDTO>
        //{
        //    new ComboTicketDetailDTO { TicketId = 1, Quantity = 6, Description = "Updated Ticket" }
        //}
        //    };

        //    // Act
        //    ComboDAO.UpdateCombo(updatedCombo);

        //    // Assert
        //    var updatedComboFromDb = dbContext.Combos
        //        .Include(c => c.ComboCampingGearDetails)
        //        .Include(c => c.ComboFootDetails)
        //        .Include(c => c.ComboTicketDetails)
        //        .FirstOrDefault(c => c.ComboId == comboId);

        //    updatedComboFromDb.Should().NotBeNull();
        //    updatedComboFromDb.ComboName.Should().Be("Updated Combo Name");
        //    updatedComboFromDb.Description.Should().Be("Updated Description");
        //    updatedComboFromDb.Price.Should().Be(1200);
        //    updatedComboFromDb.ImgUrl.Should().Be("http://example.com/updatedcombo.jpg");

        //    // Assert that food details remain unchanged
        //    updatedComboFromDb.ComboFootDetails.Should().HaveCount(1);
        //    updatedComboFromDb.ComboFootDetails.First().Quantity.Should().Be(5);
        //}

        //[Fact]
        //public async Task UpdateCombo_ShouldUpdateComboWithoutTicketDetails()
        //{
        //    // Arrange
        //    var dbContext = await GetDbContext();
        //    ComboDAO.InitializeContext(dbContext);

        //    var comboId = 1;

        //    // Create initial combo with ticket details
        //    var existingCombo = new Combo
        //    {
        //        ComboId = comboId,
        //        ComboName = "Original Combo Name",
        //        Description = "Original Description",
        //        Price = 1000,
        //        ImgUrl = "http://example.com/originalcombo.jpg",
        //        ComboCampingGearDetails = new List<ComboCampingGearDetail>
        //{
        //    new ComboCampingGearDetail { GearId = 1, Quantity = 3 }
        //},
        //        ComboFootDetails = new List<ComboFootDetail>
        //{
        //    new ComboFootDetail { ItemId = 1, Quantity = 5 }
        //},
        //        ComboTicketDetails = new List<ComboTicketDetail>
        //{
        //    new ComboTicketDetail { TicketId = 1, Quantity = 5, Description = "Original Ticket" }
        //}
        //    };
        //    dbContext.Combos.Add(existingCombo);
        //    dbContext.SaveChanges();

        //    // Updated combo without ticket details
        //    var updatedCombo = new AddCombo
        //    {
        //        ComboId = comboId,
        //        ComboName = "Updated Combo Name",
        //        Description = "Updated Description",
        //        Price = 1200,
        //        ImgUrl = "http://example.com/updatedcombo.jpg",
        //        ComboCampingGearDetails = new List<ComboCampingGearDetailDTO>
        //{
        //    new ComboCampingGearDetailDTO { GearId = 1, Quantity = 4 }
        //},
        //        ComboFootDetails = new List<ComboFootDetailDTO>
        //{
        //    new ComboFootDetailDTO { ItemId = 1, Quantity = 6 }
        //}
        //    };

        //    // Act
        //    ComboDAO.UpdateCombo(updatedCombo);

        //    // Assert
        //    var updatedComboFromDb = dbContext.Combos
        //        .Include(c => c.ComboCampingGearDetails)
        //        .Include(c => c.ComboFootDetails)
        //        .Include(c => c.ComboTicketDetails)
        //        .FirstOrDefault(c => c.ComboId == comboId);

        //    updatedComboFromDb.Should().NotBeNull();
        //    updatedComboFromDb.ComboName.Should().Be("Updated Combo Name");
        //    updatedComboFromDb.Description.Should().Be("Updated Description");
        //    updatedComboFromDb.Price.Should().Be(1200);
        //    updatedComboFromDb.ImgUrl.Should().Be("http://example.com/updatedcombo.jpg");

        //    // Assert that ticket details remain unchanged
        //    updatedComboFromDb.ComboTicketDetails.Should().HaveCount(1);
        //    updatedComboFromDb.ComboTicketDetails.First().Quantity.Should().Be(5);
        //    updatedComboFromDb.ComboTicketDetails.First().Description.Should().Be("Original Ticket");
        //}

        //[Fact]
        //public async Task UpdateCombo_ShouldUpdateComboWithoutCampingGearDetails_FoodDetails_TicketDetails()
        //{
        //    // Arrange
        //    var dbContext = await GetDbContext();
        //    ComboDAO.InitializeContext(dbContext);

        //    var comboId = 1;

        //    // Create initial combo with all details
        //    var existingCombo = new Combo
        //    {
        //        ComboId = comboId,
        //        ComboName = "Original Combo Name",
        //        Description = "Original Description",
        //        Price = 1000,
        //        ImgUrl = "http://example.com/originalcombo.jpg",
        //        ComboCampingGearDetails = new List<ComboCampingGearDetail>
        //{
        //    new ComboCampingGearDetail { GearId = 1, Quantity = 3 }
        //},
        //        ComboFootDetails = new List<ComboFootDetail>
        //{
        //    new ComboFootDetail { ItemId = 1, Quantity = 5 }
        //},
        //        ComboTicketDetails = new List<ComboTicketDetail>
        //{
        //    new ComboTicketDetail { TicketId = 1, Quantity = 5, Description = "Original Ticket" }
        //}
        //    };
        //    dbContext.Combos.Add(existingCombo);
        //    dbContext.SaveChanges();

        //    // Updated combo without any details
        //    var updatedCombo = new AddCombo
        //    {
        //        ComboId = comboId,
        //        ComboName = "Updated Combo Name",
        //        Description = "Updated Description",
        //        Price = 1200,
        //        ImgUrl = "http://example.com/updatedcombo.jpg"
        //    };

        //    // Act
        //    ComboDAO.UpdateCombo(updatedCombo);

        //    // Assert
        //    var updatedComboFromDb = dbContext.Combos
        //        .Include(c => c.ComboCampingGearDetails)
        //        .Include(c => c.ComboFootDetails)
        //        .Include(c => c.ComboTicketDetails)
        //        .FirstOrDefault(c => c.ComboId == comboId);

        //    updatedComboFromDb.Should().NotBeNull();
        //    updatedComboFromDb.ComboName.Should().Be("Updated Combo Name");
        //    updatedComboFromDb.Description.Should().Be("Updated Description");
        //    updatedComboFromDb.Price.Should().Be(1200);
        //    updatedComboFromDb.ImgUrl.Should().Be("http://example.com/updatedcombo.jpg");

        //    // Assert that no details were changed
        //    updatedComboFromDb.ComboCampingGearDetails.Should().HaveCount(1);
        //    updatedComboFromDb.ComboCampingGearDetails.First().Quantity.Should().Be(3);

        //    updatedComboFromDb.ComboFootDetails.Should().HaveCount(1);
        //    updatedComboFromDb.ComboFootDetails.First().Quantity.Should().Be(5);

        //    updatedComboFromDb.ComboTicketDetails.Should().HaveCount(1);
        //    updatedComboFromDb.ComboTicketDetails.First().Quantity.Should().Be(5);
        //    updatedComboFromDb.ComboTicketDetails.First().Description.Should().Be("Original Ticket");
        //}

        [Fact]
        public async Task UpdateCombo_ShouldUpdateComboWithInvalidComboId()
        {
            // Arrange
            var dbContext = await GetDbContext();
            ComboDAO.InitializeContext(dbContext);

            var invalidComboId = 999; // An ID that does not exist in the database

            var updatedCombo = new AddCombo
            {
                ComboId = invalidComboId,
                ComboName = "Updated Combo Name",
                Description = "Updated Description",
                Price = 1500,
                ImgUrl = "http://example.com/updatedcombo.jpg"
            };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                 ComboDAO.UpdateCombo(updatedCombo);
            });
        }

        [Fact]
        public void UpdateCombo_ShouldThrowException_WhenContextIsNull()
        {
            // Arrange
            ComboDAO.InitializeContext(null); // Null context

            var updatedCombo = new AddCombo
            {
                ComboId = 1,
                ComboName = "Updated Combo Name",
                Description = "Updated Description",
                Price = 1500,
                ImgUrl = "http://example.com/updatedcombo.jpg",
                ComboCampingGearDetails = new List<ComboCampingGearDetailDTO>
        {
            new ComboCampingGearDetailDTO { GearId = 1, Quantity = 5 }, // Update quantity
            new ComboCampingGearDetailDTO { GearId = 2, Quantity = 3 }  // Add new detail
        },
                ComboFootDetails = new List<ComboFootDetailDTO>
        {
            new ComboFootDetailDTO { ItemId = 1, Quantity = 10 } // Update quantity
        },
                ComboTicketDetails = new List<ComboTicketDetailDTO>
        {
            new ComboTicketDetailDTO { TicketId = 1, Quantity = 8, Description = "Updated Ticket Description" },
            new ComboTicketDetailDTO { TicketId = 2, Quantity = 4, Description = "New Ticket Detail" }
        }
            };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => ComboDAO.UpdateCombo(updatedCombo));
            Assert.Contains("Object reference not set", exception.Message);
        }



    }
}
