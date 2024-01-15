using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserAuthentication.Data;
using UserAuthentication.Models;
using UserAuthentication.ViewModels;

namespace UserAuthentication.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private ApplicationDbContext context;

        public UsersController(ApplicationDbContext context, UserManager<IdentityUser> userManager) 
        {
            _context = context;
            _userManager = userManager; 
        }

        public UsersController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var model = new UsersIndexViewModel();

            var users = await _context.Users.ToListAsync();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var userWithRoles = new UserWithRoles
                {
                    User = user,
                    Roles = roles.ToList(),
                };

                model.Users.Add(userWithRoles);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole([FromForm] string userId, [FromForm] string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }

            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);

            var result = await _userManager.AddToRoleAsync(user, newRole); 

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return View("Error");
        }





        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminProduct()
        {
            var objProductList = _context.Products.ToList();

            return View(objProductList);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product obj)
        {
            _context.Products.Add(obj);
            _context.SaveChanges();
            return RedirectToAction("AdminProduct");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditProduct(int? id)
        {
            if (id == null || id==0)
            {
                return NotFound();
            }
            Product productFromDb = _context.Products.Find(id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Product obj)
        {
           _context.Products.Update(obj);
            _context.SaveChanges();
            return RedirectToAction("AdminProduct");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product productFromDb = _context.Products.Find(id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost, ActionName("DeleteProduct")]
        public async Task<IActionResult> DeleteProductPost(int? id)
        {
            Product obj = _context.Products.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _context.Products.Remove(obj);
            _context.SaveChanges();
            return RedirectToAction("AdminProduct");
        }




        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminCategory()
        {
            var objCategoryList = _context.Categories.ToList();
            return View(objCategoryList);

        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category obj)
        {
            _context.Categories.Add(obj);
            _context.SaveChanges();
            return RedirectToAction("AdminCategory");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditCategory(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category CategoryFromDb = _context.Categories.Find(id);
            if (CategoryFromDb == null)
            {
                return NotFound();
            }
            return View(CategoryFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(Category obj)
        {
            _context.Categories.Update(obj);
            _context.SaveChanges();
            return RedirectToAction("AdminCategory");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categortFromDb = _context.Categories.Find(id);
            if (categortFromDb == null)
            {
                return NotFound();
            }
            return View(categortFromDb);
        }

        [HttpPost, ActionName("DeleteCategory")]
        public async Task<IActionResult> DeleteCategoryPost(int? id)
        {
            Category obj = _context.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(obj);
            _context.SaveChanges();
            return RedirectToAction("AdminCategory");
        }

    }
}
