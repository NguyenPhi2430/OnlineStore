using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineStoreSolution.Data.Configuration;
using OnlineStoreSolution.Data.Entities;
using OnlineStoreSolution.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreSolution.Data.EF_Core
{
    public class OnlineStoreDBContext : IdentityDbContext<AppUser,Role,int>
    {
        public OnlineStoreDBContext(DbContextOptions<OnlineStoreDBContext> options) : base(options) { }
 

        // Configure Entities
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Data Seeding
            modelBuilder.Seed();

            //Fluent API Configuration
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductInCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new ProductImageConfiguration());

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles").HasKey(x=> new {x.UserId, x.RoleId});
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins").HasKey(x=>x.UserId);

            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserTokens").HasKey(x => x.UserId);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set;}
        public DbSet<OrderDetails> OrdersDetails { get; set;}
        public DbSet<ProductInCategory> ProductInCategories { get; set;}
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Role> Roles { get; set; }

    }
}
