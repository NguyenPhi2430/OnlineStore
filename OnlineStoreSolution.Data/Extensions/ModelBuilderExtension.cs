using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStoreSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreSolution.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                    new Product{
                        Id= 1,
                        Name = "Product 1",
                        Description= "Description 1",
                        Price= 20000,
                        Stock= 10,
                        Views = 0,
                        SeoAlias = "Product",
                        Date= DateTime.Now,
                    }
                );

            var Role_Id = 101;
            var Admin_Id = 101;
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = Role_Id,
                    Name = "admin",
                    NormalizedName = "admin",
                    Desc = "Adminnistrator"
                });

            var hasher = new PasswordHasher<User>();
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Admin_Id,
                    UserName = "admin",
                    NormalizedUserName = "admin",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "admin@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "123456"),
                    SecurityStamp = string.Empty,
                    firstName = "Nguyen",
                    lastName = "Phi",
                    dateOfBirth = new DateTime(1996,8,24)
                });
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                RoleId = Role_Id,
                UserId = Admin_Id
            });
        }
    }
}
