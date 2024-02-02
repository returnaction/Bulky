
using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAccessData;
using Bulky.Utility;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            // If you want name and display order not be the same when can do that trick
            //if(obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "DisplayOrder cannot be exactly match the Name.");
            //}

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }

            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
                return NotFound();

            Category? category = _unitOfWork.Category.Get(c => c.Id == id);
            if (category is null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {


            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category edited successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
                return NotFound();

            Category? category = _unitOfWork.Category.Get(c => c.Id == id);
            if (category is null)
                return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _unitOfWork.Category.Get(c => c.Id == id);

            if (obj == null)
                return NotFound();

            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index");
        }

    }
}
