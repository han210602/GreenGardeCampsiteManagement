using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess.DAO;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGardenAPITest.DAO
{
    public class FoodComboDAOTest
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

            // Seed data for Food combo
            if (!await databaseContext.FoodCombos.AnyAsync()) // Only add data if the table is empty
            {
                databaseContext.FoodCombos.AddRange(
                new FoodCombo
                {
                    ComboId = 1,
                    ComboName = "Combo A",
                    Price = 1000,
                    ImgUrl = "http://example.com/comboA.jpg"
                },
                new FoodCombo
                {
                    ComboId = 2,
                    ComboName = "Combo B",
                    Price = 2000,
                    ImgUrl = "http://example.com/comboB.jpg"
                }
            );

                await databaseContext.SaveChangesAsync();
            }

            return databaseContext;

        }

        // Test for method GetAllComboFoods
        [Fact]
        public async Task GetAllComboFoods_ShouldReturnAllCombos_WhenCombosExist()
        {
            // Arrange
            var dbContext = await GetDbContext();
            FoodComboDAO.InitializeContext(dbContext);

            // Act
            var result = FoodComboDAO.getAllComboFoods();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().Contain(c => c.ComboName == "Combo A" && c.Price == 1000);
            result.Should().Contain(c => c.ComboName == "Combo B" && c.Price == 2000);
        }

        [Fact]
        public async Task GetAllComboFoods_ShouldReturnEmptyList_WhenNoCombosExist()
        {
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // unique name for each test
                .Options;

            var emptyContext = new GreenGardenContext(options);
            emptyContext.Database.EnsureCreated();
            // Arrange
            
            FoodComboDAO.InitializeContext(emptyContext);

            // Act
            var result = FoodComboDAO.getAllComboFoods();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllComboFoods_ShouldThrowException_WhenRepositoryThrowsException()
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
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(FoodComboDAO.getAllComboFoods()));
        }

        // Test for method GetAllComboFoods
        private async Task<GreenGardenContext> GetDbContextWithCustomerComboFoods()
        {
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique database for each test
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated();

            // Seed mock data
            if (!await dbContext.FoodCombos.AnyAsync())
            {
                dbContext.FoodCombos.AddRange(
                    new FoodCombo
                    {
                        ComboId = 1,
                        ComboName = "Combo A",
                        Price = 1000,
                        ImgUrl = "http://example.com/comboA.jpg",
                        Status = true
                    },
                    new FoodCombo
                    {
                        ComboId = 2,
                        ComboName = "Combo B",
                        Price = 2000,
                        ImgUrl = "http://example.com/comboB.jpg",
                        Status = false
                    },
                    new FoodCombo
                    {
                        ComboId = 3,
                        ComboName = "Combo C",
                        Price = 1500,
                        ImgUrl = "http://example.com/comboC.jpg",
                        Status = true
                    }
                );

                await dbContext.SaveChangesAsync();
            }

            return dbContext;
        }

        [Fact]
        public async Task GetAllCustomerComboFoods_ShouldReturnActiveCombosOnly()
        {
            // Arrange
            var dbContext = await GetDbContextWithCustomerComboFoods();
            FoodComboDAO.InitializeContext(dbContext);

            // Act
            var result = FoodComboDAO.getComboFoodDetail();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2); // Only combos with Status == true
            result.Should().Contain(c => c.ComboName == "Combo A" && c.Price == 1000);
            result.Should().Contain(c => c.ComboName == "Combo C" && c.Price == 1500);
            result.Should().NotContain(c => c.ComboName == "Combo B");
        }

        [Fact]
        public async Task GetAllCustomerComboFoods_ShouldReturnEmptyList_WhenNoActiveCombosExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated();

            // Seed only inactive combos
            dbContext.FoodCombos.AddRange(
                new FoodCombo
                {
                    ComboId = 1,
                    ComboName = "Combo A",
                    Price = 1000,
                    ImgUrl = "http://example.com/comboA.jpg",
                    Status = false
                },
                new FoodCombo
                {
                    ComboId = 2,
                    ComboName = "Combo B",
                    Price = 2000,
                    ImgUrl = "http://example.com/comboB.jpg",
                    Status = false
                }
            );
            await dbContext.SaveChangesAsync();

            FoodComboDAO.InitializeContext(dbContext);

            // Act
            var result = FoodComboDAO.getComboFoodDetail();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty(); // No active combos
        }

        [Fact]
        public async Task GetAllCustomerComboFoods_ShouldThrowException_WhenRepositoryThrowsException()
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
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(FoodComboDAO.getComboFoodDetail()));
        }

        // Test for method getComboFoodDetail
        private async Task<GreenGardenContext> GetDbContextWithFoodComboDetails()
        {
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique database for each test
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated();

            // Seed mock data
            if (!await dbContext.FoodCombos.AnyAsync())
            {
                dbContext.FoodCombos.Add(new FoodCombo
                {
                    ComboId = 1,
                    ComboName = "Combo A",
                    Price = 1000,
                    ImgUrl = "http://example.com/comboA.jpg",
                    Description = "Description for Combo A",
                    
                });

                dbContext.FoodCombos.Add(new FoodCombo
                {
                    ComboId = 2,
                    ComboName = "Combo B",
                    Price = 1500,
                    ImgUrl = "http://example.com/comboB.jpg",
                    Description = "Description for Combo B",
                   
                });

                await dbContext.SaveChangesAsync();
            }

            return dbContext;
        }

        [Fact]
        public async Task GetComboFoodDetail_ShouldReturnComboDetails_WhenComboExists()
        {
            // Arrange
            var dbContext = await GetDbContextWithFoodComboDetails();
            FoodComboDAO.InitializeContext(dbContext);

            // Act
            var result = FoodComboDAO.getComboFoodDetail(1);

            // Assert
            result.Should().NotBeNull();
            result.ComboId.Should().Be(1);
            result.ComboName.Should().Be("Combo A");
            result.Price.Should().Be(1000);
            result.Description.Should().Be("Description for Combo A");
            
        }

        [Fact]
        public async Task GetComboFoodDetail_ShouldReturnNull_WhenComboDoesNotExist()
        {
            // Arrange
            var dbContext = await GetDbContextWithFoodComboDetails();
            FoodComboDAO.InitializeContext(dbContext);

            // Act
            var result = FoodComboDAO.getComboFoodDetail(99); // Non-existent ID

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetComboFoodDetail_ShouldThrowException_WhenRepositoryThrowsException()
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
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(FoodComboDAO.getComboFoodDetail()));
        }
    }
}
