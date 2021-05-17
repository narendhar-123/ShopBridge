using Moq;
using NUnit.Framework;
using ShopBridge.Domain.Model;
using ShopBridge.IRepository;
using ShopBridge.Logger;
using ShopBridge.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ShopBridge.Service.Test
{
    [TestFixture]
    public class InventoryServiceTest
    {
        private readonly InventoryService _service;
        private readonly Mock<IInventoryRepository> _repository;
        private readonly Mock<ILoggerManager> _logger;

        public InventoryServiceTest()
        {
            _repository = new Mock<IInventoryRepository>();
            _logger = new Mock<ILoggerManager>();
            _service = new InventoryService(_repository.Object, _logger.Object);
        }

        #region Add
        [Test]
        public async Task Add_Return_InventoryId_Zero()
        {
            var expected = new Inventory() { Id = 1 };
            var input = new InventoryDTO() { Id = 1 };
            _repository.Setup(s => s.Add(It.IsAny<Inventory>())).ReturnsAsync(expected);

            var actual = await _service.Add(input);

            Assert.IsNotNull(actual);
            Assert.AreEqual(false, actual.IsSucceeded);
            Assert.AreEqual("Inventory id should be zero while adding", actual.GetErrorString());
        }

        [Test]
        public async Task Add_Return_InventoryName_Empty()
        {
            var expected = new Inventory() { Id = 0 };
            var input = new InventoryDTO() { Id = 0 };
            _repository.Setup(s => s.Add(It.IsAny<Inventory>())).ReturnsAsync(expected);

            var actual = await _service.Add(input);

            Assert.IsNotNull(actual);
            Assert.AreEqual(false, actual.IsSucceeded);
            Assert.AreEqual("Inventory Name should not be empty", actual.GetErrorString());
        }

        [Test]
        public async Task Add_Return_InventoryPrice_Zero()
        {
            var expected = new Inventory() { Id = 0, Name = "Name" };
            var input = new InventoryDTO() { Id = 0, Name = "Name" };
            _repository.Setup(s => s.Add(It.IsAny<Inventory>())).ReturnsAsync(expected);

            var actual = await _service.Add(input);

            Assert.IsNotNull(actual);
            Assert.AreEqual(false, actual.IsSucceeded);
            Assert.AreEqual("Inventory Price should not be zero", actual.GetErrorString());
        }

        [Test]
        public async Task Add_Return_InventoryID_Zero()
        {
            var expected = new Inventory() { Id = 0, Name = "Name", Price = 1 };
            var input = new InventoryDTO() { Id = 0, Name = "Name", Price = 1 };
            _repository.Setup(s => s.Add(It.IsAny<Inventory>())).ReturnsAsync(expected);

            var actual = await _service.Add(input);

            Assert.IsNotNull(actual);
            Assert.AreEqual(false, actual.IsSucceeded);
            Assert.AreEqual("Failed to add inventory", actual.GetErrorString());
        }

        [Test]
        public async Task Add_Return_InventoryID()
        {
            var expected = new Inventory() { Id = 1, Name = "Name", Price = 1 };
            var input = new InventoryDTO() { Id = 0, Name = "Name", Price = 1 };
            _repository.Setup(s => s.Add(It.IsAny<Inventory>())).ReturnsAsync(expected);

            var actual = await _service.Add(input);

            Assert.IsNotNull(actual);
            Assert.AreEqual(true, actual.IsSucceeded);
            Assert.AreEqual("", actual.GetErrorString());
            Assert.AreEqual(1, actual.Value);
        }

        [Test]
        public async Task Add_Return_Exception()
        {
            var input = new InventoryDTO() { Id = 0, Name = "Name", Price = 1 };
            _repository.Setup(s => s.Add(It.IsAny<Inventory>())).ReturnsAsync((Inventory)null);

            var actual = await _service.Add(input);

            Assert.IsNotNull(actual);
            Assert.AreEqual(false, actual.IsSucceeded);
            Assert.AreEqual("Failed to add inventory", actual.GetErrorString());
        }

        #endregion

        #region Delete
        [Test]
        public async Task Delete_Return_InventoryId_Zero_Error()
        {

            var actual = await _service.Delete(0);

            Assert.AreEqual(actual.IsSucceeded, false);
            Assert.AreEqual("Inventory Id should be greater than zero", actual.GetErrorString());
        }

        [Test]
        public async Task Delete_Return_No_InventoryId_Found_Error()
        {

            _repository.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync((Inventory)null);

            var actual = await _service.Delete(1);

            Assert.AreEqual(false, actual.IsSucceeded);
            Assert.AreEqual("No inventory found to delete", actual.GetErrorString());
        }

        [Test]
        public async Task Delete_Return_True()
        {

            _repository.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync((new Inventory() { Id = 0 }));

            var actual = await _service.Delete(1);

            Assert.AreEqual(actual.IsSucceeded, true);
            Assert.AreEqual("", actual.GetErrorString());
        }
        #endregion

        #region GetAll
        [Test]
        public async Task GetAll_Return_InventoryList()
        {
            var expected = new List<Inventory>() { new Inventory() { Id = 1, Name = "Name" } };
            _repository.Setup(s => s.GetAll(It.IsAny<int>(), It.IsAny<int>())).Returns(expected.AsQueryable());

            var actual = await _service.GetAll(1, 1);

            Assert.AreEqual(true, actual.IsSucceeded);
            Assert.AreEqual(1, actual.Value.Count);
        }

        #endregion

        #region GetById
        [Test]
        public async Task GetById_Return_Inventory()
        {
            var expected = new Inventory() { Id = 1 };

            _repository.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync(expected);

            var actual = await _service.GetById(1);

            Assert.AreEqual(true, actual.IsSucceeded);
            Assert.IsNotNull(actual.Value);
        }

        #endregion

        #region Update
        [Test]
        public async Task Update_Return_InventoryId_Zero_Error()
        {
            var expected = new Inventory() { Id = 1 };

            _repository.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync(expected);

            var actual = await _service.Update(new InventoryDTO());

            Assert.AreEqual(false, actual.IsSucceeded);
            Assert.AreEqual("Inventory id should not be zero while updating", actual.GetErrorString());
        }

        [Test]
        public async Task Update_Return_InventoryName_Empty_Error()
        {
            var expected = new Inventory() { Id = 1 };

            _repository.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync(expected);

            var actual = await _service.Update(new InventoryDTO() { Id = 1 });

            Assert.AreEqual(false, actual.IsSucceeded);
            Assert.AreEqual("Inventory Name should not be empty", actual.GetErrorString());
        }

        [Test]
        public async Task Update_Return_InventoryPrice_Zero_Error()
        {
            var expected = new Inventory() { Id = 1, Name = "Name" };

            _repository.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync(expected);

            var actual = await _service.Update(new InventoryDTO() { Id = 1, Name = "Name" });

            Assert.AreEqual(false, actual.IsSucceeded);
            Assert.AreEqual("Inventory Price should not be zero", actual.GetErrorString());
        }

        [Test]
        public async Task Update_Return_No_Inventory_Error()
        {

            _repository.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync((Inventory)null);

            var actual = await _service.Update(new InventoryDTO() { Id = 1, Name = "Name", Price = 1 });

            Assert.AreEqual(false, actual.IsSucceeded);
            Assert.AreEqual("No inventory found to update", actual.GetErrorString());
        }

        [Test]
        public async Task Update_Return_Fail_Update_Inventory_Error()
        {
            var expected = new Inventory() { Id = 0, Name = "Name", Price = 1 };

            _repository.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync(expected);
            _repository.Setup(s => s.Update(It.IsAny<Inventory>())).ReturnsAsync(expected);

            var actual = await _service.Update(new InventoryDTO() { Id = 1, Name = "Name", Price = 1 });

            Assert.AreEqual(false, actual.IsSucceeded);
            Assert.AreEqual("Failed to update inventory", actual.GetErrorString());
        }

        [Test]
        public async Task Update_Return_Updated_Inventory()
        {
            var expected = new Inventory() { Id = 1, Name = "Name", Price = 1 };

            _repository.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync(expected);
            _repository.Setup(s => s.Update(It.IsAny<Inventory>())).ReturnsAsync(expected);

            var actual = await _service.Update(new InventoryDTO() { Id = 1, Name = "Name", Price = 1 });

            Assert.AreEqual(true, actual.IsSucceeded);
            Assert.AreEqual("", actual.GetErrorString());
        }
        #endregion
    }
}