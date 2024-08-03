namespace BookStore.Core.ViewModels;

public class CategoryFormViewModel
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; } = null!;
}
