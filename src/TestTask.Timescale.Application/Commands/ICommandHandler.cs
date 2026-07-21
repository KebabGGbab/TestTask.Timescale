namespace TestTask.Timescale.Application.Commands
{
    public interface ICommandHandler<TCommand, TResult>
        where TCommand : ICommand
    {
        Task<TResult> HandleAsync(TCommand command, CancellationToken cancellation = default);
    }
}
