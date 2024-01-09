using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserAuthentication.Models;

namespace UserAuthentication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Category>().HasData(
        //        new Category { Id = 1, Name = "Phones" },
        //        new Category { Id = 2, Name = "Smart Watch" }
        //        );
        //    modelBuilder.Entity<Product>().HasData(
        //        new Product { Id = 1, Name = "Iphone 12", Price = 6490 },
        //        new Product { Id = 2, Name = "Samsung Galexy S23", Price = 11490 }
        //        );
        //}
    }
}
