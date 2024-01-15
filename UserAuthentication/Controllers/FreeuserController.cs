using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserAuthentication.Data;

namespace UserAuthentication.Controllers
{
    public class FreeuserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public FreeuserController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Product()
        {
            var objProductList = _context.Products.ToList();
            return View(objProductList);
        }

        public async Task<IActionResult> Category()
        {
            var objCategoryList = _context.Categories.ToList();
            return View(objCategoryList);
        }
    }
}
