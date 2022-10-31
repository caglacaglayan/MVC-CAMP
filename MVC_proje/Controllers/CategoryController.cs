using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace MVC_proje.Controllers
{
    public class CategoryController : Controller
    {
        Context _context;
        EFCategoryDal efc;
        CategoryManager cm;

        public CategoryController(Context context)
        {
            _context = context;
            efc = new EFCategoryDal(context);
            cm = new CategoryManager(efc);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetCategoryList()
        {
            var categoryValues = cm.GetList();
            return View(categoryValues);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category p)
        {
            CategoryValidator validationRules = new CategoryValidator();
            ValidationResult validationResult = validationRules.Validate(p);
            if (validationResult.IsValid)
            {
                cm.AddCategory(p);
                return RedirectToAction("GetCategoryList");
            }
            else
            {
                foreach(var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return RedirectToAction("GetCategoryList");
        }
    }
}
