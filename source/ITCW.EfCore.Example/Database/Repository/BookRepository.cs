using ITCW.EfCore.Example.Database.Models;

namespace ITCW.EfCore.Example.Database.Repository;

public class BookRepository : BaseRepository<Book>, IBookRepository
{
    public BookRepository(LocalDbContext context) : base(context)
    {
    }
}