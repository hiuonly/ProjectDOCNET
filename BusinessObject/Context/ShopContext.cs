using BusinessObject.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Context
{
    public class ShopContext : DbContext
    {
        public ShopContext() { }
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyStoreDB"));
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        protected override void OnModelCreating(ModelBuilder optionBuilder)
        {
            optionBuilder.Entity<Role>().HasData(
                new Role { roleId = 1, roleName = "Admin" },
                new Role { roleId = 2, roleName = "User" }
                );
        }
    }
}
