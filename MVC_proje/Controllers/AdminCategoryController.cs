using Microsoft.AspNetCore.Mvc;

namespace MVC_proje.Controllers
{
    public class AdminCategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
