using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TestTask.Timescale.SharedKernel.Domain.BaseModels;
using TestTask.Timescale.SharedKernel.Domain.Data;
using TestTask.Timescale.SharedKernel.Domain.Events;
using TestTask.Timescale.SharedKernel.Infrastructure.Resources;

namespace TestTask.Timescale.SharedKernel.Infrastructure
{
    public abstract class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IDomainEventDispatcher _eventDispatcher;
        private bool _disposed;

        protected readonly DbContext _db;

        protected IDbContextTransaction? _currentTransaction;

        protected UnitOfWork(DbContext db, IDomainEventDispatcher eventDispatcher)
        {
            ArgumentNullException.ThrowIfNull(db);
            ArgumentNullException.ThrowIfNull(eventDispatcher);

            _db = db;
            _eventDispatcher = eventDispatcher;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellation = default)
        {
            if (HasActiveTransaction())
            {
                throw new InvalidOperationException(ExceptionMessages.UnitOfWorkBeginAlreadyBegunningTransaction);
            }

            _currentTransaction = await _db.Database.BeginTransactionAsync(cancellation);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellation = default)
        {
            if (_currentTransaction == null)
            {
                throw new InvalidOperationException(ExceptionMessages.UnitOfWorkCommitUnbeggunningTransaction);
            }

            IEnumerable<AggregateRoot> aggregates = _db.ChangeTracker.Entries<AggregateRoot>()
                .Select(e => e.Entity);

            try
            {
                await _db.SaveChangesAsync(cancellation);

                foreach (IDomainEvent domainEvent in aggregates.SelectMany(e => e.Events))
                {
                    _eventDispatcher.Dispatch(domainEvent);
                }

                await _currentTransaction.CommitAsync(cancellation);
            }
            catch
            {
                await RollbackTransactionAsync(cancellation);
                throw;
            }
            finally
            {
                foreach (AggregateRoot aggregate in aggregates)
                {
                    aggregate.ClearDomainEvents();
                }
                ResetTransaction();
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellation = default)
        {
            if (_currentTransaction == null)
            {
                throw new InvalidOperationException(ExceptionMessages.UnitOfWorkRollbackUnbeggunningTransaction);
            }

            try
            {
                await _currentTransaction.RollbackAsync(cancellation);
            }
            finally
            {
                ResetTransaction();
            }
        }

        public async Task SaveAsync(CancellationToken cancellation = default)
        {
            await _db.SaveChangesAsync(cancellation);
        }

        [MemberNotNullWhen(true, nameof(_currentTransaction))]
        public bool HasActiveTransaction()
        {
            return _currentTransaction != null;
        }

        private void ResetTransaction()
        {
            if (HasActiveTransaction())
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            if (disposing)
            {
                ResetTransaction();
                _db.Dispose();
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
