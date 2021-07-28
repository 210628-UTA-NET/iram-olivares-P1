using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SAModels;

namespace SADL
{
    public class StoreAppDBContext : DbContext
    {
        public StoreAppDBContext() : base()
        { }

        public StoreAppDBContext(DbContextOptions options) : base(options)
        { }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<LineItem> LineItems { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<StoreFront> Stores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(customer => customer.CustomerID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Product>()
                .Property(product => product.ProductID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<StoreFront>()
                .Property(store => store.StoreFrontID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Order>()
                .Property(order => order.OrderID)
                .ValueGeneratedOnAdd();    

            modelBuilder.Entity<LineItem>()
                .Property(item => item.LineItemID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<OrderItem>()
                .Property(item => item.OrderItemID)
                .ValueGeneratedOnAdd();
        }
    }
}