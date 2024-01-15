using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAuthentication.Controllers;
using UserAuthentication.Data;
using UserAuthentication.Models;

namespace TestProject1
{
    internal class AdminCategoryUnitTest
    {
        [Test]
        public async Task TestControllerProductAsync_InMemory()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;


            using (var context = new ApplicationDbContext(options))
            {
                context.Categories.AddRange(new List<Category>
                {
                    new Category { Id = 1, Name = "Test Product1"},
                    new Category { Id = 2, Name = "Test Product2"}
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
