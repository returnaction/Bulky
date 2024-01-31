using BulkyWebRazor_Web.Data;
using BulkyWebRazor_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Web.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Category? Category { get; set; }
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int? id)
        {
            if(id != null & id != 0)
            {
                Category = _db.Categories.FirstOrDefault(c => c.Id == id);
            }

        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(Category);
                _db.SaveChanges();
                //TempData["success"] = "Category created successfully";
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
