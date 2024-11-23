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
            FoodAndDrinkDAO.InitializeContext(null); // Provide null context to force an exception

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(FoodAndDrinkDAO.GetAllFoodAndDrink()));
            exception.Message.Should().Be("Object reference not set to an instance of an object.");
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
            FoodAndDrinkDAO.InitializeContext(null); // Provide null context to force an exception

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(FoodAndDrinkDAO.GetAllCustomerFoodAndDrink()));
            exception.Message.Should().Be("Object reference not set to an instance of an object.");
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
            FoodAndDrinkDAO.InitializeContext(null); // Provide null context to force an exception

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(FoodAndDrinkDAO.GetFoodAndDrinkDetail(1)));
            exception.Message.Should().Be("Object reference not set to an instance of an object.");
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
            FoodAndDrinkDAO.InitializeContext(null); // Provide null context to force an exception

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(FoodAndDrinkDAO.GetAllFoodAndDrinkCategories()));
            exception.Message.Should().Be("Object reference not set to an instance of an object.");
        }

        // Test for method GetFoodAndDrinks
        private async Task<GreenGardenContext> GetDbContext2() // Create a database in memory with mock data.
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
                var category3 = new FoodAndDrinkCategory { CategoryId = 3, CategoryName = "Desserts" };

                databaseContext.FoodAndDrinkCategories.AddRange(category1, category2, category3);

                databaseContext.FoodAndDrinks.AddRange(
                    new FoodAndDrink
                    {
                        Status = true,
                        ItemId = 1,
                        ItemName = "Coca-Cola",
                        Price = 1.5m, // Price below 300,000
                        Description = "Refreshing beverage",
                        Category = category1,
                        ImgUrl = "http://example.com/coke.jpg"
                    },
                    new FoodAndDrink
                    {
                        Status = true,
                        ItemId = 2,
                        ItemName = "Potato Chips",
                        Price = 2.0m, // Price below 300,000
                        Description = "Crispy and salty",
                        Category = category2,
                        ImgUrl = "http://example.com/chips.jpg"
                    },
                    new FoodAndDrink
                    {
                        Status = true,
                        ItemId = 3,
                        ItemName = "Premium Coffee",
                        Price = 350000m, // Price between 300,000 and 500,000
                        Description = "Smooth and strong coffee",
                        Category = category3,
                        ImgUrl = "http://example.com/coffee.jpg"
                    }
                );

                await databaseContext.SaveChangesAsync();
            }

            return databaseContext;
        }

        [Fact]
        public async Task GetFoodAndDrinks_ShouldReturnAllItems_WhenNoFilterOrSort()
        {
            // Arrange
            var dbContext = await GetDbContext2();
            FoodAndDrinkDAO.InitializeContext(dbContext);

            // Act
            var result =  FoodAndDrinkDAO.GetFoodAndDrinks(null, null, null);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);  // All active items should be returned
        }

        [Fact]
        public async Task GetFoodAndDrinks_ShouldReturnCategory1Items_WhenCategoryIdIs1()
        {
            // Arrange
            var dbContext = await GetDbContext2();
            FoodAndDrinkDAO.InitializeContext(dbContext);

            // Act
            var result = FoodAndDrinkDAO.GetFoodAndDrinks(1, null, null);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);  // Only 1 item in category 1
            Assert.Equal("Coca-Cola", result.First().ItemName);
        }

        [Fact]
        public async Task GetFoodAndDrinks_ShouldReturnItemsUnder300K_WhenPriceRangeIsUnder300K()
        {
            // Arrange
            var dbContext = await GetDbContext2();
            FoodAndDrinkDAO.InitializeContext(dbContext);

            // Act
            var result = FoodAndDrinkDAO.GetFoodAndDrinks(null, null, 1);  // Price range 1 (< 300K)

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);  // 2 items priced under 300,000
        }

        [Fact]
        public async Task GetFoodAndDrinks_ShouldReturnItems300Kto500K_WhenPriceRangeIs300Kto500K()
        {
            // Arrange
            var dbContext = await GetDbContext2();
            FoodAndDrinkDAO.InitializeContext(dbContext);

            // Act
            var result = FoodAndDrinkDAO.GetFoodAndDrinks(null, null, 2);  // Price range 2 (300K to 500K)

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count);  // No items in this price range
        }

        [Fact]
        public async Task GetFoodAndDrinks_ShouldReturnItemsOver500K_WhenPriceRangeIsOver500K()
        {
            // Arrange
            var dbContext = await GetDbContext2();
            FoodAndDrinkDAO.InitializeContext(dbContext);

            // Act
            var result = FoodAndDrinkDAO.GetFoodAndDrinks(null, null, 3);  // Price range 3 (> 500K)

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Count);  // No items over 500,000
        }

        [Fact]
        public async Task GetFoodAndDrinks_ShouldReturnItemsSortedByPriceAsc_WhenSortedByPriceAsc()
        {
            // Arrange
            var dbContext = await GetDbContext2();
            FoodAndDrinkDAO.InitializeContext(dbContext);

            // Act
            var result = FoodAndDrinkDAO.GetFoodAndDrinks(null, 1, null);  // Sort by price ascending

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal("Coca-Cola", result.First().ItemName);  // Food1 should come first (lowest price)
        }

        [Fact]
        public async Task GetFoodAndDrinks_ShouldReturnItemsSortedByPriceDesc_WhenSortedByPriceDesc()
        {
            // Arrange
            var dbContext = await GetDbContext2();
            FoodAndDrinkDAO.InitializeContext(dbContext);

            // Act
            var result = FoodAndDrinkDAO.GetFoodAndDrinks(null, 2, null);  // Sort by price descending

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal("Premium Coffee", result.First().ItemName);  // Premium Coffee should be the highest priced
        }

        [Fact]
        public async Task GetFoodAndDrinks_ShouldReturnItemsSortedByNameAsc_WhenSortedByNameAsc()
        {
            // Arrange
            var dbContext = await GetDbContext2();
            FoodAndDrinkDAO.InitializeContext(dbContext);

            // Act
            var result = FoodAndDrinkDAO.GetFoodAndDrinks(null, 3, null);  // Sort by name ascending

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal("Coca-Cola", result.First().ItemName);  // Coca-Cola should come first alphabetically
        }

        [Fact]
        public async Task GetFoodAndDrinks_ShouldReturnItemsSortedByNameDesc_WhenSortedByNameDesc()
        {
            // Arrange
            var dbContext = await GetDbContext2();
            FoodAndDrinkDAO.InitializeContext(dbContext);

            // Act
            var result = FoodAndDrinkDAO.GetFoodAndDrinks(null, 4, null);  // Sort by name descending

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal("Premium Coffee", result.First().ItemName);  // Premium Coffee should come first alphabetically (Z-A)
        }

        [Fact]
        public async Task GetFoodAndDrinks_ShouldReturnItemsSortedByCreationDate_WhenSortedByCreationDate()
        {
            // Arrange
            var dbContext = await GetDbContext2();
            FoodAndDrinkDAO.InitializeContext(dbContext);

            // Act
            var result = FoodAndDrinkDAO.GetFoodAndDrinks(null, 5, null);  // Sort by creation date

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetFoodAndDrinks_ShouldReturnItemsSortedByQuantityDesc_WhenSortedByQuantityDesc()
        {
            // Arrange
            var dbContext = await GetDbContext2();
            FoodAndDrinkDAO.InitializeContext(dbContext);

            // Act
            var result = FoodAndDrinkDAO.GetFoodAndDrinks(null, 6, null);  // Sort by quantity descending

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetFoodAndDrinks_ShouldReturnCategory1ItemsUnder300K_WhenCategoryIs1AndPriceRangeIsUnder300K()
        {
            // Arrange
            var dbContext = await GetDbContext2();
            FoodAndDrinkDAO.InitializeContext(dbContext);

            // Act
            var result = FoodAndDrinkDAO.GetFoodAndDrinks(1, null, 1);  // Category 1 and price range under 300K

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count);  // Only 1 item in category 1 and price under 300K
        }

        [Fact]
        public async Task GetFoodAndDrinks_ShouldReturnCategory1SortedByPriceAsc_WhenCategoryIs1AndSortedByPriceAsc()
        {
            // Arrange
            var dbContext = await GetDbContext2();
            FoodAndDrinkDAO.InitializeContext(dbContext);

            // Act
            var result = FoodAndDrinkDAO.GetFoodAndDrinks(1, 1, null);  // Category 1 and sorted by price ascending

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
            Assert.Equal("Coca-Cola", result.First().ItemName);  // The lowest price item in category 1
        }

        [Fact]
        public async Task GetFoodAndDrinks_ShouldReturnAllItems_WhenInvalidPriceRangeAndSortBy()
        {
            // Arrange
            var dbContext = await GetDbContext2();
            FoodAndDrinkDAO.InitializeContext(dbContext);

            // Act
            var result = FoodAndDrinkDAO.GetFoodAndDrinks(null, 999, 999);  // Invalid price range and sort value

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);  // All items should be returned as filters are invalid
        }

        [Fact]
        public async Task GetFoodAndDrinks_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new GreenGardenContext(options);
            databaseContext.Database.EnsureCreated();

            // Simulate an exception scenario by setting the context to null or failing in some way
            FoodAndDrinkDAO.InitializeContext(null); // Provide null context to force an exception

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(FoodAndDrinkDAO.GetFoodAndDrinks(1, 1, 1)));
            exception.Message.Should().Be("Object reference not set to an instance of an object.");
        }

        [Fact]
        public async Task GetFoodAndDrinks_ShouldReturnEmpty_WhenCategoryHasNoItems()
        {
            // Arrange
            var dbContext = await GetDbContext2();
            FoodAndDrinkDAO.InitializeContext(dbContext);

            // Act
            var result = FoodAndDrinkDAO.GetFoodAndDrinks(999, null, null);  // Non-existent category

            // Assert
            Assert.Empty(result);  // No items for non-existent category
        }

        // Test for method AddFoodAndDrink
        [Fact]
        public async Task AddFoodAndDrink_ShouldAddItem_WhenInputIsValid()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);
            FoodAndDrinkDAO.InitializeContext(dbContext);

            var itemDto = new AddFoodOrDrinkDTO
            {
                ItemId = 1,
                ItemName = "Pizza",
                Price = 12.50m,
                Description = "Delicious cheesy pizza",
                ImgUrl = "http://example.com/pizza.jpg",
                CategoryId = 2
            };

            // Act
            FoodAndDrinkDAO.AddFoodAndDrink(itemDto);

            // Assert
            var addedItem = dbContext.FoodAndDrinks.FirstOrDefault(f => f.ItemId == itemDto.ItemId);
            Assert.NotNull(addedItem);
            Assert.Equal(itemDto.ItemName, addedItem.ItemName);
            Assert.Equal(itemDto.Price, addedItem.Price);
            Assert.Equal(itemDto.Description, addedItem.Description);
            Assert.Equal(itemDto.ImgUrl, addedItem.ImgUrl);
            Assert.Equal(itemDto.CategoryId, addedItem.CategoryId);
            Assert.True(addedItem.Status);
        }

        [Fact]
        public async Task AddFoodAndDrink_ShouldThrowException_WhenItemNameIsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);
            FoodAndDrinkDAO.InitializeContext(dbContext);

            var itemDto = new AddFoodOrDrinkDTO
            {
                ItemId = 1,
                ItemName = null!, // Invalid input
                Price = 10.00m,
                Description = "Tasty burger",
                ImgUrl = "http://example.com/burger.jpg",
                CategoryId = 3
            };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => FoodAndDrinkDAO.AddFoodAndDrink(itemDto));
            Assert.Contains("Object reference not set", exception.Message);
        }

        [Fact]
        public async Task AddFoodAndDrink_ShouldThrowException_WhenContextIsNull()
        {
            // Arrange
            FoodAndDrinkDAO.InitializeContext(null); // Null context

            var itemDto = new AddFoodOrDrinkDTO
            {
                ItemId = 1,
                ItemName = "Burger",
                Price = 8.99m,
                Description = "Juicy burger",
                ImgUrl = "http://example.com/burger.jpg",
                CategoryId = 1
            };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => FoodAndDrinkDAO.AddFoodAndDrink(itemDto));
            Assert.Contains("Object reference not set", exception.Message);
        }

        [Fact]
        public async Task AddFoodAndDrink_ShouldAddItemWithNullOptionalFields()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);
            FoodAndDrinkDAO.InitializeContext(dbContext);

            var itemDto = new AddFoodOrDrinkDTO
            {
                ItemId = 2,
                ItemName = "Sandwich",
                Price = 5.00m,
                Description = null, // Optional field
                ImgUrl = null, // Optional field
                CategoryId = null // Optional field
            };

            // Act
            FoodAndDrinkDAO.AddFoodAndDrink(itemDto);

            // Assert
            var addedItem = dbContext.FoodAndDrinks.FirstOrDefault(f => f.ItemId == itemDto.ItemId);
            Assert.NotNull(addedItem);
            Assert.Equal(itemDto.ItemName, addedItem.ItemName);
            Assert.Equal(itemDto.Price, addedItem.Price);
            Assert.Null(addedItem.Description); // Ensure optional field is null
            Assert.Null(addedItem.ImgUrl); // Ensure optional field is null
            Assert.Null(addedItem.CategoryId); // Ensure optional field is null
            Assert.True(addedItem.Status);
        }

        [Fact]
        public async Task AddFoodAndDrink_ShouldThrowException_WhenPriceIsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);
            FoodAndDrinkDAO.InitializeContext(dbContext);

            var itemDto = new AddFoodOrDrinkDTO
            {
                ItemId = 1,
                ItemName = "Juice",
                Price = 0, // Invalid price
                Description = "Fresh orange juice",
                ImgUrl = "http://example.com/juice.jpg",
                CategoryId = 2
            };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => FoodAndDrinkDAO.AddFoodAndDrink(itemDto));
            Assert.Contains("The field Price must be a positive value", exception.Message);
        }

        // Test for method UpdateFoodOrDrink
        [Fact]
        public async Task UpdateFoodOrDrink_ShouldUpdateSuccessfully_WhenItemExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);
            FoodAndDrinkDAO.InitializeContext(dbContext);

            var existingItem = new FoodAndDrink
            {
                ItemId = 1,
                ItemName = "Original Burger",
                Price = 5.99m,
                Description = "Classic beef burger",
                CategoryId = 1,
                ImgUrl = "http://example.com/original.jpg"
            };
            dbContext.FoodAndDrinks.Add(existingItem);
            await dbContext.SaveChangesAsync();

            var updateDto = new UpdateFoodOrDrinkDTO
            {
                ItemId = 1,
                ItemName = "Updated Burger",
                Price = 6.99m,
                Description = "Updated description",
                CategoryId = 2,
                ImgUrl = "http://example.com/updated.jpg"
            };

            // Act
            FoodAndDrinkDAO.UpdateFoodOrDrink(updateDto);

            // Assert
            var updatedItem = dbContext.FoodAndDrinks.FirstOrDefault(f => f.ItemId == 1);
            Assert.NotNull(updatedItem);
            Assert.Equal("Updated Burger", updatedItem.ItemName);
            Assert.Equal(6.99m, updatedItem.Price);
            Assert.Equal("Updated description", updatedItem.Description);
            Assert.Equal(2, updatedItem.CategoryId);
            Assert.Equal("http://example.com/updated.jpg", updatedItem.ImgUrl);
        }

        [Fact]
        public async Task UpdateFoodOrDrink_ShouldThrowException_WhenContextIsNull()
        {
            // Arrange
            FoodAndDrinkDAO.InitializeContext(null); // Null context

            var updateDto = new UpdateFoodOrDrinkDTO
            {
                ItemId = 1,
                ItemName = "Updated Burger",
                Price = 6.99m,
                Description = "Updated description",
                CategoryId = 2,
                ImgUrl = "http://example.com/updated.jpg"
            };


            // Act & Assert
            var exception = Assert.Throws<Exception>(() => FoodAndDrinkDAO.UpdateFoodOrDrink(updateDto));
            Assert.Contains("Object reference not set", exception.Message);
        }

        [Fact]
        public void UpdateFoodOrDrink_ShouldThrowException_WhenItemDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);
            FoodAndDrinkDAO.InitializeContext(dbContext);

            var updateDto = new UpdateFoodOrDrinkDTO
            {
                ItemId = 999, // Non-existing item ID
                ItemName = "Non-Existent Item",
                Price = 10.99m,
                Description = "This item does not exist",
                CategoryId = 1,
                ImgUrl = "http://example.com/nonexistent.jpg"
            };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => FoodAndDrinkDAO.UpdateFoodOrDrink(updateDto));
            Assert.Equal("Food and Drink with ID 999 does not exist.", exception.Message);
        }

        [Fact]
        public void UpdateFoodOrDrink_ShouldThrowException_WhenPriceIsNegative()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);
            FoodAndDrinkDAO.InitializeContext(dbContext);

            var existingItem = new FoodAndDrink
            {
                ItemId = 1,
                ItemName = "Original Burger",
                Price = 5.99m,
                Description = "Classic beef burger",
                CategoryId = 1,
                ImgUrl = "http://example.com/original.jpg"
            };
            dbContext.FoodAndDrinks.Add(existingItem);
            dbContext.SaveChanges();

            var updateDto = new UpdateFoodOrDrinkDTO
            {
                ItemId = 1,
                ItemName = "Updated Burger",
                Price = -1.99m, // Invalid price
                Description = "Updated description",
                CategoryId = 2,
                ImgUrl = "http://example.com/updated.jpg"
            };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => FoodAndDrinkDAO.UpdateFoodOrDrink(updateDto));
            Assert.Contains("Price must be greater than or equal to zero", exception.Message); // Adjust based on actual validation message
        }

        // Test for method ChangeFoodStatus
        [Fact]
        public async Task ChangeFoodStatus_ShouldToggleStatus_WhenItemExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);
            FoodAndDrinkDAO.InitializeContext(dbContext);

            var existingItem = new FoodAndDrink
            {
                ItemId = 1,
                ItemName = "Sample Food Item",
                Status = true, // Initial status
                Price = 10.99m
            };
            dbContext.FoodAndDrinks.Add(existingItem);
            await dbContext.SaveChangesAsync();

            // Act
            FoodAndDrinkDAO.ChangeFoodStatus(existingItem.ItemId);

            // Assert
            var updatedItem = dbContext.FoodAndDrinks.FirstOrDefault(f => f.ItemId == existingItem.ItemId);
            Assert.NotNull(updatedItem);
            Assert.False(updatedItem.Status); // Ensure the status is toggled to false

            // Act (toggle back)
            FoodAndDrinkDAO.ChangeFoodStatus(existingItem.ItemId);

            // Assert (after second toggle)
            updatedItem = dbContext.FoodAndDrinks.FirstOrDefault(f => f.ItemId == existingItem.ItemId);
            Assert.NotNull(updatedItem);
            Assert.True(updatedItem.Status); // Ensure the status is toggled back to true
        }

        [Fact]
        public void ChangeFoodStatus_ShouldThrowException_WhenItemDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);
            FoodAndDrinkDAO.InitializeContext(dbContext);

            var nonExistentItemId = 999; // Non-existent item ID

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => FoodAndDrinkDAO.ChangeFoodStatus(nonExistentItemId));
            Assert.Equal($"Food and Drink with ID {nonExistentItemId} does not exist.", exception.Message);
        }

        [Fact]
        public void ChangeFoodStatus_ShouldThrowException_WhenContextIsNull()
        {
            // Arrange
            FoodAndDrinkDAO.InitializeContext(null); // Nullify the context

            var itemId = 1; // Random item ID for testing

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => FoodAndDrinkDAO.ChangeFoodStatus(itemId));
            Assert.Equal("Object reference not set to an instance of an object.", exception.Message);
        }


    }
}
