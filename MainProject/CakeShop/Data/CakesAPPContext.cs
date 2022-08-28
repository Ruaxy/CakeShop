using CakeShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CakeShop.Data
{
    public class CakesAPPContext: DbContext
    {
        public CakesAPPContext(DbContextOptions<CakesAPPContext> options) : base(options)
        {
        }

        public DbSet<CakeModel> Cakes { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<CartModel> Carts { get; set; }
        public DbSet<CakeTypeModel> Types { get; set; }
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<UserModel> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CakeModel>().ToTable("Cake");
            modelBuilder.Entity<MessageLog>().ToTable("MessageLog");
            modelBuilder.Entity<OrderModel>().ToTable("Order");
            modelBuilder.Entity<CartModel>().ToTable("Cart");
            modelBuilder.Entity<CakeTypeModel>().ToTable("CakeType");
            modelBuilder.Entity<CustomerModel>().ToTable("Customer");
            modelBuilder.Entity<UserModel>().ToTable("User");
        }




    }
}