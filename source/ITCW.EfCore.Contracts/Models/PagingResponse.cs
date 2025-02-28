namespace ITCW.EfCore.Contracts.Models;

public class PagingResponse<T>
{
    public IEnumerable<T> Data { get; set; } = [];

    public int Count { get; set; }
}