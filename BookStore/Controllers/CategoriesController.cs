﻿using BookStore.Core.Models;
using BookStore.Core.ViewModels;
using BookStore.Data;
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
    public IActionResult Create()
    {
        return PartialView("_Form");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CategoryFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View("_Form", viewModel);

        Category category = new() { Name = viewModel.Name };

        _context.Categories.Add(category);
        _context.SaveChanges();

        TempData["feedbackMessage"] = "Field created successfully";

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        Category? category = _context.Categories.Find(id);
        if (category is null)
            return NotFound();

        CategoryFormViewModel viewModel = new()
        {
            Id = category.Id,
            Name = category.Name
        };

        return View("_Form", viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CategoryFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View("_Form", viewModel);
        Category? category = _context.Categories.Find(viewModel.Id);

        if (category is null)
            return NotFound();

        category.Name = viewModel.Name;
        category.LastUpdatedOn = DateTime.Now;

        _context.SaveChanges();

        TempData["feedbackMessage"] = "Field edited successfully";

        return RedirectToAction(nameof(Index));

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

        TempData["feedbackMessage"] = "Field status toggled successfully";

        return Ok(category.LastUpdatedOn.ToString());
    }

}
