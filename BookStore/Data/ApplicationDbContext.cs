using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BookStore.Data;
public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Category> Categories { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}

