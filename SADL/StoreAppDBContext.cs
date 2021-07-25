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

        /*protected override void OnConfiguring(DbContextOptionsBuilder p_options)
            => p_options.UseSqlServer("Server=tcp:ieo.database.windows.net,1433;Initial Catalog=ieoDemoDB;Persist Security Info=False;User ID=ieolivares;Password=RlrrLrll97;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
*/
        protected override void OnModelCreating(ModelBuilder p_modelBuilder)
        {
            p_modelBuilder.Entity<Customer>()
                .Property(customer => customer.CustomerID)
                .ValueGeneratedOnAdd();

            p_modelBuilder.Entity<Product>()
                .Property(product => product.ProductID)
                .ValueGeneratedOnAdd();

            p_modelBuilder.Entity<StoreFront>()
                .Property(store => store.StoreFrontID)
                .ValueGeneratedOnAdd();

            p_modelBuilder.Entity<Order>()
                .Property(order => order.OrderID)
                .ValueGeneratedOnAdd();    

            p_modelBuilder.Entity<LineItem>()
                .Property(item => item.LineItemID)
                .ValueGeneratedOnAdd();

            p_modelBuilder.Entity<OrderItem>()
                .Property(item => item.OrderItemID)
                .ValueGeneratedOnAdd();
        }
    }
}