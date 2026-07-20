namespace TestTask.Timescale.SharedKernel.Domain.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransactionAsync(CancellationToken cancellation = default);

        Task CommitTransactionAsync(CancellationToken cancellation = default);

        Task RollbackTransactionAsync(CancellationToken cancellation = default);

        Task SaveAsync(CancellationToken cancellation = default);

        bool HasActiveTransaction();
    }
}
