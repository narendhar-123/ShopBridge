using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.IService;
using ShopBridge.Model;

namespace ShopBridge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }
        [HttpGet, Route("GetById/{Id:int}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var result = await _inventoryService.GetById(Id);
            if (!result.IsSucceeded)
            {
                return BadRequest(result.GetErrorString());
            }
            return Ok(result.Value);
        }
        [HttpGet, Route("GetAll")]
        public async Task<IActionResult> GetAll(int pageNumber = 0, int pageSize = 0)
        {
            var result = await _inventoryService.GetAll(pageNumber, pageSize);
            if (!result.IsSucceeded)
            {
                return BadRequest(result.GetErrorString());
            }
            return Ok(result.Value);
        }
        [HttpPost, Route("Add")]
        public async Task<IActionResult> Add(InventoryDTO inventory)
        {
            var result = await _inventoryService.Add(inventory);
            if (!result.IsSucceeded)
            {
                return BadRequest(result.GetErrorString());
            }
            return Ok(result.Value);
        }
        [HttpPut, Route("Update")]
        public async Task<IActionResult> Update(InventoryDTO inventory)
        {
            var result = await _inventoryService.Update(inventory);
            if (!result.IsSucceeded)
            {
                return BadRequest(result.GetErrorString());
            }
            return Ok(result.Value);
        }
        [HttpDelete, Route("Delete/{Id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _inventoryService.Delete(Id);
            if (!result.IsSucceeded)
            {
                return BadRequest(result.GetErrorString());
            }
            return Ok(result.Value);
        }
    }
}
