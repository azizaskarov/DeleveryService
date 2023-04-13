using DeliveryService.Domain.Entities.Categories;
using DeliveryService.Domain.Entities.Compositions;
using DeliveryService.Domain.Entities.Orders;
using DeliveryService.Domain.Entities.Products;
using DeliveryService.Domain.Entities.TimeCategories;
using DeliveryService.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Composition> Compositions { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<TimeCategory> TimeCategories { get; set; }
        public object TimeCategorys { get; internal set; }
    }
}
