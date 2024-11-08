using BusinessObject.Models;
using DataAccess.DAO;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenGardenAPITest.DAO
{
    public class CampingGearDAOTest
    {
        // test for method GetCampingGear

        private readonly Mock<GreenGardenContext> _contextMock;
        private readonly IQueryable<CampingGear> _campingGears;

        public CampingGearDAOTest()
        {
            _contextMock = new Mock<GreenGardenContext>();

            _campingGears = new List<CampingGear>
        {
            new CampingGear { GearId = 1, GearName = "Tent", GearCategoryId = 1, RentalPrice = 50000, QuantityAvailable = 10, CreatedAt = DateTime.Now.AddDays(-10) },
            new CampingGear { GearId = 2, GearName = "Sleeping Bag", GearCategoryId = 2, RentalPrice = 150000, QuantityAvailable = 5, CreatedAt = DateTime.Now.AddDays(-5) },
            new CampingGear { GearId = 3, GearName = "Backpack", GearCategoryId = 3, RentalPrice = 300000, QuantityAvailable = 20, CreatedAt = DateTime.Now.AddDays(-1) },
            new CampingGear { GearId = 4, GearName = "Cooking Set", GearCategoryId = 1, RentalPrice = 450000, QuantityAvailable = 15, CreatedAt = DateTime.Now.AddDays(-3) }
        }.AsQueryable();

            var dbSetMock = new Mock<DbSet<CampingGear>>();
            dbSetMock.As<IQueryable<CampingGear>>().Setup(m => m.Provider).Returns(_campingGears.Provider);
            dbSetMock.As<IQueryable<CampingGear>>().Setup(m => m.Expression).Returns(_campingGears.Expression);
            dbSetMock.As<IQueryable<CampingGear>>().Setup(m => m.ElementType).Returns(_campingGears.ElementType);
            dbSetMock.As<IQueryable<CampingGear>>().Setup(m => m.GetEnumerator()).Returns(_campingGears.GetEnumerator());

            _contextMock.Setup(c => c.CampingGears).Returns(dbSetMock.Object);
        }

        [Fact]
        public void GetCampingGears_NoFilters_ReturnsAllItems()
        {
            var result = CampingGearDAO.GetCampingGears(null, null, null, null);

            Assert.Equal(4, result.Count);
        }

        [Fact]
        public void GetCampingGears_FilterByCategory_ReturnsCategoryItems()
        {
            var result = CampingGearDAO.GetCampingGears(1, null, null, null);

            Assert.Equal(2, result.Count);
            Assert.All(result, gear => Assert.Equal("Shelter", gear.GearCategoryName));
        }

        [Fact]
        public void GetCampingGears_FilterByPriceRangeBelow100000_ReturnsCorrectItems()
        {
            var result = CampingGearDAO.GetCampingGears(null, null, 1, null);

            Assert.Single(result);
            Assert.All(result, gear => Assert.True(gear.RentalPrice < 100000));
        }

        [Fact]
        public void GetCampingGears_FilterByPriceRangeBetween100000And300000_ReturnsCorrectItems()
        {
            var result = CampingGearDAO.GetCampingGears(null, null, 2, null);

            Assert.Equal(2, result.Count);
            Assert.All(result, gear => Assert.InRange(gear.RentalPrice, 100000, 300000));
        }

        [Fact]
        public void GetCampingGears_FilterByPriceRangeAbove300000_ReturnsCorrectItems()
        {
            var result = CampingGearDAO.GetCampingGears(null, null, 3, null);

            Assert.Single(result);
            Assert.All(result, gear => Assert.True(gear.RentalPrice > 300000));
        }

        [Fact]
        public void GetCampingGears_SortByPriceAscending_ReturnsItemsInAscendingOrder()
        {
            var result = CampingGearDAO.GetCampingGears(null, 1, null, null);

            Assert.Equal(4, result.Count);
            Assert.True(result.SequenceEqual(result.OrderBy(g => g.RentalPrice)));
        }

        [Fact]
        public void GetCampingGears_SortByPriceDescending_ReturnsItemsInDescendingOrder()
        {
            var result = CampingGearDAO.GetCampingGears(null, 2, null, null);

            Assert.Equal(4, result.Count);
            Assert.True(result.SequenceEqual(result.OrderByDescending(g => g.RentalPrice)));
        }

        [Fact]
        public void GetCampingGears_SortByNameAscending_ReturnsItemsInAlphabeticalOrder()
        {
            var result = CampingGearDAO.GetCampingGears(null, 3, null, null);

            Assert.Equal(4, result.Count);
            Assert.True(result.SequenceEqual(result.OrderBy(g => g.GearName)));
        }

        [Fact]
        public void GetCampingGears_SortByNameDescending_ReturnsItemsInReverseAlphabeticalOrder()
        {
            var result = CampingGearDAO.GetCampingGears(null, 4, null, null);

            Assert.Equal(4, result.Count);
            Assert.True(result.SequenceEqual(result.OrderByDescending(g => g.GearName)));
        }

        [Fact]
        public void GetCampingGears_SortByPopularityMostAvailable_ReturnsItemsByQuantityAvailable()
        {
            var result = CampingGearDAO.GetCampingGears(null, null, null, 1);

            Assert.Equal(4, result.Count);
            Assert.True(result.SequenceEqual(result.OrderByDescending(g => g.QuantityAvailable)));
        }

        [Fact]
        public void GetCampingGears_SortByNewest_ReturnsItemsByCreationDate()
        {
            var result = CampingGearDAO.GetCampingGears(null, null, null, 2);

            Assert.Equal(4, result.Count);
            Assert.True(result.SequenceEqual(result.OrderByDescending(g => g.CreatedAt)));
        }

        [Fact]
        public void GetCampingGears_FilterByCategoryAndPriceRange_ReturnsCorrectItems()
        {
            var result = CampingGearDAO.GetCampingGears(1, null, 1, null);

            Assert.Single(result);
            Assert.Equal("Shelter", result.First().GearCategoryName);
            Assert.True(result.First().RentalPrice < 100000);
        }

        [Fact]
        public void GetCampingGears_FilterByInvalidCategory_ReturnsNoItems()
        {
            var result = CampingGearDAO.GetCampingGears(9999, null, null, null);

            Assert.Empty(result);
        }

        [Fact]
        public void GetCampingGears_InvalidSortAndFilterOptions_ReturnsAllItemsUnsorted()
        {
            var result = CampingGearDAO.GetCampingGears(null, 99, 99, 99);

            Assert.Equal(4, result.Count);
        }
    }
}
