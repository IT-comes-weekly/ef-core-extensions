using ITCW.EfCore.Example.Database.Models;
using ITCW.EfCore.Repositories;

namespace ITCW.EfCore.Example.Database.Repository;

public class BaseRepository<TEntity> : ITCWBaseRepository<TEntity, LocalDbContext>, IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    public BaseRepository(LocalDbContext context) : base(context)
    {
    }
}