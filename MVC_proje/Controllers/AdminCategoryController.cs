using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace MVC_proje.Controllers
{
    public class AdminCategoryController : Controller
    {
        Context _context;
        EFCategoryDal efc;
        CategoryManager cm;

        public AdminCategoryController(Context context)
        {
            _context = context;
            efc = new EFCategoryDal(context);
            cm = new CategoryManager(efc);
        }

        public IActionResult Index()
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
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public IActionResult DeleteCategory(int id)
        {
            var categoryValue = cm.GetByID(id);
            cm.DeleteCategory(categoryValue);
            return View("Index");
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var categoryValue = cm.GetByID(id);
            return View(categoryValue);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category p)
        {
            cm.UpdateCategory(p);
            return View("Index");
        }
    }
}
