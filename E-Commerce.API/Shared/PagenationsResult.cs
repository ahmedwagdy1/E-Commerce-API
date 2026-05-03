namespace Shared
{
    public record PagenationsResult<TData>(int? PageIndex, int? PageSize, int? TotalCount, IEnumerable<TData>? Data)
    {
    }
}
