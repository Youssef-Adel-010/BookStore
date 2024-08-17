using BookStore.Core.Models;
using BookStore.Core.ViewModels;
using BookStore.Data;
using BookStore.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers;
public class CategoriesController : Controller
{
    private readonly ApplicationDbContext _context;

    public CategoriesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var categories = _context.Categories.AsNoTracking().ToList();
        return View(categories);
    }

    [HttpGet]
    [AjaxOnly]
    public IActionResult Create()
    {
        return PartialView("_Form");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CategoryFormViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        Category category = new() { Name = model.Name };

        _context.Categories.Add(category);
        _context.SaveChanges();

        return PartialView("_CategoryRow", category);
    }

    [HttpGet]
    [AjaxOnly]
    public IActionResult Edit(int id)
    {
        Category? category = _context.Categories.Find(id);
        if (category is null)
            return NotFound();

        CategoryFormViewModel model = new()
        {
            Id = category.Id,
            Name = category.Name
        };

        return PartialView("_Form", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CategoryFormViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        Category? category = _context.Categories.Find(model.Id);

        if (category is null)
            return NotFound();

        category.Name = model.Name;
        category.LastUpdatedOn = DateTime.Now;

        _context.SaveChanges();

        return PartialView("_CategoryRow", category);

    }
    [HttpGet]
    public IActionResult Clear()
    {
        _context.Categories.ExecuteDelete();
        _context.SaveChanges();

        TempData["feedbackMessage"] = "The data cleared successfully";

        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ToggleStatus(int id)
    {
        Category? category = _context.Categories.Find(id);

        if (category is null)
            return NotFound();

        category.IsDeleted = !category.IsDeleted;
        category.LastUpdatedOn = DateTime.Now;

        _context.SaveChanges();

        return Ok(category.LastUpdatedOn.ToString());
    }

    public IActionResult UniqueFieldValidation(CategoryFormViewModel model)
    {
        Category? category = _context.Categories.SingleOrDefault(c => c.Name == model.Name);
        bool IsAccepted = category is null || category.Id.Equals(model.Id);

        return Json(IsAccepted);
    }
}
