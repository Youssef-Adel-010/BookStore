namespace BookStore.Core.ViewModels;

public class CategoryFormViewModel
{
    public int Id { get; set; }

    [MaxLength(50, ErrorMessage = Errors.MaxLength)]
    [Display(Name = "Category")]
    [Remote("UniqueFieldValidation", "Categories", AdditionalFields = "Id", ErrorMessage = Errors.Duplicated)]
    public string Name { get; set; } = null!;
}
