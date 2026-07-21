namespace TestTask.Timescale.Application.Queries
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery
    {
        Task<TResult> HandleAsync(TQuery query, CancellationToken cancellation = default); 
    }
}
