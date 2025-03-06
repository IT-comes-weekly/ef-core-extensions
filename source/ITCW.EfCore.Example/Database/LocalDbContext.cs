using ITCW.EfCore.Example.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ITCW.EfCore.Example.Database;

public class LocalDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasKey(x => x.Id);
    }
}