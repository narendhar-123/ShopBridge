using ShopBridge.Domain.Model;
using ShopBridge.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Service.Mapper
{
    public static class InventoryMapper
    {
        public static Inventory GetInventory(this InventoryDTO inventory)
        {
            var inv = new Inventory()
            {
                Id = inventory.Id,
                Name = inventory.Name,
                Description = inventory.Description,
                Price = inventory.Price,
                IsDeleted = inventory.IsDeleted,
                CreatedBy = inventory.CreatedById,
                CreatedOn = inventory.CreatedOn,
                ModifiedBy = inventory.ModifiedById,
                ModifiedOn = inventory.ModifiedOn
            };
            return inv;
        }

        public static InventoryDTO GetInventoryDTO(this Inventory inventory)
        {
            return new InventoryDTO()
            {
                Id = inventory.Id,
                Name = inventory.Name,
                Description = inventory.Description,
                Price = inventory.Price,
                IsDeleted = inventory.IsDeleted,
                CreatedById = inventory.CreatedBy,
                CreatedOn = inventory.CreatedOn,
                ModifiedById = inventory.ModifiedBy,
                ModifiedOn = inventory.ModifiedOn
            };
        }
        public static List<InventoryDTO> GetInventoryDTO(this List<Inventory> inventories)
        {
            List<InventoryDTO> inventoryDTOs = new List<InventoryDTO>();
            foreach (var inventory in inventories)
            {
                inventoryDTOs.Add(inventory.GetInventoryDTO());
            }
            return inventoryDTOs;
        }
        public static Inventory BaseMap(this Inventory entity)
        {
            if (entity.Id > 0)
            {
                entity.ModifiedBy = 1;
                entity.ModifiedOn = DateTime.Now;
            }
            else
            {
                entity.CreatedBy = 1;
                entity.CreatedOn = DateTime.Now;
                entity.ModifiedBy = 1;
                entity.ModifiedOn = DateTime.Now;
            }
            return entity;
        }
    }
}
