using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace MVC_proje.Controllers
{
    public class CategoryController : Controller
    {
        CategoryManager cm = new CategoryManager();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetCategoryList()
        {
            List<Category> categoryValues = cm.GetAllBL();
            return View(categoryValues);
        }
    }
}
