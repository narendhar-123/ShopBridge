using Dasync.Collections;
using Microsoft.EntityFrameworkCore;
using Moq;
using NSubstitute;
using NUnit.Framework;
using ShopBridge.Domain.Model;
using ShopBridge.Repository.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Repository.Test
{
    [TestFixture]
    public class InventoryRepositoryTest
    {
        private InventoryRepository _repository;
        private ShopBridgeContext _context;

        public InventoryRepositoryTest()
        {
            
        }
        [SetUp]
        public void Setup()
        {
            var inventories = new List<Inventory>();
            inventories.Add(new Inventory()
            {
                Id = 1,
                Name = "Name",
                Price = 1
            });

            inventories.Add(new Inventory()
            {
                Id = 2,
                Name = "Name 2",
                Price = 2
            });

            var queryable = inventories.AsQueryable();
            IDbSet<Inventory> mockDbSet = Substitute.For<IDbSet<Inventory>>();
            mockDbSet.Provider.Returns(queryable.Provider);
            mockDbSet.Expression.Returns(queryable.Expression);
            mockDbSet.ElementType.Returns(queryable.ElementType);
            mockDbSet.GetEnumerator().Returns(queryable.GetEnumerator());

            _context = Substitute.For<ShopBridgeContext>();
            _context.Inventories.Returns(mockDbSet);
            _repository = new InventoryRepository(_context);
            
        }

        [Test]
        public async Task GetAll_Return_List()
        {
            
            var actual = _repository.GetAll();

            Assert.AreEqual(2, actual.Count());
        }
    }

    public static class DbContextMock
    {
        public static List<Inventory> GetInventories()
        {
            List<Inventory> inventories = new List<Inventory>();
            inventories.Add(new Inventory()
            {
                Id = 1,
                Name = "Name",
                Price = 1
            });

            inventories.Add(new Inventory()
            {
                Id = 2,
                Name = "Name 2",
                Price = 2
            });
            return inventories;
        }
        public static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();
            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));
            return dbSet.Object;
        }
    }
}