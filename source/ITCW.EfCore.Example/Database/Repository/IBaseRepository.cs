using ITCW.EfCore.Contracts.Repositories;
using ITCW.EfCore.Example.Database.Models;

namespace ITCW.EfCore.Example.Database.Repository;

public interface IBaseRepository<TEntity> : IITCWBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    
}