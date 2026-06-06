using InventorySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventorySystem.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PurchaseItem>(entity =>
            {
                entity.Property(e => e.Quantity)
                .HasPrecision(18, 4);
                entity.Property(e => e.PurchasePrice)
                .HasPrecision(18, 4);
                entity.Property(e => e.SalesPrice)
                .HasPrecision(18, 4);
            });

            modelBuilder.Entity<SaleItem>(entity =>
            {
                entity.Property(e => e.Quantity)
                .HasPrecision(18, 4);
                entity.Property(e => e.SalesPrice)
                .HasPrecision(18, 4);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email)
                .IsUnique();
            });
        }

    }
}
