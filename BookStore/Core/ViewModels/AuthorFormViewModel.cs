namespace BookStore.Core.ViewModels;

public class AuthorFormViewModel
{
    public int Id { get; set; }

    [MaxLength(50, ErrorMessage = Errors.MaxLength)]
    [Display(Name = "Category")]
    [Remote("UniqueFieldValidation", "Authors", AdditionalFields = "Id", ErrorMessage = Errors.Duplicated)]
    public string Name { get; set; } = null!;
}
