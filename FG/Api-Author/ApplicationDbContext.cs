
using Api_Author.Entities;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AuthorBook>()
                .HasKey(authorBook => new {authorBook.AuthorId, authorBook.BookId});
    }

    public DbSet<Author> Authors { get; set; }

    public DbSet<Book> Books { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<AuthorBook> AuthorBooks { get; set; }

}