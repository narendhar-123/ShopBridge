using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Domain.Model
{
    public class Inventory : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
