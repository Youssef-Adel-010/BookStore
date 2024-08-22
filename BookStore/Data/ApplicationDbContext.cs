using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BookStore.Data;
public class ApplicationDbContext : IdentityDbContext
{
    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<BookCategory> BookCategories { get; set; }
    public virtual DbSet<Category> Categories { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<BookCategory>()
            .HasKey(e => new { e.BookId, e.CategoryId });

        base.OnModelCreating(builder);
    }
}