using BusinessObject.DTOs;
using BusinessObject.Models;
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
    public class FoodAndDrinkDAOTest
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
            // Seed mock data
            if (!await databaseContext.FoodAndDrinks.AnyAsync())
            {
                var category1 = new FoodAndDrinkCategory { CategoryId = 1, CategoryName = "Beverages" };
                var category2 = new FoodAndDrinkCategory { CategoryId = 2, CategoryName = "Snacks" };

                databaseContext.FoodAndDrinkCategories.AddRange(category1, category2);

                databaseContext.FoodAndDrinks.AddRange(
                    new FoodAndDrink
                    {
                        Status = true,
                        ItemId = 1,
                        ItemName = "Coca-Cola",
                        Price = 1.5m,
                        Description = "Refreshing beverage",
                        Category = category1,
                        ImgUrl = "http://example.com/coke.jpg"
                    },
                    new FoodAndDrink
                    {
                        Status = true,
                        ItemId = 2,
                        ItemName = "Potato Chips",
                        Price = 2.0m,
                        Description = "Crispy and salty",
                        Category = category2,
                        ImgUrl = "http://example.com/chips.jpg"
                    },
                    new FoodAndDrink
                    {
                        ItemId = 3,
                        ItemName = "Expired Snack",
                        Price = 1.0m,
                        Description = "Not for sale",
                        Category = category2,
                        Status = false, // Inactive item
                        ImgUrl = "http://example.com/expired.jpg"
                    }
                    );

                await databaseContext.SaveChangesAsync();
            }

            return databaseContext;
        }

        // Test for method GetAllFoodAndDrink
        [Fact]
        public async Task GetAllFoodAndDrink_ShouldReturnAllItems()
        {
            // Arrange
            var dbContext = await GetDbContext();
            FoodAndDrinkDAO.InitializeContext(dbContext);

            // Act
            var result = FoodAndDrinkDAO.GetAllFoodAndDrink();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);

            result.Should().Contain(item =>
                item.ItemId == 1 &&
                item.ItemName == "Coca-Cola" &&
                item.Price == 1.5m &&
                item.Description == "Refreshing beverage" &&
                item.CategoryName == "Beverages" &&
                item.ImgUrl == "http://example.com/coke.jpg");

            result.Should().Contain(item =>
                item.ItemId == 2 &&
                item.ItemName == "Potato Chips" &&
                item.Price == 2.0m &&
                item.Description == "Crispy and salty" &&
                item.CategoryName == "Snacks" &&
                item.ImgUrl == "http://example.com/chips.jpg");
        }

        [Fact]
        public async Task GetAllFoodAndDrink_ShouldReturnEmptyList_WhenNoItemsExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // unique name for each test
                 .Options;

            var emptyContext = new GreenGardenContext(options);
            emptyContext.Database.EnsureCreated();

            FoodAndDrinkDAO.InitializeContext(emptyContext);

            // Act
            var result = FoodAndDrinkDAO.GetAllFoodAndDrink();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllFoodAndDrink_ShouldThrowException_WhenRepositoryThrowsException()
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
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(FoodAndDrinkDAO.GetAllFoodAndDrink()));
        }

        // Test for method GetAllCustomerFoodAndDrink
        [Fact]
        public async Task GetAllCustomerFoodAndDrink_ShouldReturnActiveItemsOnly()
        {
            // Arrange
            var dbContext = await GetDbContext();
            FoodAndDrinkDAO.InitializeContext(dbContext);

            // Act
            var result = FoodAndDrinkDAO.GetAllCustomerFoodAndDrink();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2); // Only active items should be returned

            result.Should().Contain(item =>
                item.ItemId == 1 &&
                item.ItemName == "Coca-Cola" &&
                item.Price == 1.5m &&
                item.Description == "Refreshing beverage" &&
                item.CategoryName == "Beverages" &&
                item.ImgUrl == "http://example.com/coke.jpg");

            result.Should().Contain(item =>
                item.ItemId == 2 &&
                item.ItemName == "Potato Chips" &&
                item.Price == 2.0m &&
                item.Description == "Crispy and salty" &&
                item.CategoryName == "Snacks" &&
                item.ImgUrl == "http://example.com/chips.jpg");

            result.Should().NotContain(item => item.ItemId == 3); // Inactive item should not be in the result
        }

        [Fact]
        public async Task GetAllCustomerFoodAndDrink_ShouldReturnEmptyList_WhenNoActiveItemsExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // unique name for each test
                 .Options;

            var emptyContext = new GreenGardenContext(options);
            emptyContext.Database.EnsureCreated();

            FoodAndDrinkDAO.InitializeContext(emptyContext);

            // Act
            var result = FoodAndDrinkDAO.GetAllCustomerFoodAndDrink();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllCustomerFoodAndDrink_ShouldThrowException_WhenRepositoryThrowsException()
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
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(FoodAndDrinkDAO.GetAllCustomerFoodAndDrink()));
        }

        // Test for method GetFoodAndDrinkDetail
        [Fact]
        public async Task GetFoodAndDrinkDetail_ShouldReturnItem_WhenItemExists()
        {
            // Arrange
            var dbContext = await GetDbContext();
            FoodAndDrinkDAO.InitializeContext(dbContext);

            // Act
            var result = FoodAndDrinkDAO.GetFoodAndDrinkDetail(1); // Existing ItemId

            // Assert
            result.Should().NotBeNull();
            result.ItemId.Should().Be(1);
            result.ItemName.Should().Be("Coca-Cola");
            result.Price.Should().Be(1.5m);
            result.Description.Should().Be("Refreshing beverage");
            result.CategoryName.Should().Be("Beverages");
            result.ImgUrl.Should().Be("http://example.com/coke.jpg");
        }

        [Fact]
        public async Task GetFoodAndDrinkDetail_ShouldReturnNull_WhenItemDoesNotExist()
        {
            // Arrange
            var dbContext = await GetDbContext();
            FoodAndDrinkDAO.InitializeContext(dbContext);

            // Act
            var result = FoodAndDrinkDAO.GetFoodAndDrinkDetail(999); // Non-existent ItemId

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetFoodAndDrinkDetail_ShouldThrowException_WhenRepositoryThrowsException()
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
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(FoodAndDrinkDAO.GetFoodAndDrinkDetail(1)));
        }

        // Test for method GetFoodAndDrinkDetail
        private async Task<GreenGardenContext> GetDbContextWithCategories()
        {
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique name for each test
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated();

            // Seed mock data
            if (!await dbContext.FoodAndDrinkCategories.AnyAsync())
            {
                dbContext.FoodAndDrinkCategories.AddRange(
                    new FoodAndDrinkCategory
                    {
                        CategoryId = 1,
                        CategoryName = "Beverages",
                        Description = "Drinks and refreshing beverages",
                        CreatedAt = DateTime.UtcNow
                    },
                    new FoodAndDrinkCategory
                    {
                        CategoryId = 2,
                        CategoryName = "Snacks",
                        Description = "Crispy and crunchy snacks",
                        CreatedAt = DateTime.UtcNow
                    });

                await dbContext.SaveChangesAsync();
            }

            return dbContext;
        }

        [Fact]
        public async Task GetAllFoodAndDrinkCategories_ShouldReturnCategories_WhenCategoriesExist()
        {
            // Arrange
            var dbContext = await GetDbContextWithCategories();
            FoodAndDrinkDAO.InitializeContext(dbContext);

            // Act
            var result = FoodAndDrinkDAO.GetAllFoodAndDrinkCategories();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);  // Ensure there are two categories
            result[0].CategoryId.Should().Be(1);
            result[0].CategoryName.Should().Be("Beverages");
            result[1].CategoryId.Should().Be(2);
            result[1].CategoryName.Should().Be("Snacks");
        }

        [Fact]
        public async Task GetAllFoodAndDrinkCategories_ShouldReturnEmpty_WhenNoCategoriesExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated();

            FoodAndDrinkDAO.InitializeContext(dbContext);

            // Act
            var result = FoodAndDrinkDAO.GetAllFoodAndDrinkCategories();

            // Assert
            result.Should().BeEmpty();  // No categories should be returned
        }

        [Fact]
        public async Task GetAllFoodAndDrinkCategories_ShouldThrowException_WhenRepositoryThrowsException()
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
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(FoodAndDrinkDAO.GetAllFoodAndDrinkCategories()));
        }


    }
}
