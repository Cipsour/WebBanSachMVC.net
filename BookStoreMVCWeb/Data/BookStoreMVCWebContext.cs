using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookStoreMVCWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BookStoreMVCWeb.Data
{
    public class BookStoreMVCWebContext : IdentityDbContext<AppUser>
    {
        public BookStoreMVCWebContext (DbContextOptions<BookStoreMVCWebContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<OrderDeatails> OrderDeatails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }

    }
}
