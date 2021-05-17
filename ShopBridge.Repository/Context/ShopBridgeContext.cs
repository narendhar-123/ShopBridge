using Microsoft.EntityFrameworkCore;
using ShopBridge.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Repository.Context
{
    public partial class ShopBridgeContext : DbContext
    {
        public ShopBridgeContext()
        {
        }

        public ShopBridgeContext(DbContextOptions<ShopBridgeContext> options)
            : base(options)
        {
        }

        public DbSet<Inventory> Inventories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>().ToTable("Inventory");
        }
    }
}
