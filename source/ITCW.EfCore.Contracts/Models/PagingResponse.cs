namespace ITCW.EfCore.Contracts.Models;

/// <summary>
/// POCO for storing paged responses.
/// </summary>
/// <typeparam name="T">Type of the paged data.</typeparam>
public class PagingResponse<T>
{
    /// <summary>
    /// The returned data.
    /// </summary>
    public IEnumerable<T> Data { get; set; } = [];

    /// <summary>
    /// The total amount of data entries.
    /// </summary>
    public int Count { get; set; }
}