using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserAuthentication.Controllers;
using UserAuthentication.Data;
using UserAuthentication.Models;

namespace UnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]


        //Test database for Product
        public async Task TestControllerIndexAsync_InMemory()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Products.AddRange(new List<Product>
                {
                    new Product { Id = 1, Name = "Test product 1", Price = 1},
                    new Product { Id = 2, Name = "Test product 2", Price= 2}
                });
                await context.SaveChangesAsync();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var controller = new UsersController(context);
                var result = await controller.AdminProduct() as ViewResult;
                var model = result?.Model as List<Product>;


                Assert.That(result, Is.Not.Null);
                Assert.That(model?.Count, Is.EqualTo(2));
            }
        }

        [Test]
        //test for category
        public async Task TestControllerControllerAsync_InMemory()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "TestDatabase")
               .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Categories.AddRange(new List<Category>
                {
                    new Category { Id = 1, Name = "Test category 1"},
                    new Category { Id = 2, Name = "Test category 2"}
                });
                await context.SaveChangesAsync();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var controller = new UsersController(context);
                var result = await controller.AdminProduct() as ViewResult;
                var model = result?.Model as List<Category>;


                Assert.That(result, Is.Not.Null);
                Assert.That(model?.Count, Is.EqualTo(2));
            }
        }
    }
}