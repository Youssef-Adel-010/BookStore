namespace BookStore.Core.ViewModels;

public class BookFormViewModel
{
    public int Id { get; set; }

    [MaxLength(50, ErrorMessage = Errors.MaxLength)]
    [Remote("UniqueTogetherValidation", "Books", AdditionalFields = "Id,AuthorId", ErrorMessage = Errors.DuplicatedTogether)]
    public string Title { get; set; } = null!;

    [MaxLength(50, ErrorMessage = Errors.MaxLength)]
    public string Publisher { get; set; } = null!;

    [MaxLength(50, ErrorMessage = Errors.MaxLength)]
    public string Hall { get; set; } = null!;

    [MaxLength(200, ErrorMessage = Errors.MaxLength)]
    public string Description { get; set; } = null!;

    [Display(Name = "Publishing Date")]
    [AssertThat("PublishingDate <= Today()", ErrorMessage = Errors.FutureDates)]
    public DateTime PublishingDate { get; set; } = DateTime.Now;

    [Display(Name = "This book is available for rental")]
    public bool IsAvailableForRental { get; set; }

    public IFormFile? Image { get; set; }

    public string? ImageUrl { get; set; }

    [ForeignKey(nameof(Author))]
    [Display(Name = "Author")]
    [Remote("UniqueTogetherValidation", "Books", AdditionalFields = "Id,Title", ErrorMessage = Errors.DuplicatedTogether)]
    public int AuthorId { get; set; }

    public IEnumerable<SelectListItem>? Authors { get; set; }

    [Display(Name = "Categories")]
    public IList<int> SelectedCategories { get; set; } = [];

    public IEnumerable<SelectListItem>? Categories { get; set; }
}

