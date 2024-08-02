using BookStore.Core.Models;
using BookStore.Core.ViewModels;
using BookStore.Data;
using Microsoft.AspNetCore.Mvc;

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
        var categories = _context.Categories.ToList();
        return View(categories);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreateCategoryViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        Category category = new() { Name = viewModel.Name };

        _context.Categories.Add(category);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

}
