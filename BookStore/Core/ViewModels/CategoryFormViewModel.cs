using Microsoft.AspNetCore.Mvc;

namespace BookStore.Core.ViewModels;

public class CategoryFormViewModel
{
    public int Id { get; set; }

    [MaxLength(50)]
    [Remote("UniqueFieldValidation", "Categories", AdditionalFields = "Id", ErrorMessage = "Field with the same name is already exist")]
    public string Name { get; set; } = null!;
}
