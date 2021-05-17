using ShopBridge.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.IService
{
    public interface IInventoryService : IBaseService
    {
        public Task<Result<List<InventoryDTO>>> GetAll(int pageNumber = 0, int pageSize = 0);
        public Task<Result<InventoryDTO>> GetById(int Id);
        public Task<Result<int>> Add(InventoryDTO inventory);
        public Task<Result<int>> Update(InventoryDTO inventory);
        public Task<Result<bool>> Delete(int Id);
    }
}
