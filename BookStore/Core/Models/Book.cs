namespace BookStore.Core.Models;

[Index(nameof(Title), nameof(Publisher), IsUnique = true)]
public class Book : BaseModel
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string Title { get; set; } = null!;

    [MaxLength(50)]
    public string Publisher { get; set; } = null!;

    [MaxLength(50)]
    public string Hall { get; set; } = null!;

    public DateTime PublishingDate { get; set; }

    public string? ImageUrl { get; set; }

    public bool IsAvailableForRental { get; set; }

    public string Description { get; set; } = null!;

    [ForeignKey(nameof(Author))]
    public int AuthorId { get; set; }

    public virtual Author? Author { get; set; }

    public virtual ICollection<BookCategory> Categories { get; set; } = [];
}
