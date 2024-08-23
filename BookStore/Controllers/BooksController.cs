namespace BookStore.Controllers;
public class BooksController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly List<string> _allowedExtensions = [".jpg", ".jpeg", ".png"];
    private const int _maxAllowedSize = 2_097_152;

    public BooksController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
    {
        this._context = context;
        this._mapper = mapper;
        this._webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View("Form", FillViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(BookFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View("Form", FillViewModel(model));

        var book = _mapper.Map<Book>(model);

        if (model.Image is not null)
        {
            var extension = Path.GetExtension(model.Image.FileName);

            if (!_allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError(nameof(model.Image), Errors.InvalidExtension);
                return View("Form", FillViewModel(model));
            }

            if (model.Image.Length > _maxAllowedSize)
            {

                ModelState.AddModelError(nameof(model.Image), Errors.MaxSize);
                return View("Form", FillViewModel(model));
            }

            var ImageName = $"{Guid.NewGuid()}{extension}";

            var path = Path.Combine($"{_webHostEnvironment.WebRootPath}/images/books", ImageName);

            using var stream = System.IO.File.Create(path);

            model.Image.CopyTo(stream);

            book.ImageUrl = ImageName;

        }

        foreach (var categoryId in model.SelectedCategories)
        {
            book.Categories.Add(new BookCategory() { CategoryId = categoryId });
        }

        _context.Add(book);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var book = _context.Books.Find(id);

        if (book is null)
            return NotFound();

        var viewModel = FillViewModel(_mapper.Map<BookFormViewModel>(book));

        viewModel.SelectedCategories = book.Categories.Select(c => c.CategoryId).ToList();

        return View("Form", viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(BookFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View("Form", FillViewModel(model));

        var book = _context.Books.Find(model.Id);

        if (book is null)
            return NotFound();

        if (model.Image is not null)
        {
            if (!string.IsNullOrEmpty(book.ImageUrl))
            {
                var oldImagePath = Path.Combine($"{_webHostEnvironment.WebRootPath}/images/books", book.ImageUrl);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            var extension = Path.GetExtension(model.Image.FileName);

            if (!_allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError(nameof(model.Image), Errors.InvalidExtension);
                return View("Form", FillViewModel(model));
            }

            if (model.Image.Length > _maxAllowedSize)
            {
                ModelState.AddModelError(nameof(model.Image), Errors.MaxSize);
                return View("Form", FillViewModel(model));
            }

            var ImageName = $"{Guid.NewGuid()}{extension}";

            var path = Path.Combine($"{_webHostEnvironment.WebRootPath}/images/books", ImageName);

            using var stream = System.IO.File.Create(path);

            model.Image.CopyTo(stream);

            model.ImageUrl = ImageName;

        }
        else if (model.Image is null && !string.IsNullOrEmpty(book.ImageUrl))
        {
            model.ImageUrl = book.ImageUrl;
        }

        book = _mapper.Map(model, book);
        book.LastUpdatedOn = DateTime.Now;
        book.Categories.Clear();

        foreach (var categoryId in model.SelectedCategories)
        {
            book.Categories.Add(new BookCategory() { CategoryId = categoryId });
        }

        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public bool UniqueTogetherValidation(BookFormViewModel model)
    {
        var book = _context.Books.SingleOrDefault(b => b.Title == model.Title && b.AuthorId == model.AuthorId);
        var isAllowed = book is null || book.Id.Equals(model.Id);

        return isAllowed;
    }

    private BookFormViewModel FillViewModel(BookFormViewModel? model = null)
    {
        var viewModel = model is null ? new BookFormViewModel() : model;

        var authors = _context.Authors
            .Where(a => !a.IsDeleted)
            .OrderBy(a => a.Name)
            .ToList();

        var categories = _context.Categories
            .Where(c => !c.IsDeleted)
            .OrderBy(c => c.Name)
            .ToList();

        viewModel.Authors = _mapper.Map<IEnumerable<SelectListItem>>(authors);
        viewModel.Categories = _mapper.Map<IEnumerable<SelectListItem>>(categories);

        return viewModel;
    }
}