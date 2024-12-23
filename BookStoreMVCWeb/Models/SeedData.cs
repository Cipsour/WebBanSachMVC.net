using BookStoreMVCWeb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BookStoreMVCWeb.Models
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelbuilder)
        {
           
                modelbuilder.Entity<Book>().HasData(
                 new Book
                 {
                     Id=1,
                     Title = "Bröderna Lejonhjärta",
                     Language = "Swedish",
                     IBNS = "9789129688313",
                     DatePublished = DateTime.Parse("2013-9-26"),
                     Description="",
                     Price = 139,
                     Author = "Astrid Lindgren",
                     ImageUrl = "/themes/images/tab-item1.jpg",
                     Category= "adventure"
                 },

                 new Book
                 {
                     Id=6,
                     Title = "The Fellowship of the Ring",
                     Language = "English",
                     IBNS = "9780261102354",
                     DatePublished = DateTime.Parse("1991-7-4"),
                     Description = "",
                     Price = 100,
                     Author = "J. R. R. Tolkien",
                     ImageUrl = "/themes/images/tab-item6.jpg",
                     Category= "technology"
                 },

                 new Book
                 {
                     Id=2,
                     Title = "Mystic River",
                     Language = "English",
                     IBNS = "9780062068408",
                     DatePublished = DateTime.Parse("2011-6-1"),
                     Description = "",
                     Price = 91,
                     Author = "Dennis Lehane",
                     ImageUrl = "/themes/images/tab-item2.jpg",
                     Category= "fictional"
                 },

                 new Book
                 {
                     Id=5,
                     Title = "Of Mice and Men",
                     Language = "English",
                     IBNS = "9780062068408",
                     DatePublished = DateTime.Parse("1994-1-2"),
                     Description = "",
                     Price = 166,
                     Author = "John Steinbeck",
                     ImageUrl = "/themes/images/tab-item5.jpg",
                     Category= "romantic"
                 },

                 new Book
                 {
                     Id=3,
                     Title = "The Old Man and the Sea",
                     Language = "English",
                     IBNS = "9780062068408",
                     DatePublished = DateTime.Parse("1994-8-18"),
                     Description = "",
                     Price = 84,
                     Author = "Ernest Hemingway",
                     ImageUrl = "/themes/images/tab-item3.jpg",
                     Category= "adventure"
                 },

                 new Book
                 {
                     Id=4,
                     Title = "The Road",
                     Language = "English",
                     IBNS = "9780307386458",
                     DatePublished = DateTime.Parse("2007-5-1"),
                     Description = "",
                     Price = 95,
                     Author = "Cormac McCarthy",
                     ImageUrl = "/themes/images/tab-item7.jpg",
                     Category= "adventure"
                 }
             );
          SeedRoles(modelbuilder);
           SeedUser(modelbuilder);
        }
        public static void SeedRoles(this ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<IdentityRole>().HasData(
               new IdentityRole(){ Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
               new IdentityRole() { Id = "2", Name = "User", NormalizedName = "USER" }
           );
        }
        public static void SeedUser(this ModelBuilder modelbuilder)
        {
            var adminUser = new AppUser
            {
                Id = "1",
                Opccupation="AdminWeb",
                PhoneNumber="0765184992",
                UserName = "admin@example.com",  //xíu lấy Tài Khoản Này Để test Admin
                Email = "admin@example.com",
                NormalizedUserName = "ADMIN@EXAMPLE.COM",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true
            };
            adminUser.PasswordHash = new PasswordHasher<AppUser>().HashPassword(adminUser, "Admin123"); //SHA256

            modelbuilder.Entity<AppUser>().HasData(adminUser);

            // Assign Admin user to Admin role
            modelbuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "1", UserId = "1" }
            );

            // Seed regular User
            var regularUser = new AppUser
            {
                Id = "2",
                Opccupation="BacSi",
                PhoneNumber="0123456789",
                UserName = "user@example.com",//xíu lấy Tài Khoản Này Để test User
                Email = "user@example.com",
                NormalizedUserName = "USER@EXAMPLE.COM",
                NormalizedEmail = "USER@EXAMPLE.COM",
                EmailConfirmed = true
            };
            regularUser.PasswordHash = new PasswordHasher<AppUser>().HashPassword(regularUser, "User123");//pass

            modelbuilder.Entity<AppUser>().HasData(regularUser);

            // Assign regular User to User role
            modelbuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "2", UserId = "2" }
            );
        }

    }
}
