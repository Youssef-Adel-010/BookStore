namespace BookStore.Core.Models;

[Index(nameof(Name), IsUnique = true)]
public class Category : BaseModel
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; } = null!;

    public ICollection<BookCategory> Books { get; set; } = [];

}
