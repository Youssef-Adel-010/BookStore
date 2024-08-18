namespace BookStore.Controllers;
public class AuthorsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AuthorsController(ApplicationDbContext context, IMapper mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var authors = _context.Authors
            .AsNoTracking()
            .ToList();

        var viewModel = _mapper.Map<IEnumerable<AuthorViewModel>>(authors);

        return View(viewModel);
    }

    [HttpGet]
    [AjaxOnly]
    public IActionResult Create()
    {
        return PartialView("_Form");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AuthorFormViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var author = _mapper.Map<Author>(model);

        _context.Authors.Add(author);
        _context.SaveChanges();

        var viewModel = _mapper.Map<AuthorViewModel>(author);

        return PartialView("_AuthorRow", viewModel);
    }

    [HttpGet]
    [AjaxOnly]
    public IActionResult Edit(int id)
    {
        var author = _context.Authors.Find(id);

        if (author is null)
            return NotFound();

        var model = _mapper.Map<AuthorFormViewModel>(author);

        return PartialView("_Form", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(AuthorFormViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var author = _context.Authors.Find(model.Id);

        if (author is null)
            return NotFound();

        author = _mapper.Map(model, author);
        author.LastUpdatedOn = DateTime.Now;
        _context.SaveChanges();

        var viewModel = _mapper.Map<AuthorViewModel>(author);

        return PartialView("_AuthorRow", viewModel);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ToggleStatus(int id)
    {
        var author = _context.Authors.Find(id);

        if (author is null)
            return NotFound();

        author.IsDeleted = !author.IsDeleted;
        author.LastUpdatedOn = DateTime.Now;

        _context.SaveChanges();

        return Ok(author.LastUpdatedOn.ToString());
    }

    public IActionResult UniqueFieldValidation(AuthorFormViewModel model)
    {
        var author = _context.Authors.SingleOrDefault(c => c.Name == model.Name);

        bool IsAccepted = author is null || author.Id.Equals(model.Id);

        return Json(IsAccepted);
    }

    [HttpGet]
    public IActionResult Clear()
    {
        _context.Authors.ExecuteDelete();
        _context.SaveChanges();

        TempData["feedbackMessage"] = "The data cleared successfully";

        return RedirectToAction(nameof(Index));
    }
}
