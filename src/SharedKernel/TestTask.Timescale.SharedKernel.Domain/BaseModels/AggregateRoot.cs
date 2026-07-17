namespace TestTask.Timescale.SharedKernel.Domain.BaseModels
{
    public abstract class AggregateRoot : Entity
    {
        // На данный момент не требуются события или что-либо еще, что необходимо
        // поместить в корень агрегата, поэтому сейчас этот класс просто маркер
    }
}
