using ShopBridge.Domain.Model;
using ShopBridge.IRepository;
using ShopBridge.Model;
using ShopBridge.Repository.Context;
using ShopBridge.Repository.Utilities;
using System.Linq;

namespace ShopBridge.Repository
{
    public class InventoryRepository : BaseRepository<Inventory>, IInventoryRepository
    {
        public InventoryRepository(ShopBridgeContext dbContext) : base(dbContext)
        {
        }
    }
}
