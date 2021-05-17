using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ShopBridge.API.Controllers;
using ShopBridge.IService;
using ShopBridge.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.API.Test
{
    [TestFixture]
    class InventoryControllerTest
    {
        private InventoryController _controller;
        private readonly Mock<IInventoryService> _service;
        public InventoryControllerTest()
        {
            _service = new Mock<IInventoryService>();
            _controller = new InventoryController(_service.Object);
        }

        [Test]
        public async Task GetById_Return_Entity()
        {
            var expectedinv = new InventoryDTO() { Id = 1, Name = "Name" };
            _service.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(new Result<InventoryDTO>(expectedinv, true, ""));

            var actual = await _controller.GetById(1);
            var okResult = actual as OkObjectResult;
            var actualObject = (InventoryDTO)okResult.Value;

            Assert.AreEqual(expectedinv.Id, actualObject.Id);
        }
        [Test]
        public async Task GetById_Return_BadRequest()
        {
            _service.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(new Result<InventoryDTO>(null, false, "Failed"));

            var actual = await _controller.GetById(1);
            var okResult = actual as BadRequestObjectResult;

            Assert.AreEqual(okResult.Value, "Failed");
            Assert.AreEqual(okResult.StatusCode, 400);
        }

        [Test]
        public async Task GetAll_Return_List()
        {
            var expected = new List<InventoryDTO>() { new InventoryDTO() { Id = 1, Name = "Name", Description = "Description" } };

            _service.Setup(s => s.GetAll(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new Result<List<InventoryDTO>>(expected, true, ""));

            var actual = await _controller.GetAll(1, 1);
            var actualObject = actual as OkObjectResult;
            var actualList = actualObject.Value as List<InventoryDTO>;

            Assert.IsNotNull(actualList);
            Assert.AreEqual(actualList.Count, 1);
            Assert.AreEqual(actualList[0].Id, 1);

        }

        [Test]
        public async Task GetAll_Return_BadRequest()
        {
            _service.Setup(s => s.GetAll(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new Result<List<InventoryDTO>>(null, false, "Failed"));

            var actual = await _controller.GetAll(0, 0);
            var okResult = actual as BadRequestObjectResult;

            Assert.AreEqual(okResult.Value, "Failed");
            Assert.AreEqual(okResult.StatusCode, 400);
        }

        [Test]
        public async Task Add_Return_Entity()
        {
            var expected = new InventoryDTO() { Id = 1 };
            _service.Setup(s => s.Add(It.IsAny<InventoryDTO>())).ReturnsAsync(new Result<int>(1, true, ""));

            var actual = await _controller.Add(expected);
            var actualObject = actual as OkObjectResult;

            Assert.IsNotNull(actualObject);
            Assert.AreEqual(actualObject.Value, 1);
        }


        [Test]
        public async Task Add_Return_BadRequest()
        {
            var expected = new InventoryDTO() { Id = 1 };
            _service.Setup(s => s.Add(It.IsAny<InventoryDTO>())).ReturnsAsync(new Result<int>(1, false, "Failed"));

            var actual = await _controller.Add(expected);
            var actualObject = actual as BadRequestObjectResult;

            Assert.IsNotNull(actualObject);
            Assert.AreEqual(actualObject.StatusCode, 400);
            Assert.AreEqual(actualObject.Value, "Failed");
        }

        [Test]
        public async Task Update_Return_Entity()
        {
            var expected = new InventoryDTO() { Id = 1 };
            _service.Setup(s => s.Update(It.IsAny<InventoryDTO>())).ReturnsAsync(new Result<int>(1, true, ""));

            var actual = await _controller.Update(expected);
            var actualObject = actual as OkObjectResult;

            Assert.IsNotNull(actualObject);
            Assert.AreEqual(actualObject.Value, 1);
        }


        [Test]
        public async Task Update_Return_BadRequest()
        {
            var expected = new InventoryDTO() { Id = 1 };
            _service.Setup(s => s.Update(It.IsAny<InventoryDTO>())).ReturnsAsync(new Result<int>(1, false, "Failed"));

            var actual = await _controller.Update(expected);
            var actualObject = actual as BadRequestObjectResult;

            Assert.IsNotNull(actualObject);
            Assert.AreEqual(actualObject.StatusCode, 400);
            Assert.AreEqual(actualObject.Value, "Failed");
        }

        [Test]
        public async Task Delete_Return_True()
        {
            _service.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(new Result<bool>(true, true, ""));

            var actual = await _controller.Delete(1);
            var actualObject = actual as OkObjectResult;

            Assert.IsNotNull(actualObject);
            Assert.AreEqual(true, actualObject.Value);
        }
    }
}
