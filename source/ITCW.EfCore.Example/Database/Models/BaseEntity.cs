namespace ITCW.EfCore.Example.Database.Models;

public abstract class BaseEntity
{
    public required Guid Id { get; set; }
}