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
    public class CampingGearDAOTest
    {
        private async Task<GreenGardenContext> GetDbContextWithMockData()
        {
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique name for each test
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated();

            if (!await dbContext.CampingGears.AnyAsync())
            {
                // Add Gear Categories
                dbContext.CampingCategories.AddRange(
                    new CampingCategory { GearCategoryId = 1, GearCategoryName = "Tents" },
                    new CampingCategory { GearCategoryId = 2, GearCategoryName = "Sleeping Bags" }
                );
                // Add Camping Gears
                dbContext.CampingGears.AddRange(
                    new CampingGear
                    {
                        GearId = 1,
                        GearName = "4-Person Tent",
                        QuantityAvailable = 10,
                        RentalPrice = 50.00m,
                        Description = "Spacious 4-person tent.",
                        CreatedAt = DateTime.UtcNow,
                        GearCategoryId = 1,
                        ImgUrl = "http://example.com/tent.jpg",
                        Status = true
                    },
                    new CampingGear
                    {
                        GearId = 2,
                        GearName = "Sleeping Bag",
                        QuantityAvailable = 20,
                        RentalPrice = 15.00m,
                        Description = "Warm sleeping bag.",
                        CreatedAt = DateTime.UtcNow,
                        GearCategoryId = 2,
                        ImgUrl = "http://example.com/sleepingbag.jpg",
                        Status = true
                    }
                );

                await dbContext.SaveChangesAsync();
            }

            return dbContext;
        }

        // Test for method GetAllCampingGears
        [Fact]
        public async Task GetAllCampingGears_ShouldReturnAllGears()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData();
            CampingGearDAO.InitializeContext(dbContext); // Initialize DAO context

            // Act
            var campingGears = CampingGearDAO.GetAllCampingGears();

            // Assert
            Assert.NotNull(campingGears);
            Assert.Equal(2, campingGears.Count); // Expecting 2 camping gears

            var tent = campingGears.FirstOrDefault(g => g.GearName == "4-Person Tent");
            Assert.NotNull(tent);
            Assert.Equal(1, tent.GearCategoryId);
            Assert.Equal("Tents", tent.GearCategoryName);

            var sleepingBag = campingGears.FirstOrDefault(g => g.GearName == "Sleeping Bag");
            Assert.NotNull(sleepingBag);
            Assert.Equal(2, sleepingBag.GearCategoryId);
            Assert.Equal("Sleeping Bags", sleepingBag.GearCategoryName);
        }

        [Fact]
        public async Task GetAllCampingGears_ShouldReturnEmptyList_WhenNoCampingGearsExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated();
            CampingGearDAO.InitializeContext(dbContext); // Initialize DAO context

            // Act
            var campingGears = CampingGearDAO.GetAllCampingGears();

            // Assert
            Assert.NotNull(campingGears);
            Assert.Empty(campingGears); // Expecting an empty list
        }

        [Fact]
        public async Task GetAllCampingGears_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new GreenGardenContext(options);
            databaseContext.Database.EnsureCreated();

            // Simulate an exception scenario by setting the context to null
            CampingGearDAO.InitializeContext(null); // Provide null context to force an exception

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(CampingGearDAO.GetAllCampingGears()));

        }

        // Test for method GetAllCustomerCampingGears
        private async Task<GreenGardenContext> GetDbContext2()
        {
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique name for each test
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated();

            if (!await dbContext.CampingGears.AnyAsync())
            {
                // Add Gear Categories
                dbContext.CampingCategories.AddRange(
                    new CampingCategory { GearCategoryId = 1, GearCategoryName = "Tents" },
                    new CampingCategory { GearCategoryId = 2, GearCategoryName = "Sleeping Bags" }
                );

                // Add Camping Gears
                dbContext.CampingGears.AddRange(
                    new CampingGear
                    {
                        GearId = 1,
                        GearName = "4-Person Tent",
                        QuantityAvailable = 10,
                        RentalPrice = 50.00m,
                        Description = "Spacious 4-person tent.",
                        CreatedAt = DateTime.UtcNow,
                        GearCategoryId = 1,
                        ImgUrl = "http://example.com/tent.jpg",
                        Status = true // Visible to customers
                    },
                    new CampingGear
                    {
                        GearId = 2,
                        GearName = "Sleeping Bag",
                        QuantityAvailable = 20,
                        RentalPrice = 15.00m,
                        Description = "Warm sleeping bag.",
                        CreatedAt = DateTime.UtcNow,
                        GearCategoryId = 2,
                        ImgUrl = "http://example.com/sleepingbag.jpg",
                        Status = false // Not visible to customers
                    }
                );

                await dbContext.SaveChangesAsync();
            }

            return dbContext;
        }
        [Fact]
        public async Task GetAllCustomerCampingGears_ShouldReturnOnlyActiveGears()
        {
            // Arrange
            var dbContext = await GetDbContext2();
            CampingGearDAO.InitializeContext(dbContext); // Initialize DAO context

            // Act
            var customerCampingGears = CampingGearDAO.GetAllCustomerCampingGears();

            // Assert
            Assert.NotNull(customerCampingGears);
            Assert.Single(customerCampingGears); // Expecting only 1 active gear

            var tent = customerCampingGears.FirstOrDefault(g => g.GearName == "4-Person Tent");
            Assert.NotNull(tent);
            Assert.Equal("Tents", tent.GearCategoryName);

            var sleepingBag = customerCampingGears.FirstOrDefault(g => g.GearName == "Sleeping Bag");
            Assert.Null(sleepingBag); // Sleeping bag has `Status == false`
        }

        [Fact]
        public async Task GetAllCustomerCampingGears_ShouldReturnEmptyList_WhenNoActiveGearsExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique name for this test
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated();

            // Add Camping Gears with `Status == false`
            dbContext.CampingGears.AddRange(
                new CampingGear
                {
                    GearId = 1,
                    GearName = "Inactive Tent",
                    QuantityAvailable = 5,
                    RentalPrice = 40.00m,
                    Description = "An inactive camping gear.",
                    CreatedAt = DateTime.UtcNow,
                    GearCategoryId = 1,
                    Status = false
                }
            );
            await dbContext.SaveChangesAsync();

            CampingGearDAO.InitializeContext(dbContext); // Initialize DAO context

            // Act
            var customerCampingGears = CampingGearDAO.GetAllCustomerCampingGears();

            // Assert
            Assert.NotNull(customerCampingGears);
            Assert.Empty(customerCampingGears); // Expecting no active gears
        }

        [Fact]
        public async Task GetAllCustomerCampingGears_ShouldReturnEmptyList_WhenDatabaseIsEmpty()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated();
            CampingGearDAO.InitializeContext(dbContext); // Initialize DAO context

            // Act
            var customerCampingGears = CampingGearDAO.GetAllCustomerCampingGears();

            // Assert
            Assert.NotNull(customerCampingGears);
            Assert.Empty(customerCampingGears); // Expecting no results in an empty database
        }

        [Fact]
        public async Task GetAllCustomerCampingGears_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new GreenGardenContext(options);
            databaseContext.Database.EnsureCreated();

            // Simulate an exception scenario by setting the context to null
            CampingGearDAO.InitializeContext(null); // Provide null context to force an exception

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(CampingGearDAO.GetAllCustomerCampingGears()));

        }

        // Test for method GetCampingGearDetail
        [Fact]
        public async Task GetCampingGearById_ShouldReturnGear_WhenGearExists()
        {
            // Arrange
            var dbContext = await GetDbContext2();
            CampingGearDAO.InitializeContext(dbContext); // Initialize DAO context

            // Act
            var gear = CampingGearDAO.GetCampingGearDetail(1); // GearId = 1

            // Assert
            Assert.NotNull(gear);
            Assert.Equal(1, gear.GearId);
            Assert.Equal("4-Person Tent", gear.GearName);
            Assert.Equal("Tents", gear.GearCategoryName);
            Assert.True(gear.Status);
        }

        [Fact]
        public async Task GetCampingGearDetail_ShouldReturnNull_WhenGearDoesNotExist()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData();
            CampingGearDAO.InitializeContext(dbContext); // Initialize DAO context

            // Act
            var gear = CampingGearDAO.GetCampingGearDetail(999); // Non-existing GearId

            // Assert
            Assert.Null(gear);
        }

        [Fact]
        public async Task GetCampingGearDetail_ShouldReturnNull_WhenGearIdIsInvalid()
        {
            // Arrange
            var dbContext = await GetDbContextWithMockData();
            CampingGearDAO.InitializeContext(dbContext); // Initialize DAO context

            // Act
            var gear = CampingGearDAO.GetCampingGearDetail(0); // Invalid GearId

            // Assert
            Assert.Null(gear);
        }

        [Fact]
        public async Task GetCampingGearDetail_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new GreenGardenContext(options);
            databaseContext.Database.EnsureCreated();

            // Simulate an exception scenario by setting the context to null
            CampingGearDAO.InitializeContext(null); // Provide null context to force an exception

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(CampingGearDAO.GetCampingGearDetail(1)));

        }

        // Test for method GetAllCampingGearCategories
        private async Task<GreenGardenContext> GetDbContext3()
        {
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique name for each test
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated();

            if (!await dbContext.CampingCategories.AnyAsync())
            {
                dbContext.CampingCategories.AddRange(
                    new CampingCategory
                    {
                        GearCategoryId = 1,
                        GearCategoryName = "Tents",
                        Description = "Different types of tents.",
                        CreatedAt = DateTime.UtcNow.AddDays(-10)
                    },
                    new CampingCategory
                    {
                        GearCategoryId = 2,
                        GearCategoryName = "Sleeping Bags",
                        Description = "Various sleeping bags for rent.",
                        CreatedAt = DateTime.UtcNow.AddDays(-5)
                    }
                );

                await dbContext.SaveChangesAsync();
            }

            return dbContext;
        }

        [Fact]
        public async Task GetAllCampingGearCategories_ShouldReturnAllCategories()
        {
            // Arrange
            var dbContext = await GetDbContext3();
            CampingGearDAO.InitializeContext(dbContext); // Initialize DAO context

            // Act
            var categories = CampingGearDAO.GetAllCampingGearCategories();

            // Assert
            Assert.NotNull(categories);
            Assert.Equal(2, categories.Count); // Two categories exist in mock data

            var tentsCategory = categories.FirstOrDefault(c => c.GearCategoryName == "Tents");
            Assert.NotNull(tentsCategory);
            Assert.Equal("Different types of tents.", tentsCategory.Description);

            var sleepingBagsCategory = categories.FirstOrDefault(c => c.GearCategoryName == "Sleeping Bags");
            Assert.NotNull(sleepingBagsCategory);
            Assert.Equal("Various sleeping bags for rent.", sleepingBagsCategory.Description);
        }

        [Fact]
        public async Task GetAllCampingGearCategories_ShouldReturnEmptyList_WhenNoCategoriesExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated();
            CampingGearDAO.InitializeContext(dbContext); // Initialize DAO context without adding categories

            // Act
            var categories = CampingGearDAO.GetAllCampingGearCategories();

            // Assert
            Assert.NotNull(categories);
            Assert.Empty(categories); // No categories in the database
        }

        [Fact]
        public async Task GetAllCampingGearCategories_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new GreenGardenContext(options);
            databaseContext.Database.EnsureCreated();

            // Simulate an exception scenario by setting the context to null
            CampingGearDAO.InitializeContext(null); // Provide null context to force an exception

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(CampingGearDAO.GetAllCampingGearCategories()));

        }

        // Test for method GetCampingGears -----------------------------------------------------------

        private async Task<GreenGardenContext> GetDbContextWithMockData2()
        {
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated();

            if (!await dbContext.CampingGears.AnyAsync())
            {
                dbContext.CampingCategories.AddRange(
                    new CampingCategory { GearCategoryId = 1, GearCategoryName = "Tents" },
                     new CampingCategory { GearCategoryId = 2, GearCategoryName = "Sleeping Bags" },
                     new CampingCategory { GearCategoryId = 3, GearCategoryName = "Camping Gear" }

                    );

                dbContext.CampingGears.AddRange(
                    new CampingGear
                    {
                        GearId = 1,
                        GearName = "Tent",
                        QuantityAvailable = 10,
                        RentalPrice = 90000,
                        Description = "A basic camping tent",
                        CreatedAt = DateTime.UtcNow.AddDays(-5),
                        GearCategoryId = 1,
                        Status = true,

                    },
                    new CampingGear
                    {
                        GearId = 2,
                        GearName = "Bent",
                        QuantityAvailable = 4,
                        RentalPrice = 240000,
                        Description = "A pre camping tent",
                        CreatedAt = DateTime.UtcNow.AddDays(-4),
                        GearCategoryId = 1,
                        Status = true,

                    },
                    new CampingGear
                    {
                        GearId = 3,
                        GearName = "Sleeping Bag",
                        QuantityAvailable = 5,
                        RentalPrice = 250000,
                        Description = "A comfortable sleeping bag",
                        CreatedAt = DateTime.UtcNow.AddDays(-3),
                        GearCategoryId = 2,
                        Status = true,

                    },
                    new CampingGear
                    {
                        GearId = 4,
                        GearName = "Camping Stove",
                        QuantityAvailable = 0,
                        RentalPrice = 50000,
                        Description = "Portable camping stove",
                        CreatedAt = DateTime.UtcNow.AddDays(-1),
                        GearCategoryId = 3,
                        Status = false,
                    }
                );
                await dbContext.SaveChangesAsync();
            }

            return dbContext;
        }

        // 1. Returns all camping equipment without any sorting or filtering
        [Fact]
        public async Task GetCampingGears_ShouldReturnAllCampingEquipment_WhenNoFiltersApplied()
        {
            var dbContext = await GetDbContextWithMockData2();
            CampingGearDAO.InitializeContext(dbContext);
            var result = CampingGearDAO.GetCampingGears(null, null, null, null);
            Assert.NotNull(result);
            Assert.Equal(3, result.Count); // Corrected count, 4 items were added in mock data with Status = true
        }

        // 2. Returns camping devices belonging to a category with `id = 1`
        [Fact]
        public async Task GetCampingGears_ShouldReturnCampingGearsByCategoryId()
        {
            var dbContext = await GetDbContextWithMockData2();
            CampingGearDAO.InitializeContext(dbContext);
            var result = CampingGearDAO.GetCampingGears(1, null, null, null);
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            
        }

        // 3. Returns camping equipment with rental price under 100,000
        [Fact]
        public async Task GetCampingGears_ShouldReturnCampingGearsUnder100000()
        {
            var dbContext = await GetDbContextWithMockData2();
            CampingGearDAO.InitializeContext(dbContext);
            var result = CampingGearDAO.GetCampingGears(null, null, 1, null);
            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
            
        }

        // 4. Returns camping equipment with rental price from 100,000 to 300,000
        [Fact]
        public async Task GetCampingGears_ShouldReturnCampingGearsBetween100000And300000()
        {
            var dbContext = await GetDbContextWithMockData2();
            CampingGearDAO.InitializeContext(dbContext);
            var result = CampingGearDAO.GetCampingGears(null, null, 2, null);
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        // 5. Returns camping equipment with rental price over 300,000
        [Fact]
        public async Task GetCampingGears_ShouldReturnCampingGearsAbove300000()
        {
            var dbContext = await GetDbContextWithMockData2();
            CampingGearDAO.InitializeContext(dbContext);
            var result = CampingGearDAO.GetCampingGears(null, null, 3, null);
            Assert.Empty(result); // No gear with rental price above 300,000
        }

        // 6. Returns all camping equipment, sorted by rental price from low to high
        [Fact]
        public async Task GetCampingGears_ShouldReturnSortedByPriceLowToHigh()
        {
            var dbContext = await GetDbContextWithMockData2();
            CampingGearDAO.InitializeContext(dbContext);
            var result = CampingGearDAO.GetCampingGears(null, 1, null, null);
            Assert.Equal("Tent", result.FirstOrDefault()?.GearName); // Camping Stove has the lowest price
        }

        // 7. Returns all camping equipment, sorted by rental price from high to low
        [Fact]
        public async Task GetCampingGears_ShouldReturnSortedByPriceHighToLow()
        {
            var dbContext = await GetDbContextWithMockData2();
            CampingGearDAO.InitializeContext(dbContext);
            var result = CampingGearDAO.GetCampingGears(null, 2, null, null);
            Assert.Equal("Sleeping Bag", result.FirstOrDefault()?.GearName); // Sleeping Bag has the higher price
        }

        // 8. Returns all camping equipment, sorted by name A-Z
        [Fact]
        public async Task GetCampingGears_ShouldReturnSortedByNameAZ()
        {
            var dbContext = await GetDbContextWithMockData2();
            CampingGearDAO.InitializeContext(dbContext);
            var result = CampingGearDAO.GetCampingGears(null, 3, null, null);
            Assert.Equal("Bent", result.FirstOrDefault()?.GearName); // A-Z sort
        }

        // 9. Returns all camping equipment, sorted by name Z-A
        [Fact]
        public async Task GetCampingGears_ShouldReturnSortedByNameZA()
        {
            var dbContext = await GetDbContextWithMockData2();
            CampingGearDAO.InitializeContext(dbContext);
            var result = CampingGearDAO.GetCampingGears(null, 4, null, null);
            Assert.Equal("Tent", result.FirstOrDefault()?.GearName); // Z-A sort
        }

        // 10. Returns all camping equipment, sorted by decreasing availability
        [Fact]
        public async Task GetCampingGears_ShouldReturnSortedByAvailabilityDescending()
        {
            var dbContext = await GetDbContextWithMockData2();
            CampingGearDAO.InitializeContext(dbContext);
            var result = CampingGearDAO.GetCampingGears(null, null, null, 1);
            Assert.Equal("Tent", result.FirstOrDefault()?.GearName); // Tent has the most availability
        }

        // 11. Returns all camping equipment, sorted by creation date in descending order
        [Fact]
        public async Task GetCampingGears_ShouldReturnSortedByCreationDateDescending()
        {
            var dbContext = await GetDbContextWithMockData2();
            CampingGearDAO.InitializeContext(dbContext);
            var result = CampingGearDAO.GetCampingGears(null, null, null, 2);
            Assert.Equal("Sleeping Bag", result.FirstOrDefault()?.GearName); // Latest creation date
        }

        // 12. Returns camping equipment that belongs to `categoryId = 1` and has a rental price of less than 100,000
        [Fact]
        public async Task GetCampingGears_ShouldReturnByCategoryAndPriceUnder100000()
        {
            var dbContext = await GetDbContextWithMockData2();
            CampingGearDAO.InitializeContext(dbContext);
            var result = CampingGearDAO.GetCampingGears(1, null, 1, null);
            Assert.Single(result);
            Assert.Equal("Tent", result.FirstOrDefault()?.GearName); // Only one gear under 100,000
        }

        // 13. Returns camping equipment that belongs to `categoryId = 1` and has a rental price from 100,000 to 300,000
        [Fact]
        public async Task GetCampingGears_ShouldReturnByCategoryAndPriceBetween100000And300000()
        {
            var dbContext = await GetDbContextWithMockData2();
            CampingGearDAO.InitializeContext(dbContext);
            var result = CampingGearDAO.GetCampingGears(1, null, 2, null);
            Assert.Single(result);
            Assert.Equal("Bent", result.FirstOrDefault()?.GearName);
        }

        // 14. Returns camping equipment that belongs to `categoryId = 1` and has a rental price over 300,000
        [Fact]
        public async Task GetCampingGears_ShouldReturnByCategoryAndPriceAbove300000()
        {
            var dbContext = await GetDbContextWithMockData2();
            CampingGearDAO.InitializeContext(dbContext);
            var result = CampingGearDAO.GetCampingGears(1, null, 3, null);
            Assert.Empty(result); // No gear with rental price above 300,000
        }

        // 15. Returns camping equipment in the `categoryId = 1` and sorted by rental price from low to high
        [Fact]
        public async Task GetCampingGears_ShouldReturnByCategoryAndSortedByPriceLowToHigh()
        {
            var dbContext = await GetDbContextWithMockData2();
            CampingGearDAO.InitializeContext(dbContext);
            var result = CampingGearDAO.GetCampingGears(1, 1, null, null);
            Assert.Equal("Tent", result.FirstOrDefault()?.GearName);
        }

        // 16. Returns camping equipment in the `categoryId = 1` and sorted by rental price from high to low
        [Fact]
        public async Task GetCampingGears_ShouldReturnByCategoryAndSortedByPriceHighToLow()
        {
            var dbContext = await GetDbContextWithMockData2();
            CampingGearDAO.InitializeContext(dbContext);
            var result = CampingGearDAO.GetCampingGears(1, 2, null, null);
            Assert.Equal("Bent", result.FirstOrDefault()?.GearName);
        }

        // 17. Returns camping equipment in the `categoryId = 1` and sorted by name A-Z
        [Fact]
        public async Task GetCampingGears_ShouldReturnByCategoryAndSortedByNameAZ()
        {
            var dbContext = await GetDbContextWithMockData2();
            CampingGearDAO.InitializeContext(dbContext);
            var result = CampingGearDAO.GetCampingGears(1, 3, null, null);
            Assert.Equal("Bent", result.FirstOrDefault()?.GearName); // A-Z sort
        }

        // 18. Returns camping equipment in the `categoryId = 1` and sorted by name Z-A
        [Fact]
        public async Task GetCampingGears_ShouldReturnByCategoryAndSortedByNameZA()
        {
            var dbContext = await GetDbContextWithMockData2();
            CampingGearDAO.InitializeContext(dbContext);
            var result = CampingGearDAO.GetCampingGears(1, 4, null, null);
            Assert.Equal("Tent", result.FirstOrDefault()?.GearName); // Z-A sort
        }

        // 19. Returns all camping equipment in the `categoryId = 1`, sorted by decreasing availability
        [Fact]
        public async Task GetCampingGears_ShouldReturnByCategoryAndSortedByAvailabilityDescending()
        {
            var dbContext = await GetDbContextWithMockData2();
            CampingGearDAO.InitializeContext(dbContext);
            var result = CampingGearDAO.GetCampingGears(1, null, null, 1);
            Assert.Equal("Tent", result.FirstOrDefault()?.GearName);
        }

        // 20. Returns all camping equipment in the `categoryId = 1`, sorted by creation date in descending order
        [Fact]
        public async Task GetCampingGears_ShouldReturnByCategoryAndSortedByCreationDateDescending()
        {
            var dbContext = await GetDbContextWithMockData2();
            CampingGearDAO.InitializeContext(dbContext);
            var result = CampingGearDAO.GetCampingGears(1, null, null, 2);
            Assert.Equal("Bent", result.FirstOrDefault()?.GearName); // Most recent
        }

        // 21. Returns camping equipment in the `category GearCategoryId = 1`, priced under 100,000 and sorted by available quantity in descending order
        [Fact]
        public async Task GetCampingGears_ShouldReturnByCategoryPriceUnder100000AndSortedByAvailability()
        {
            var dbContext = await GetDbContextWithMockData2();
            CampingGearDAO.InitializeContext(dbContext);
            var result = CampingGearDAO.GetCampingGears(1, null, 1, 1);
            Assert.Equal("Tent", result.FirstOrDefault()?.GearName);
        }

        // 22. Returns camping equipment with rental price from 100,000 to 300,000 and sorted by latest creation date
        [Fact]
        public async Task GetCampingGears_ShouldReturnByPriceRangeAndSortedByCreationDate()
        {
            var dbContext = await GetDbContextWithMockData2();
            CampingGearDAO.InitializeContext(dbContext);
            var result = CampingGearDAO.GetCampingGears(null, null, 2, 2);
            Assert.Equal("Sleeping Bag", result.FirstOrDefault()?.GearName);
        }

        // 23. Returns an empty list because there are no devices in this category
        [Fact]
        public async Task GetCampingGears_ShouldReturnEmptyList_WhenCategoryHasNoGears()
        {
            var dbContext = await GetDbContextWithMockData2();
            CampingGearDAO.InitializeContext(dbContext);
            var result = CampingGearDAO.GetCampingGears(99, null, null, null); // Invalid category
            Assert.Empty(result);
        }

        // 24. Returns all camping devices without applying any filtering or sorting because the `sortBy`, `priceRange`, and `popularity` values are invalid
        [Fact]
        public async Task GetCampingGears_ShouldReturnEmptyList_WhenInvalidParametersProvided()
        {
            var dbContext = await GetDbContextWithMockData2();
            CampingGearDAO.InitializeContext(dbContext);
            var result = CampingGearDAO.GetCampingGears(null, 99, 99, 99); 
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }

        // 25. Returns an empty list when database is empty
        [Fact]
        public async Task GetCampingGears_ShouldReturnEmptyList_WhenDatabaseEmpty()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new GreenGardenContext(options);
            dbContext.Database.EnsureCreated();
            CampingGearDAO.InitializeContext(dbContext); // Initialize DAO context

            // Act
            var campingGears = CampingGearDAO.GetCampingGears(null, null, null, null);

            // Assert
            Assert.NotNull(campingGears);
            Assert.Empty(campingGears); // Expecting an empty list
        }

        // 26. NullReferenceException
        [Fact]
        public async Task GetCampingGears_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new GreenGardenContext(options);
            databaseContext.Database.EnsureCreated();

            // Simulate an exception scenario by setting the context to null
            CampingGearDAO.InitializeContext(null); // Provide null context to force an exception

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => Task.FromResult(CampingGearDAO.GetCampingGears(1, null, null, null)));

        }

        // Test for method ChangeGearStatus  -----------------------------------------------------------
        [Fact]
        public async Task ChangeGearStatus_ShouldUpdateStatus_WhenGearExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);
            CampingGearDAO.InitializeContext(dbContext); // Initialize context for the DAO

            int gearId = 1; // Existing GearId
            dbContext.CampingGears.Add(new CampingGear
            {
                GearId = gearId,
                GearName = "Tent",
                Status = true // Initial Status
            });
            dbContext.SaveChanges();

            // Act
            CampingGearDAO.ChangeGearStatus(gearId);

            // Assert
            var updatedGear = dbContext.CampingGears.FirstOrDefault(g => g.GearId == gearId);
            Assert.NotNull(updatedGear);
            Assert.False(updatedGear.Status); // Status should have toggled from true to false
        }

        [Fact]
        public async Task ChangeGearStatus_ShouldThrowException_WhenGearDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);
            CampingGearDAO.InitializeContext(dbContext); // Initialize context for the DAO

            int nonExistentGearId = 999; // Non-existing GearId

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => CampingGearDAO.ChangeGearStatus(nonExistentGearId));
            Assert.Equal($"Camping gear with ID {nonExistentGearId} does not exist.", exception.Message);
        }

        [Fact]
        public async Task ChangeGearStatus_ShouldThrowException_WhenContextIsNull()
        {
            // Arrange
            OrderDAO.InitializeContext(null); // Initialize with a null context

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => CampingGearDAO.ChangeGearStatus(1));
            
            Assert.Contains("Object reference not set to an instance of an object", exception.Message);
        }

        // Test for method AddCampingGear  -----------------------------------------------------------
        [Fact]
        public void AddCampingGear_ShouldAddGear_WhenValidDtoIsProvided()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);
            CampingGearDAO.InitializeContext(dbContext); // Initialize the context for the DAO

            var gearDto = new AddCampingGearDTO
            {
                GearId = 1,
                GearName = "Tent",
                QuantityAvailable = 10,
                RentalPrice = 50m,
                Description = "High quality tent",
                GearCategoryId = 2,
                ImgUrl = "http://example.com/tent.jpg",
                Status = true,
                CreatedAt = DateTime.Now // Optional field
            };

            // Act
            CampingGearDAO.AddCampingGear(gearDto); // Call the method to add camping gear

            // Assert
            var campingGear = dbContext.CampingGears.FirstOrDefault(g => g.GearId == gearDto.GearId);
            Assert.NotNull(campingGear); // Ensure the gear was added
            Assert.Equal(gearDto.GearName, campingGear.GearName); // Check GearName
            Assert.Equal(gearDto.QuantityAvailable, campingGear.QuantityAvailable); // Check QuantityAvailable
            Assert.Equal(gearDto.RentalPrice, campingGear.RentalPrice); // Check RentalPrice
            Assert.Equal(gearDto.Description, campingGear.Description); // Check Description
            Assert.Equal(gearDto.GearCategoryId, campingGear.GearCategoryId); // Check GearCategoryId
            Assert.Equal(gearDto.ImgUrl, campingGear.ImgUrl); // Check ImgUrl
            Assert.True(campingGear.Status); // Check that the Status is true
        }

        [Fact]
        public void AddCampingGear_ShouldThrowException_WhenGearNameIsNullOrEmpty()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);
            CampingGearDAO.InitializeContext(dbContext); // Initialize the context for the DAO

            var gearDto = new AddCampingGearDTO
            {
                GearId = 1,
                GearName = "", // Invalid empty GearName
                QuantityAvailable = 10,
                RentalPrice = 50m,
                Description = "High quality tent",
                GearCategoryId = 2,
                ImgUrl = "http://example.com/tent.jpg",
                Status = true,
                CreatedAt = DateTime.Now
            };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() =>  CampingGearDAO.AddCampingGear(gearDto));
            Assert.Contains("GearName", exception.Message); // Check if the error message mentions GearName
        }

        [Fact]
        public void AddCampingGear_ShouldThrowException_WhenQuantityAvailableIsZero()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);
            CampingGearDAO.InitializeContext(dbContext);

            var gearDto = new AddCampingGearDTO
            {
                GearId = 2,
                GearName = "Tent",
                QuantityAvailable = 0, // Invalid quantity
                RentalPrice = 50m,
                Description = "High quality tent",
                GearCategoryId = 2,
                ImgUrl = "http://example.com/tent.jpg",
                Status = true,
                CreatedAt = DateTime.Now
            };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => CampingGearDAO.AddCampingGear(gearDto));
            Assert.Contains("QuantityAvailable", exception.Message); // Check if the error message mentions QuantityAvailable
        }

        [Fact]
        public void AddCampingGear_ShouldAddGear_WhenOptionalFieldsAreNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);
            CampingGearDAO.InitializeContext(dbContext);

            var gearDto = new AddCampingGearDTO
            {
                GearId = 1,
                GearName = "Tent",
                QuantityAvailable = 10,
                RentalPrice = 50m,
                Description = null, // Optional field
                GearCategoryId = 1,
                ImgUrl = "http://example.com/tent.jpg", // Optional field
                Status = true,
                CreatedAt = null // Optional field
            };

            // Act
            CampingGearDAO.AddCampingGear(gearDto); // Call the method to add camping gear

            // Assert
            var campingGear = dbContext.CampingGears.FirstOrDefault(g => g.GearId == gearDto.GearId);
            Assert.NotNull(campingGear); // Ensure the gear was added
            Assert.Null(campingGear.Description); // Ensure Description is null (since it was not provided)
            Assert.Null(campingGear.CreatedAt); // Ensure CreatedAt is null (since it was not provided)
            Assert.Equal(gearDto.ImgUrl, campingGear.ImgUrl); // Ensure ImgUrl was added correctly
        }

        [Fact]
        public void AddCampingGear_ShouldThrowException_WhenImgUrlIsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);
            CampingGearDAO.InitializeContext(dbContext);

            var gearDto = new AddCampingGearDTO
            {
                GearId = 1,
                GearName = "Tent",
                QuantityAvailable = 10,
                RentalPrice = 50m,
                Description = "Comfortable sleeping bag",
                GearCategoryId = 1,
                ImgUrl = null, // ImgUrl is required but null
                Status = true,
                CreatedAt = DateTime.Now
            };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => CampingGearDAO.AddCampingGear(gearDto));
            Assert.Contains("ImgUrl", exception.Message); // Ensure error mentions ImgUrl
        }

        [Fact]
        public void AddCampingGear_ShouldThrowException_WhenDbContextIsNull()
        {
            // Arrange
            CampingGearDAO.InitializeContext(null); // Initialize with a null context

            var gearDto = new AddCampingGearDTO
            {
                GearId = 1,
                GearName = "Tent",
                QuantityAvailable = 10,
                RentalPrice = 50m,
                Description = "High quality tent",
                GearCategoryId = 2,
                ImgUrl = "http://example.com/tent.jpg"
            };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => CampingGearDAO.AddCampingGear(gearDto));
            Assert.NotNull(exception);
            Assert.Contains("Object reference not set to an instance of an object", exception.Message);
        }

        // Test for method UpdateCampingGear  -----------------------------------------------------------
        [Fact]
        public async Task UpdateCampingGear_ShouldUpdateGear_WhenGearExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);

            // Seed data
            var existingGear = new CampingGear
            {
                GearId = 1,
                GearName = "Tent",
                QuantityAvailable = 10,
                RentalPrice = 50.0m,
                Description = "A sturdy tent",
                GearCategoryId = 2,
                ImgUrl = "http://example.com/tent.jpg",
            };
            dbContext.CampingGears.Add(existingGear);
            await dbContext.SaveChangesAsync();

            // Initialize context for the DAO
            CampingGearDAO.InitializeContext(dbContext);

            var updateDto = new UpdateCampingGearDTO
            {
                GearId = 1,
                GearName = "Updated Tent",
                QuantityAvailable = 15,
                RentalPrice = 60.0m,
                Description = "An updated sturdy tent",
                GearCategoryId = 3,
                ImgUrl = "http://example.com/updated_tent.jpg",
            };

            // Act
            CampingGearDAO.UpdateCampingGear(updateDto);

            // Assert
            var updatedGear = dbContext.CampingGears.FirstOrDefault(g => g.GearId == 1);
            Assert.NotNull(updatedGear);
            Assert.Equal(updateDto.GearName, updatedGear.GearName);
            Assert.Equal(updateDto.QuantityAvailable, updatedGear.QuantityAvailable);
            Assert.Equal(updateDto.RentalPrice, updatedGear.RentalPrice);
            Assert.Equal(updateDto.Description, updatedGear.Description);
            Assert.Equal(updateDto.GearCategoryId, updatedGear.GearCategoryId);
            Assert.Equal(updateDto.ImgUrl, updatedGear.ImgUrl);
        }

        [Fact]
        public async Task UpdateCampingGear_ShouldThrowException_WhenGearDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);

            // No gear is seeded
            CampingGearDAO.InitializeContext(dbContext);

            var updateDto = new UpdateCampingGearDTO
            {
                GearId = 999, // Non-existing GearId
                GearName = "Non-Existent Gear",
                QuantityAvailable = 5,
                RentalPrice = 100.0m,
                Description = "Description for non-existent gear",
                GearCategoryId = 1,
                ImgUrl = "http://example.com/nonexistent.jpg",
            };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => CampingGearDAO.UpdateCampingGear(updateDto));
            Assert.Contains($"Camping gear with ID {updateDto.GearId} does not exist.", exception.Message);
        }

        [Fact]
        public async Task UpdateCampingGear_ShouldHandleNullFields_Gracefully()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GreenGardenContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new GreenGardenContext(options);

            var existingGear = new CampingGear
            {
                GearId = 1,
                GearName = "Tent",
                QuantityAvailable = 10,
                RentalPrice = 50.0m,
                Description = "A sturdy tent",
                GearCategoryId = 2,
                ImgUrl = "http://example.com/tent.jpg",
            };
            dbContext.CampingGears.Add(existingGear);
            await dbContext.SaveChangesAsync();

            CampingGearDAO.InitializeContext(dbContext);

            var updateDto = new UpdateCampingGearDTO
            {
                GearId = 1,
                GearName = "Updated Tent",
                QuantityAvailable = 20,
                RentalPrice = 80.0m,
                Description = null, // Null description
                GearCategoryId = null, // Null category
                ImgUrl = null, // Null image URL
            };

            // Act
            CampingGearDAO.UpdateCampingGear(updateDto);

            // Assert
            var updatedGear = dbContext.CampingGears.FirstOrDefault(g => g.GearId == 1);
            Assert.NotNull(updatedGear);
            Assert.Equal(updateDto.GearName, updatedGear.GearName);
            Assert.Equal(updateDto.QuantityAvailable, updatedGear.QuantityAvailable);
            Assert.Equal(updateDto.RentalPrice, updatedGear.RentalPrice);
            Assert.Null(updatedGear.Description); // Description should be null
            Assert.Null(updatedGear.GearCategoryId); // GearCategoryId should be null
            Assert.Null(updatedGear.ImgUrl); // ImgUrl should be null
        }

        [Fact]
        public async Task UpdateCampingGear_ShouldThrowException_WhenContextIsNull()
        {
            // Arrange
            CampingGearDAO.InitializeContext(null); // Null context

            var updateDto = new UpdateCampingGearDTO
            {
                GearId = 1,
                GearName = "Updated Tent",
                QuantityAvailable = 15,
                RentalPrice = 60.0m,
                Description = "An updated sturdy tent",
                GearCategoryId = 3,
                ImgUrl = "http://example.com/updated_tent.jpg",
            };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => CampingGearDAO.UpdateCampingGear(updateDto));
            Assert.Contains("Object reference not set to an instance of an object", exception.Message); // Ensure a null reference exception message
        }

    }
}