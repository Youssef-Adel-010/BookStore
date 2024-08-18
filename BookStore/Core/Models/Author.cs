namespace BookStore.Core.Models;

[Index(nameof(Name), IsUnique = true)]
public class Author : BaseModel
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; } = null!;
}
