using ShopBridge.Domain.Model;
using ShopBridge.IRepository;
using ShopBridge.IService;
using ShopBridge.Logger;
using ShopBridge.Model;
using ShopBridge.Service.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Service
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;
        private readonly ILoggerManager _logger;
        public InventoryService(IInventoryRepository repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<Result<int>> Add(InventoryDTO inventory)
        {
            try
            {
                if (inventory.Id > 0)
                {
                    return Result.Fail<int>("Inventory id should be zero while adding");
                }
                var validate = ValidateInventory(inventory);
                if (!validate.Value)
                {
                    return Result.Fail<int>(validate.GetErrorString());
                }
                var inv = inventory.GetInventory();
                var result = await _repository.Add(inv.BaseMap());
                if (result.Id > 0)
                {
                    return Result.Ok(result.Id);
                }
                else
                {
                    return Result.Fail<int>("Failed to add inventory");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result.Fail<int>("Failed to add inventory");
            }

        }

        public async Task<Result<bool>> Delete(int Id)
        {
            try
            {
                if (Id == 0)
                {
                    return Result.Fail<bool>("Inventory Id should be greater than zero");
                }
                var inv = await _repository.GetAsync(Id);
                if (inv == null)
                {
                    return Result.Fail<bool>("No inventory found to delete");
                }
                await _repository.Delete(Id);
                return Result.Ok<bool>(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result.Fail<bool>("No inventory found to delete");
            }

        }

        public async Task<Result<List<InventoryDTO>>> GetAll(int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = _repository.GetAll(pageNumber, pageSize).ToList();
                return Result.Ok(result.GetInventoryDTO());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result.Fail<List<InventoryDTO>>("Failed to get inventories");
            }

        }

        public async Task<Result<InventoryDTO>> GetById(int Id)
        {
            try
            {
                Inventory inventory = await _repository.GetAsync(Id);
                if (inventory != null)
                {
                    return Result.Ok(inventory.GetInventoryDTO());
                }
                return Result.Fail<InventoryDTO>("No inventory found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result.Fail<InventoryDTO>("Failed to get inventory");
            }

        }

        public async Task<Result<int>> Update(InventoryDTO inventory)
        {
            try
            {
                if (inventory.Id == 0)
                {
                    return Result.Fail<int>("Inventory id should not be zero while updating");
                }
                var validate = ValidateInventory(inventory);
                if (!validate.Value)
                {
                    return Result.Fail<int>(validate.GetErrorString());
                }
                Inventory existing = await _repository.GetAsync(inventory.Id);
                if (existing == null)
                {
                    return Result.Fail<int>("No inventory found to update");
                }
                existing.Name = inventory.Name;
                existing.Description = inventory.Description;
                existing.Price = inventory.Price;
                var result = await _repository.Update(existing.BaseMap());
                if (result.Id > 0)
                {
                    return Result.Ok(result.Id);
                }
                else
                {
                    return Result.Fail<int>("Failed to update inventory");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result.Fail<int>("Failed to update inventory");
            }

        }

        private Result<bool> ValidateInventory(InventoryDTO inventory)
        {
            if (string.IsNullOrEmpty(inventory.Name))
            {
                return Result.Fail<bool>("Inventory Name should not be empty");
            }
            if (inventory.Price == 0.0)
            {
                return Result.Fail<bool>("Inventory Price should not be zero");
            }
            return Result.Ok(true);
        }
    }
}
