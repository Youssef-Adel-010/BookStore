namespace BookStore.Core.ViewModels;

public class CreateCategoryViewModel
{
    [MaxLength(50)]
    public string Name { get; set; } = null!;
}
